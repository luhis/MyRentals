import { h, Fragment, FunctionComponent } from "preact";
import { Container, Title, Button } from "rbx";
import { useState, useEffect } from "preact/hooks";

import ModalComp from "../../components/clients/Modal";
import {
    getClients,
    deleteClient,
    putClient,
    postClient,
} from "../../api/clients";
import { Client, EditableClient, LoadingState } from "../../types/models";
import ClientsTable from "../../components/clients/ClientsTable";
import { useGoogleAuth, GoogleAuth } from "../../components/app";
import { getAccessToken } from "../../api/api";

interface State {
    clients: LoadingState<readonly Client[]>;
    editing: EditableClient | undefined;
    showGeoCodeModal: boolean;
}

const Clients: FunctionComponent = () => {
    const [state, setState] = useState<State>({
        clients: { tag: "Loading" },
        editing: undefined,
        showGeoCodeModal: false,
    });
    const populateClientData = async (auth: GoogleAuth): Promise<void> => {
        const data = await getClients(getAccessToken(auth));
        setState({
            clients: data,
            editing: undefined,
            showGeoCodeModal: false,
        });
    };

    const deleteClientAndReload = async (
        auth: GoogleAuth,
        clientId: number
    ): Promise<void> => {
        await deleteClient(getAccessToken(auth), clientId);
        await populateClientData(auth);
    };

    const save = async (auth: GoogleAuth) => {
        try {
            if (state.editing) {
                if (state.editing.clientId !== undefined) {
                    await putClient(getAccessToken(auth), state.editing);
                } else {
                    await postClient(getAccessToken(auth), state.editing);
                }
                await populateClientData(auth);
            }
        } catch (error) {
            console.log(error);
        }
    };

    const auth = useGoogleAuth();

    useEffect(() => {
        populateClientData(auth);
    }, [auth]);

    const setEditing = (client: EditableClient | undefined): void => {
        setState((s) => ({ ...s, editing: client }));
    };
    const setField = (field: keyof EditableClient, value: string): void => {
        setState((s) => ({
            ...s,
            editing: { ...s.editing, [field]: value } as EditableClient,
        }));
    };

    const newClient = (): void =>
        setEditing({
            clientId: undefined,
            clientName: "",
            realtorId: -1,
        });
    const contents = (r: LoadingState<readonly Client[]>): JSX.Element => {
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
                            <ModalComp
                                save={(): Promise<void> => save(auth)}
                                cancel={(): void => setEditing(undefined)}
                                client={state.editing}
                                setField={setField}
                            />
                        ) : null}
                        <ClientsTable
                            clients={r.value}
                            deleteItem={(clientId: number): Promise<void> =>
                                deleteClientAndReload(auth, clientId)
                            }
                            setEditing={setEditing}
                        />
                        <Button color="primary" onClick={newClient}>
                            +
                        </Button>
                    </Fragment>
                );
        }
    };

    return (
        <Container>
            <Title>Clients</Title>
            {contents(state.clients)}
        </Container>
    );
};

export default Clients;
