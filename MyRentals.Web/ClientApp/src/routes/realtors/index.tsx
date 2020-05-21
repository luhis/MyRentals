import { h, Fragment, FunctionComponent } from "preact";
import { Container, Button, Title } from "rbx";
import { useState, useEffect } from "preact/hooks";

import Modal from "../../components/realtors/Modal";
import {
    Realtor,
    EditableRealtor,
    LoadingState,
    GoogleAuth,
} from "../../types/models";
import RealtorsTable from "../../components/realtors/RealtorsTable";
import {
    getRealtors,
    putRealtor,
    postRealtor,
    deleteRealtor,
} from "../../api/realtors";
import { useGoogleAuth } from "../../components/app";
import { getAccessToken } from "../../api/api";

interface State {
    realtors: LoadingState<readonly Realtor[]>;
    editing: EditableRealtor | undefined;
}

const Realtors: FunctionComponent = () => {
    const [state, setState] = useState<State>({
        realtors: { tag: "Loading" },
        editing: undefined,
    });
    const populateRealtorData = async (auth: GoogleAuth): Promise<void> => {
        const data = await getRealtors(getAccessToken(auth));
        setState({
            realtors: data,
            editing: undefined,
        });
    };

    const deleteRealtorAndReload = async (
        auth: GoogleAuth,
        realtorId: number
    ): Promise<void> => {
        await deleteRealtor(getAccessToken(auth), realtorId);
        await populateRealtorData(auth);
    };
    const save = async (auth: GoogleAuth): Promise<void> => {
        try {
            if (state.editing) {
                if (state.editing.realtorId !== undefined) {
                    await putRealtor(getAccessToken(auth), state.editing);
                } else {
                    await postRealtor(getAccessToken(auth), state.editing);
                }
                await populateRealtorData(auth);
            }
        } catch (error) {
            console.log(error);
        }
    };
    const auth = useGoogleAuth();
    useEffect(() => {
        populateRealtorData(auth);
    }, [auth]);

    const setEditing = (realtor: EditableRealtor | undefined): void => {
        setState((s) => ({ ...s, editing: realtor }));
    };

    const setField = (field: Partial<EditableRealtor>): void => {
        setState((s) => ({
            ...s,
            editing: { ...s.editing, ...field } as EditableRealtor,
        }));
    };
    const newRealtor = (): void =>
        setEditing({
            realtorId: undefined,
            realtorName: "",
            realtorEmail: "",
        });
    const contents = (r: LoadingState<readonly Realtor[]>): JSX.Element => {
        switch (r.tag) {
            case "Error":
                return <p>{r.value}</p>;
            case "Loading":
                return (
                    <p>
                        <em>Loading...</em>
                    </p>
                );
            case "Loaded":
                return (
                    <Fragment>
                        {state.editing ? (
                            <Modal
                                save={(): Promise<void> => save(auth)}
                                cancel={(): void => setEditing(undefined)}
                                realtor={state.editing}
                                setField={setField}
                            />
                        ) : null}
                        <RealtorsTable
                            realtors={r.value}
                            deleteItem={(realtorId): Promise<void> =>
                                deleteRealtorAndReload(auth, realtorId)
                            }
                            setEditing={setEditing}
                        />
                        <Button color="primary" onClick={newRealtor}>
                            +
                        </Button>
                    </Fragment>
                );
        }
    };

    return (
        <Container>
            <Title>Realtors</Title>
            {contents(state.realtors)}
        </Container>
    );
};

export default Realtors;
