import { h, FunctionComponent } from "preact";
import { Realtor } from "../../types/models";
import { Button, Table } from "rbx";

interface Props {
    realtors: readonly Realtor[];
    setEditing: (r: Realtor) => void;
    deleteItem: (r: number) => void;
}

const RealtorsTable: FunctionComponent<Props> = ({
    realtors,
    setEditing,
    deleteItem,
}) => (
    <Table>
        <Table.Head>
            <tr>
                <Table.Heading>Name</Table.Heading>
                <Table.Heading></Table.Heading>
            </tr>
        </Table.Head>
        <Table.Body>
            {realtors.map((realtor) => (
                <Table.Row key={realtor.realtorId}>
                    <Table.Cell>{realtor.realtorName}</Table.Cell>
                    <Table.Cell>
                        <Button onClick={(): void => setEditing(realtor)}>
                            Edit
                        </Button>
                        <Button
                            onClick={(): void => deleteItem(realtor.realtorId)}
                        >
                            X
                        </Button>
                    </Table.Cell>
                </Table.Row>
            ))}
        </Table.Body>
    </Table>
);

export default RealtorsTable;
