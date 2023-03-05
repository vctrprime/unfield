import React from 'react';
import {getTitle} from "../../../helpers/utils";

export const Schedule = () => {
    document.title = getTitle("common:lk_navbar:schedule")

    return (<span>Расписание</span>);
}