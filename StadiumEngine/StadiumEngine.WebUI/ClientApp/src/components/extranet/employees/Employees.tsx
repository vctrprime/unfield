import React from 'react';
import {getTitle} from "../../../helpers/utils";

export const Employees = () => {
    document.title = getTitle("common:lk_navbar:employees")

    return <span>Персонал</span>
}