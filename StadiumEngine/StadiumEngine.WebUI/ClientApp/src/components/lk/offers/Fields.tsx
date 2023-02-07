import React, {useState} from 'react';
import {getTitle} from "../../../helpers/utils";

export const Fields = () => {
    document.title = getTitle("offers:fields_tab")

    return (<div>Площадки</div>);
}