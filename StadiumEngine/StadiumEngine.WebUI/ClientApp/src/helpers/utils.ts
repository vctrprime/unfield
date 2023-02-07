import {t} from "i18next";

export function getTitle(path: string) : string {
    return t(path) + " - Stadium Engine";
}

export function getDataTitle(name: string) : string {
    return name + " - Stadium Engine";
}

export const getOverlayNoRowsTemplate = () => {
    return '<span />';
}

export const StringFormat = (str: string, ...args: string[]) =>
    str.replace(/{(\d+)}/g, (match, index) => args[index] || '')