import {
    Filters,
    Apartment,
    EditableApartment,
    ApiResponse,
    toApiResponse,
} from "../types/models";
import { throwIfNotOk } from "./api";

export const getApartments = async (
    token: string | undefined,
    filters: Filters
): Promise<ApiResponse<readonly Apartment[]>> =>
    toApiResponse(async () => {
        const params = new URLSearchParams(
            Object.entries(filters).filter(([_, val]) => val !== null)
        ).toString();
        const response = await fetch(`apartment?${params}`, {
            headers: { Authorization: token ? `Bearer ${token}` : "" },
        });
        throwIfNotOk(response);
        return (await response.json()) as readonly Apartment[];
    });

export const deleteApartment = async (
    token: string | undefined,
    apartmentId: number
): Promise<void> => {
    const response = await fetch(`apartment/${apartmentId}`, {
        method: "delete",
        headers: { Authorization: token ? `Bearer ${token}` : "" },
    });
    throwIfNotOk(response);
};

export const putApartment = async (
    token: string | undefined,
    apartment: EditableApartment
): Promise<void> => {
    const response = await fetch(
        `apartment/${apartment.apartmentId ? apartment.apartmentId : -1}`,
        {
            method: "put",
            body: JSON.stringify(apartment),
            headers: {
                "Content-Type": "application/json",
                Authorization: token ? `Bearer ${token}` : "",
            },
        }
    );
    throwIfNotOk(response);
};

export const postApartment = async (
    token: string | undefined,
    apartment: EditableApartment
): Promise<void> => {
    const response = await fetch(`apartment/`, {
        method: "post",
        body: JSON.stringify(apartment),
        headers: {
            "Content-Type": "application/json",
            Authorization: token ? `Bearer ${token}` : "",
        },
    });
    throwIfNotOk(response);
};
