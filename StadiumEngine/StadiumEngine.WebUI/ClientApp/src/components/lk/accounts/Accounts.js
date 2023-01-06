import React from 'react';
import {Tabs} from "../common/Tabs";
import {useRecoilValue} from "recoil";
import {permissionsAtom} from "../../../state/permissions";
import {ForbiddenMessage} from "../common/ForbiddenMessage";


export const Accounts = () => {
    const permissions = useRecoilValue(permissionsAtom);
    
    const tabs = []
    
    if (permissions.filter(p => p.name === 'get-users').length > 0) {
        tabs.push({ route: '', name: 'Пользователи'});
    }
    if (permissions.filter(p => p.name === 'get-roles').length > 0) {
        tabs.push({ route: 'roles', name: 'Роли'});
    }
    if (permissions.filter(p => p.name === 'get-permissions').length > 0) {
        tabs.push({ route: 'permissions', name: 'Разрешения'});
    }
    
    return ( tabs.length > 0 ?
        <Tabs tabsData={tabs} leftNavRoute={"/lk/accounts"}/> : 
        <ForbiddenMessage/>)
};
