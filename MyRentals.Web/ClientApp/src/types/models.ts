export interface Apartment {
    readonly apartmentId: number;
    readonly name: string;
    readonly description: string;
    readonly floorArea: number;
    readonly pricePerMonth: number;
    readonly numberOfRooms: number;
    readonly lat: number;
    readonly lon: number;
    readonly realtorId: number;
    readonly isRented: boolean;
}

export interface Realtor {
    readonly realtorName: string;
    readonly realtorId: number;
    readonly realtorEmail: string;
}

export interface Client {
    readonly realtorId: number;
    readonly clientName: string;
    readonly clientId: number;
}

export interface Coords {
    readonly lat: number;
    readonly lon: number;
}

export interface Filters {
    readonly floorArea: string | number;
    readonly pricePerMonth: string | number;
    readonly numberOfRooms: string | number;
}

export type EditableClient = Omit<Client, "clientId"> & {
    readonly clientId: number | undefined;
};

export type EditableRealtor = Omit<Realtor, "realtorId"> & {
    readonly realtorId: number | undefined;
};

export type EditableApartment = Omit<Apartment, "apartmentId"> & {
    readonly apartmentId: number | undefined;
};

export type ApiResponse<T> =
    | { tag: "Loaded"; value: T }
    | { tag: "Error"; value: string };

export type LoadingState<T> = ApiResponse<T> | { tag: "Loading" };

export const valueOr = <T, TT>(
    state: LoadingState<T>,
    map: (a: T) => TT,
    defaultValue: TT
): TT => {
    switch (state.tag) {
        case "Loaded":
            return map(state.value);
        default:
            return defaultValue;
    }
};

export const toApiResponse = async <T>(
    f: () => Promise<T>
): Promise<ApiResponse<T>> => {
    try {
        return { tag: "Loaded", value: await f() };
    } catch (e) {
        return { tag: "Error", value: e.toString() };
    }
};
