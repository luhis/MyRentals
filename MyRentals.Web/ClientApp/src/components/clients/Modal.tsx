import { h, FunctionComponent } from "preact";
import { Modal, Button, Field, Label, Input } from "rbx";

import { EditableClient } from "../../types/models";
import { OnChange } from "../../types/inputs";

interface Props {
    save: () => void;
    cancel: () => void;
    setField: (s: keyof EditableClient, val: string) => void;
    client: EditableClient;
}

const ModalX: FunctionComponent<Props> = ({
    save,
    cancel,
    client,
    setField,
}) => (
    <Modal active={true}>
        <Modal.Background />
        <Modal.Card>
            <Modal.Card.Head>
                <Modal.Card.Title>
                    {client.realtorId === undefined ? "Add" : "Edit"} Client
                </Modal.Card.Title>
            </Modal.Card.Head>
            <Modal.Card.Body>
                <Field>
                    <Label>Name</Label>
                    <Input
                        value={client.clientName}
                        onChange={(e: OnChange): void =>
                            setField("clientName", e.target.value)
                        }
                    />
                </Field>
            </Modal.Card.Body>
            <Modal.Card.Foot>
                <Button onClick={save}>Save changes</Button>
                <Button onClick={cancel}>Close</Button>
            </Modal.Card.Foot>
        </Modal.Card>
    </Modal>
);

export default ModalX;
