import {
    Realtor,
    EditableRealtor,
    toApiResponse,
    ApiResponse,
} from "../types/models";
import { throwIfNotOk } from "./api";

export const getRealtors = async (
    token: string | undefined
): Promise<ApiResponse<readonly Realtor[]>> =>
    toApiResponse(async () => {
        const response = await fetch("realtor", {
            headers: { Authorization: `Bearer ${token}` },
        });

        throwIfNotOk(response);
        return await response.json();
    });

export const deleteRealtor = async (
    token: string | undefined,
    realtorId: number
): Promise<void> => {
    const response = await fetch(`realtor/${realtorId}`, {
        method: "delete",
        headers: { Authorization: `Bearer ${token}` },
    });
    throwIfNotOk(response);
};

export const putRealtor = async (
    token: string | undefined,
    realtor: EditableRealtor
): Promise<void> => {
    const response = await fetch(`realtor/${realtor.realtorId}`, {
        method: "put",
        body: JSON.stringify(realtor),
        headers: {
            "Content-Type": "application/json",
            Authorization: `Bearer ${token}`,
        },
    });
    throwIfNotOk(response);
};

export const postRealtor = async (
    token: string | undefined,
    realtor: EditableRealtor
): Promise<void> => {
    const response = await fetch(`realtor/`, {
        method: "post",
        body: JSON.stringify(realtor),
        headers: {
            "Content-Type": "application/json",
            Authorization: `Bearer ${token}`,
        },
    });
    throwIfNotOk(response);
};
