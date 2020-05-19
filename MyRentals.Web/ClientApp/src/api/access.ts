import { Access } from "../types/models";
import { throwIfNotOk } from "./api";

export const getAccess = async (token: string | undefined): Promise<Access> => {
    const response = await fetch("access", {
        headers: { Authorization: `Bearer ${token}` },
    });
    throwIfNotOk(response);
    return await response.json();
};
