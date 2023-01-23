import {t} from "i18next";

export function getTitle(path: string) : string {
    return t(path) + " - Stadium Engine";
}