import {t} from "i18next";

export function getTitle(path: string): string {
    return t(path) + " - Stadium Engine";
}

export const StringFormat = (str: string, ...args: string[]) =>
    str.replace(/{(\d+)}/g, (match, index) => args[index] || '')


export const getDurationText = (duration: number) => {
    let result = '';
    
    const str = duration.toString().replaceAll(",", ".");
    if (str.indexOf(".5") !== -1) {
        result += `${str.split(".")[0]} ${t("common:hour_short")} 30 ${t("common:minute_short")}`;
    }
    else {
        result += `${str.split(".")[0]} ${t("common:hour_short")}`;
    }

    return result;
}