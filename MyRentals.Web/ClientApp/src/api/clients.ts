import { throwIfNotOk } from "./api";
import {
    Client,
    EditableClient,
    toApiResponse,
    ApiResponse,
} from "../types/models";

export const getClients = async (
    token: string | undefined
): Promise<ApiResponse<readonly Client[]>> =>
    toApiResponse(async () => {
        const response = await fetch("client", {
            headers: { Authorization: `Bearer ${token}` },
        });
        throwIfNotOk(response);
        return await response.json();
    });

export const deleteClient = async (
    token: string | undefined,
    clientId: number
): Promise<void> => {
    const response = await fetch(`client/${clientId}`, {
        method: "delete",
        headers: { Authorization: `Bearer ${token}` },
    });
    throwIfNotOk(response);
};

export const putClient = async (
    token: string | undefined,
    client: EditableClient
): Promise<void> => {
    const response = await fetch(`client/${client.clientId}`, {
        method: "put",
        body: JSON.stringify(client),
        headers: {
            "Content-Type": "application/json",
            Authorization: `Bearer ${token}`,
        },
    });
    throwIfNotOk(response);
};

export const postClient = async (
    token: string | undefined,
    client: EditableClient
): Promise<void> => {
    const response = await fetch(`client/`, {
        method: "post",
        body: JSON.stringify(client),
        headers: {
            "Content-Type": "application/json",
            Authorization: `Bearer ${token}`,
        },
    });
    throwIfNotOk(response);
};
