import { h, FunctionComponent } from "preact";

import { Client } from "../../types/models";
import { Table, Button } from "rbx";

interface Props {
    readonly clients: readonly Client[];
    readonly setEditing: (r: Client) => void;
    readonly deleteItem: (r: number) => void;
}

const ClientsTable: FunctionComponent<Props> = ({
    clients,
    setEditing,
    deleteItem,
}) => (
    <Table>
        <Table.Head>
            <Table.Row>
                <Table.Heading>Name</Table.Heading>
                <Table.Heading></Table.Heading>
            </Table.Row>
        </Table.Head>
        <Table.Body>
            {clients.map((client) => (
                <Table.Row key={client.clientId}>
                    <Table.Cell>{client.clientName}</Table.Cell>
                    <Table.Cell>
                        <Button
                            color="primary"
                            onClick={(): void => setEditing(client)}
                        >
                            Edit
                        </Button>
                        <Button
                            color="primary"
                            onClick={(): void => deleteItem(client.clientId)}
                        >
                            X
                        </Button>
                    </Table.Cell>
                </Table.Row>
            ))}
        </Table.Body>
    </Table>
);

export default ClientsTable;
