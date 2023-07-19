import React, {useState} from 'react';
import {UsersGrid} from "./UsersGrid";
import {getTitle} from "../../../helpers/utils";
import {StadiumsGrid} from "./StadiumsGrid";
import {useRecoilValue} from "recoil";
import {UserPermissionDto} from "../../../models/dto/accounts/UserPermissionDto";
import {permissionsAtom} from "../../../state/permissions";
import {PermissionsKeys} from "../../../static/PermissionsKeys";

export const Users = () => {
    document.title = getTitle("accounts:users_tab")

    const [selectedUser, setSelectedUser] = useState(null)
    const permissions = useRecoilValue<UserPermissionDto[]>(permissionsAtom);

    const hasGetStadiumsPermission = permissions.filter(p => p.name === PermissionsKeys.GetStadiums).length > 0;
    
    return <div className="accounts-container">
        <UsersGrid setSelectedUser={setSelectedUser} />
        {hasGetStadiumsPermission ? <StadiumsGrid selectedUser={selectedUser}/> : <span/>}
    </div>
}