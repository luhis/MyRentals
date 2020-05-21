import { h, FunctionComponent, Fragment } from "preact";
import { Table, Button } from "rbx";

import { Apartment } from "../../types/models";
import { useAccess } from "../app";

interface Props {
    apartments: readonly Apartment[];
    deleteApartment: (apartmentId: number) => void;
    setEditing: (a: Apartment) => void;
}

const ApartmentsTable: FunctionComponent<Props> = ({
    apartments,
    deleteApartment,
    setEditing,
}) => {
    const [{ canEditApartments }] = useAccess();
    return (
        <Table>
            <Table.Head>
                <Table.Row>
                    <Table.Heading>Name</Table.Heading>
                    <Table.Heading>Description</Table.Heading>
                    <Table.Heading>Floor Area</Table.Heading>
                    <Table.Heading>Price</Table.Heading>
                    <Table.Heading>Number of Rooms</Table.Heading>
                    <Table.Heading></Table.Heading>
                </Table.Row>
            </Table.Head>
            <Table.Body>
                {apartments.map((apartment) => (
                    <Table.Row key={apartment.apartmentId}>
                        <Table.Cell>{apartment.name}</Table.Cell>
                        <Table.Cell>{apartment.description}</Table.Cell>
                        <Table.Cell>{apartment.floorArea}</Table.Cell>
                        <Table.Cell>{apartment.pricePerMonth}</Table.Cell>
                        <Table.Cell>{apartment.numberOfRooms}</Table.Cell>
                        <Table.Cell>
                            {canEditApartments ? (
                                <Fragment>
                                    <Button
                                        className="btn btn-primary mr-1"
                                        onClick={(): void =>
                                            setEditing(apartment)
                                        }
                                    >
                                        Edit
                                    </Button>
                                    <Button
                                        className="btn btn-primary"
                                        onClick={(): void =>
                                            deleteApartment(
                                                apartment.apartmentId
                                            )
                                        }
                                    >
                                        X
                                    </Button>
                                </Fragment>
                            ) : null}
                        </Table.Cell>
                    </Table.Row>
                ))}
            </Table.Body>
        </Table>
    );
};

export default ApartmentsTable;
