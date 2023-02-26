import {SportKind} from "../../../models/dto/offers/enums/SportKind";
import {t} from "i18next";
import React from "react";

export const SportKindsRenderer = (obj : any) => {
    const kinds = obj.data.sportKinds;

    const textKinds = kinds.map((k: SportKind) => {
        const value = SportKind[k];
        return t("offers:sports:" + value.toLowerCase());
    })

    return <span>{textKinds.join('; ')}</span>;
}
