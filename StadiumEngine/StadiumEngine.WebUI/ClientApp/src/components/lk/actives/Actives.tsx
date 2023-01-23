import React from 'react';
import {getTitle} from "../../../helpers/utils";

export const Actives = () => {
    document.title = getTitle("common:lk_navbar:actives")
    
    return <span>Активы</span>
}