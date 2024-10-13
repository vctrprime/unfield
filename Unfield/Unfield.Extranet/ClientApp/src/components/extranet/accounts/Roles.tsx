import React, {useState} from 'react';
import {RolesGrid} from "./RolesGrid";
import {StadiumsGrid} from "./StadiumsGrid";
import {useRecoilValue} from "recoil";
import {permissionsAtom} from "../../../state/permissions";
import {UserPermissionDto} from "../../../models/dto/accounts/UserPermissionDto";
import {getTitle} from "../../../helpers/utils";
import {PermissionsKeys} from "../../../static/PermissionsKeys";

export const Roles = () => {
    document.title = getTitle("accounts:roles_tab")

    

    return <div className="accounts-container">
        <RolesGrid />
    </div>
}