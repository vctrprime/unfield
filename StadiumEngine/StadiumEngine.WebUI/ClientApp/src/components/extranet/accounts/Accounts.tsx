import React from 'react';
import {TabData, Tabs} from "../../common/Tabs";
import {useRecoilValue} from "recoil";
import {permissionsAtom} from "../../../state/permissions";
import {ForbiddenMessage} from "../common/ForbiddenMessage";
import {PermissionsKeys} from "../../../static/PermissionsKeys";


export const Accounts = () => {
    const permissions = useRecoilValue(permissionsAtom);

    const tabs: TabData[] = []


    if (permissions.filter(p => p.name === PermissionsKeys.GetUsers).length > 0) {
        tabs.push({route: 'users', resourcePath: 'accounts:users_tab'});
    }
    if (permissions.filter(p => p.name === PermissionsKeys.GetRoles).length > 0) {
        tabs.push({route: 'roles', resourcePath: 'accounts:roles_tab'});
    }
    if (permissions.filter(p => p.name === PermissionsKeys.GetPermissions).length > 0) {
        tabs.push({route: 'permissions', resourcePath: 'accounts:permissions_tab'});
    }

    return (tabs.length > 0 ?
        <Tabs tabsData={tabs} leftNavRoute={"/accounts"}/> :
        <ForbiddenMessage/>)
};
