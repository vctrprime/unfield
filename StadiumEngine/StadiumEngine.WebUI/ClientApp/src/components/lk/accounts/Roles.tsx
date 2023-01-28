import React, {useState} from 'react';
import {RolesGrid} from "./RolesGrid";
import {StadiumsGrid} from "./StadiumsGrid";
import {useRecoilValue} from "recoil";
import {permissionsAtom} from "../../../state/permissions";
import {UserPermissionDto} from "../../../models/dto/accounts/UserPermissionDto";
import {getTitle} from "../../../helpers/utils";

export const Roles = () => {
    document.title = getTitle("accounts:roles_tab")
    
    const [selectedRole, setSelectedRole] = useState(null)
    const permissions = useRecoilValue<UserPermissionDto[]>(permissionsAtom);

    const hasGetStadiumsPermission = permissions.filter(p => p.name === 'get-stadiums').length > 0;
    
    return <div className="accounts-container">
        <RolesGrid selectedRole={selectedRole} setSelectedRole={setSelectedRole}/>
        {hasGetStadiumsPermission ? <StadiumsGrid selectedRole={selectedRole}/> : <span/>}
    </div>
}