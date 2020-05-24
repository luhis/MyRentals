import { h, Fragment, FunctionComponent } from "preact";
import { Container, Button, Title } from "rbx";
import { useState, useEffect } from "preact/hooks";

import Map from "../../components/apartments/Map";
import FiltersComp from "../../components/apartments/Filters";
import Modal from "../../components/apartments/Modal";
import ApartmentsTable from "../../components/apartments/ApartmentsTable";
import GeoCodeModal from "../../components/apartments/GeoCodeModal";
import {
    getApartments,
    deleteApartment,
    putApartment,
    postApartment,
} from "../../api/apartments";
import {
    Apartment,
    Realtor,
    Coords,
    Filters,
    EditableApartment,
    LoadingState,
    valueOr,
    GoogleAuth,
} from "../../types/models";
import { useGoogleAuth, useAccess } from "../../components/app";
import { getRealtors } from "../../api/realtors";
import { getAccessToken } from "../../api/api";

interface State {
    apartments: LoadingState<readonly Apartment[]>;
    realtors: LoadingState<readonly Realtor[]>;
    filters: Filters;
    editing: EditableApartment | undefined;
    showGeoCodeModal: boolean;
}

const Apartments: FunctionComponent = () => {
    const [state, setState] = useState<State>({
        apartments: { tag: "Loading" },
        realtors: { tag: "Loading" },
        filters: { floorArea: "", pricePerMonth: "", numberOfRooms: "" },
        editing: undefined,
        showGeoCodeModal: false,
    });
    const populateApartmentData = async (auth: GoogleAuth): Promise<void> => {
        const data = await getApartments(getAccessToken(auth), state.filters);
        setState((s: State) => ({
            ...s,
            apartments: data,
            editing: undefined,
        }));
    };

    const populateRealtorData = async (auth: GoogleAuth): Promise<void> => {
        const data = await getRealtors(getAccessToken(auth));
        setState((s) => ({
            ...s,
            realtors: data,
        }));
    };
    const save = async (auth: GoogleAuth): Promise<void> => {
        try {
            if (state.editing) {
                if (state.editing.apartmentId !== undefined) {
                    putApartment(getAccessToken(auth), state.editing);
                } else {
                    postApartment(getAccessToken(auth), state.editing);
                }
                await populateApartmentData(auth);
            }
        } catch (error) {
            console.log(error);
        }
    };
    const deleteApartmentAndReload = async (
        auth: GoogleAuth,
        apartmentId: number
    ): Promise<void> => {
        await deleteApartment(getAccessToken(auth), apartmentId);
        await populateApartmentData(auth);
    };
    const auth = useGoogleAuth();
    useEffect(() => {
        populateRealtorData(auth);
    }, [auth]);
    useEffect(() => {
        populateApartmentData(auth);
    }, [auth]);

    const setValue = (name: Partial<Filters>): void => {
        setState((s: State) => ({
            ...s,
            filters: { ...s.filters, ...name },
        }));
    };
    const setField = (field: Partial<EditableApartment>): void => {
        setState((s: State) => ({
            ...s,
            editing: { ...s.editing, ...field } as EditableApartment,
        }));
    };
    const setLatLon = (coords: Coords): void => {
        setState((s) => ({
            ...s,
            editing: {
                ...s.editing,
                lat: coords.lat,
                lon: coords.lon,
            } as EditableApartment,
        }));
    };

    const setEditing = (apartment: EditableApartment | undefined): void => {
        setState((s) => ({ ...s, editing: apartment }));
    };

    const toggleGeoModal = (): void => {
        setState((s) => ({
            ...s,
            showGeoCodeModal: !s.showGeoCodeModal,
        }));
    };

    const addApartment = (): void =>
        setEditing({
            apartmentId: undefined,
            name: "",
            description: "",
            floorArea: 0,
            pricePerMonth: 0,
            numberOfRooms: 0,
            realtorId: valueOr(state.realtors, (a) => a[0].realtorId, -1),
            isRented: false,
            lat: 0,
            lon: 0,
        });

    const [{ canEditApartments }] = useAccess();
    const contents = (a: LoadingState<readonly Apartment[]>): JSX.Element => {
        switch (a.tag) {
            case "Error":
                return <p>{a.value}</p>;

            case "Loading":
                return (
                    <p>
                        <em>Loading...</em>
                    </p>
                );
            case "Loaded":
                return (
                    <Fragment>
                        {state.editing && state.realtors.tag === "Loaded" ? (
                            <Modal
                                save={(): Promise<void> => save(auth)}
                                cancel={(): void => setEditing(undefined)}
                                apartment={state.editing}
                                realtors={state.realtors.value}
                                setField={setField}
                                toggleGeoLocation={toggleGeoModal}
                            />
                        ) : null}
                        {state.showGeoCodeModal ? (
                            <GeoCodeModal
                                setLatLon={setLatLon}
                                toggleGeoLocation={toggleGeoModal}
                            />
                        ) : null}
                        <FiltersComp
                            filters={state.filters}
                            setValue={setValue}
                        />
                        <ApartmentsTable
                            apartments={a.value}
                            deleteApartment={(
                                apartmentId: number
                            ): Promise<void> =>
                                deleteApartmentAndReload(auth, apartmentId)
                            }
                            setEditing={setEditing}
                        />
                        {canEditApartments ? (
                            <Button onClick={addApartment}>+</Button>
                        ) : null}
                    </Fragment>
                );
        }
    };

    return (
        <Fragment>
            <Container>
                <Title>Apartments</Title>
                {contents(state.apartments)}
            </Container>
            {state.apartments.tag === "Loaded" ? (
                <Map apartments={state.apartments.value} />
            ) : null}
        </Fragment>
    );
};

export default Apartments;
