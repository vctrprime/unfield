import React from 'react';
import {ForbiddenMessage} from "../common/ForbiddenMessage";
import {getTitle} from "../../helpers/utils";

export const Forbidden = () => {
    document.title = getTitle("errors:accounts:forbidden")

    return(<ForbiddenMessage />)
};