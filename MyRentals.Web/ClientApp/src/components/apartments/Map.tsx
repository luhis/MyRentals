import { h, FunctionComponent } from "preact";
import {
    Map as ImportedMap,
    GoogleApiWrapper,
    Marker as ImportedMarker,
    GoogleAPI,
    IMapProps,
} from "google-maps-react";

import { GoogleApiKey } from "../../consts";
import { Apartment } from "../../types/models";

const Map = (ImportedMap as unknown) as FunctionComponent<IMapProps>;
// eslint-disable-next-line @typescript-eslint/no-explicit-any
const Marker = (ImportedMarker as unknown) as FunctionComponent<any>;

const mapStyles = {
    width: "100%",
    height: "100%",
};

const london: LatLng = {
    lat: 51.5074,
    lng: 0.1278,
};

interface Props {
    apartments: readonly Apartment[];
    google: GoogleAPI;
}

interface LatLng {
    lat: number;
    lng: number;
}

const getLocation = (apart: Apartment): LatLng => ({
    lat: apart.lat,
    lng: apart.lon,
});

const MapContainer: FunctionComponent<Props> = ({ apartments, google }) => {
    const centre = apartments.length ? getLocation(apartments[0]) : london;
    return (
        <div>
            <Map google={google} style={mapStyles} center={centre}>
                {apartments.map((a) => (
                    <Marker
                        key={a.apartmentId}
                        position={getLocation(a)}
                        name="Apartment"
                    />
                ))}
            </Map>
        </div>
    );
};

const wrapped: FunctionComponent<Omit<Props, "google">> = GoogleApiWrapper({
    apiKey: GoogleApiKey,
})(MapContainer);

export default wrapped;
