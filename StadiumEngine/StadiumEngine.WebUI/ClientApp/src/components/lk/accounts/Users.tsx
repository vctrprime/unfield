import React from 'react';
import {UsersGrid} from "./UsersGrid";
import {getTitle} from "../../../helpers/utils";

export const Users = () => {
    document.title = getTitle("accounts:users_tab")
    
    return <div className="accounts-container">
        <UsersGrid/>
    </div>
}