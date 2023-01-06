import React, {useState} from 'react';
import {RolesGrid} from "./RolesGrid";
import {StadiumsGrid} from "./StadiumsGrid";
import {useRecoilValue} from "recoil";
import {permissionsAtom} from "../../../state/permissions";

export const Roles = () => {
    const [selectedRole, setSelectedRole] = useState(null)
    const permissions = useRecoilValue(permissionsAtom);

    const hasGetStadiumsPermission = permissions.filter(p => p.name === 'get-stadiums').length > 0;
    
    return <div className="accounts-container">
        <RolesGrid setSelectedRole={setSelectedRole}/>
        {hasGetStadiumsPermission ? <StadiumsGrid selectedRole={selectedRole}/> : <span/>}
    </div>
}