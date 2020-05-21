import { h, FunctionComponent } from "preact";
import Geocode from "react-geocode";
import { Button, Modal, Label, Input, Field } from "rbx";
import { useState } from "preact/hooks";

import { GoogleApiKey } from "../../consts";
import { Coords } from "../../types/models";
import { OnChange } from "../../types/inputs";

Geocode.setApiKey(GoogleApiKey);
Geocode.setLanguage("en");

const getLatLon = async (location: string): Promise<Coords> => {
    const res = await Geocode.fromAddress(location);
    const picked = res.results[0].geometry.location;
    return { lat: picked.lat, lon: picked.lng };
};

interface Props {
    toggleGeoLocation: () => void;
    setLatLon: (c: Coords) => void;
}

const GeoCodeModalComp: FunctionComponent<Props> = ({
    toggleGeoLocation,
    setLatLon,
}) => {
    const [location, setLocation] = useState("");
    const done = async (): Promise<void> => {
        setLatLon(await getLatLon(location));
        toggleGeoLocation();
    };

    return (
        <Modal active={true}>
            <Modal.Background />
            <Modal.Card>
                <Modal.Card.Head>
                    <Modal.Card.Title>GeoCoding</Modal.Card.Title>
                </Modal.Card.Head>
                <Modal.Card.Body>
                    <Field>
                        <Label>Location</Label>
                        <Input
                            value={location}
                            onChange={(e: OnChange): void =>
                                setLocation(e.target.value)
                            }
                        />
                    </Field>
                </Modal.Card.Body>
                <Modal.Card.Foot>
                    <Button onClick={done}>Save changes</Button>
                    <Button onClick={toggleGeoLocation}>Close</Button>
                </Modal.Card.Foot>
            </Modal.Card>
        </Modal>
    );
};

export default GeoCodeModalComp;
