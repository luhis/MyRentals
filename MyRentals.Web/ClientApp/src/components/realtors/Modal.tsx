import { h, FunctionComponent } from "preact";
import { Button, Modal, Field, Label, Input } from "rbx";

import { EditableRealtor } from "../../types/models";
import { OnChange } from "../../types/inputs";

interface Props {
    realtor: EditableRealtor;
    save: () => Promise<void>;
    cancel: () => void;
    setField: (k: Partial<EditableRealtor>) => void;
}

const ModalComp: FunctionComponent<Props> = ({
    save,
    cancel,
    realtor,
    setField,
}) => (
    <Modal active={true}>
        <Modal.Background />
        <Modal.Card>
            <Modal.Card.Head>
                <Modal.Card.Title>
                    {realtor.realtorId === undefined ? "Add" : "Edit"} Realtor
                </Modal.Card.Title>
            </Modal.Card.Head>
            <Modal.Card.Body>
                <Field>
                    <Label>Name</Label>
                    <Input
                        value={realtor.realtorName}
                        onChange={(e: OnChange): void =>
                            setField({ realtorName: e.target.value })
                        }
                    />
                </Field>

                <Field>
                    <Label>Email</Label>
                    <Input
                        value={realtor.realtorEmail}
                        onChange={(e: OnChange): void =>
                            setField({ realtorEmail: e.target.value })
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

export default ModalComp;
