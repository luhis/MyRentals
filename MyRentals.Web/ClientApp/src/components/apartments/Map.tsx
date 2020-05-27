import { h, FunctionComponent } from "preact";
import GoogleMapReact from "google-map-react";

import { GoogleApiKey } from "../../consts";
import { Apartment } from "../../types/models";

const london: LatLng = {
    lat: 51.5074,
    lng: 0.1278,
};

interface Props {
    apartments: readonly Apartment[];
}

interface LatLng {
    lat: number;
    lng: number;
}

const getLocation = (apart: Apartment): LatLng => ({
    lat: apart.lat,
    lng: apart.lon,
});

const MapContainer: FunctionComponent<Props> = ({ apartments }) => {
    const centre = apartments.length ? getLocation(apartments[0]) : london;
    return (
        <div style={{ height: "100vh", width: "100%" }}>
            <GoogleMapReact
                bootstrapURLKeys={{ key: GoogleApiKey }}
                defaultCenter={centre}
                defaultZoom={12}
            >
                {apartments.map((a) => (
                    <Marker
                        key={a.apartmentId}
                        lat={a.lat}
                        lng={a.lon}
                        text="Apartment"
                    />
                ))}
            </GoogleMapReact>
        </div>
    );
};

const Marker: FunctionComponent<{ lat: number; lng: number; text: string }> = (
    props
) => <span {...props} />;

export default MapContainer;
