import React from 'react';
import {getTitle} from "../../../helpers/utils";

export const Reports = () => {
    document.title = getTitle("common:lk_navbar:reports")

    return <span>Отчеты</span>
}