import React from 'react';
import {getTitle} from "../../../helpers/utils";

export const Dashboard = () => {
    document.title = getTitle("common:lk_navbar:dashboard")

    return <span>Дашбоард</span>
}