import { h, FunctionComponent } from "preact";
import {
    Modal,
    Button,
    Label,
    Input,
    Checkbox,
    Field,
    Select,
    Column,
} from "rbx";

import { Realtor, EditableApartment } from "../../types/models";
import { OnChange, OnChangeCheck } from "../../types/inputs";

const stringOrFloat = (s: string): string | number => {
    const pattern = /([-])?([0-9]+)/g;
    const matches = s.match(pattern);
    if (matches) {
        return parseFloat(s);
    } else {
        return s;
    }
};

interface Props {
    apartment: EditableApartment;
    realtors: readonly Realtor[];
    save: () => Promise<void>;
    cancel: () => void;
    setField: (k: Partial<EditableApartment>) => void;
    toggleGeoLocation: () => void;
}

const ModalX: FunctionComponent<Props> = ({
    save,
    cancel,
    apartment,
    realtors,
    setField,
    toggleGeoLocation,
}) => {
    return (
        <Modal active={true}>
            <Modal.Background />
            <Modal.Card>
                <Modal.Card.Head>
                    {apartment.apartmentId === undefined ? "Add" : "Edit"}{" "}
                    Apartment
                </Modal.Card.Head>
                <Modal.Card.Body>
                    <Field>
                        <Label>Name</Label>
                        <Input
                            value={apartment.name}
                            onChange={(e: OnChange): void =>
                                setField({ name: e.target.value })
                            }
                        />
                    </Field>
                    <Field>
                        <Label>Description</Label>
                        <Input
                            value={apartment.description}
                            onChange={(e: OnChange): void =>
                                setField({ description: e.target.value })
                            }
                        />
                    </Field>
                    <Field>
                        <Label>Floor Area</Label>
                        <Input
                            value={apartment.floorArea}
                            type="number"
                            onChange={(e: OnChange): void =>
                                setField({
                                    floorArea: parseInt(e.target.value),
                                })
                            }
                        />
                    </Field>
                    <Field>
                        <Label>Price per Month</Label>
                        <Input
                            value={apartment.pricePerMonth}
                            type="number"
                            onChange={(e: OnChange): void =>
                                setField({
                                    pricePerMonth: parseInt(e.target.value),
                                })
                            }
                        />
                    </Field>
                    <Field>
                        <Label>Number of Rooms</Label>
                        <Input
                            value={apartment.numberOfRooms}
                            type="number"
                            pattern="[0-9]*"
                            onChange={(e: OnChange): void =>
                                setField({
                                    numberOfRooms: parseInt(e.target.value),
                                })
                            }
                        />
                    </Field>
                    {realtors.length > 1 ? (
                        <Field>
                            <Label>Realtor</Label>
                            <Select.Container>
                                <Select
                                    value={apartment.realtorId}
                                    onChange={(e: OnChange): void =>
                                        setField({
                                            realtorId: parseInt(e.target.value),
                                        })
                                    }
                                >
                                    {realtors.map((a) => (
                                        <Select.Option
                                            key={a.realtorId}
                                            value={a.realtorId}
                                        >
                                            {a.realtorName}
                                        </Select.Option>
                                    ))}
                                </Select>
                            </Select.Container>
                        </Field>
                    ) : null}
                    <Field>
                        <Label>
                            <Checkbox
                                checked={apartment.isRented}
                                onChange={(e: OnChangeCheck): void =>
                                    setField({ isRented: e.target.checked })
                                }
                            />
                            Is Rented
                        </Label>
                    </Field>
                    <Column.Group>
                        <Column>
                            <Button color="primary" onClick={toggleGeoLocation}>
                                Lookup Location
                            </Button>
                        </Column>
                        <Column>
                            <Field>
                                <Label>Latitude</Label>
                                <Input
                                    value={apartment.lat}
                                    type="number"
                                    pattern="([-])?[0-9]*"
                                    onChange={(e: OnChange): void =>
                                        setField({
                                            lat: stringOrFloat(e.target.value),
                                        })
                                    }
                                />
                            </Field>
                        </Column>
                        <Column>
                            <Field>
                                <Label>Longitude</Label>
                                <Input
                                    value={apartment.lon}
                                    type="number"
                                    pattern="([-])?[0-9]*"
                                    onChange={(e: OnChange): void =>
                                        setField({
                                            lon: stringOrFloat(e.target.value),
                                        })
                                    }
                                />
                            </Field>
                        </Column>
                    </Column.Group>
                </Modal.Card.Body>
                <Modal.Card.Foot>
                    <Button color="primary" onClick={save}>
                        Save changes
                    </Button>
                    <Button color="secondary" onClick={cancel}>
                        Close
                    </Button>
                </Modal.Card.Foot>
            </Modal.Card>
        </Modal>
    );
};

export default ModalX;
