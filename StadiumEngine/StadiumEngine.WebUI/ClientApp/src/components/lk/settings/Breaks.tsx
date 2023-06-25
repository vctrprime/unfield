import React from 'react';
import {getTitle} from "../../../helpers/utils";

export const Breaks = () => {
    document.title = getTitle("settings:breaks_tab")
    
    return <div>Перерывы</div>;
}