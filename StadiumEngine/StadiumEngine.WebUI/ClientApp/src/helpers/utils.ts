import {t} from "i18next";
import {FieldDto} from "../models/dto/offers/FieldDto";

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

export function getFieldBasicFormData(data: FieldDto) : FormData {
    const form = new FormData();
    if (data.id !== undefined) {
        form.append("id", data.id.toString());
    }
    form.append("name", data.name);
    form.append("description", data.description === null || data.description === undefined ? "" : data.description);
    form.append("width", data.width.toString());
    form.append("length", data.length.toString());
    form.append("isActive", data.isActive.toString());
    form.append("parentFieldId", data.parentFieldId === null ? "" : data.parentFieldId.toString());
    form.append("coveringType", data.coveringType.toString());
    for (let i = 0; i < data.sportKinds.length; i++) {
        form.append('sportKinds['+i+']', data.sportKinds[i].toString());
    }
   
    return form;
}