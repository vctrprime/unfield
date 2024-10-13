import React from 'react';
import {TabData, Tabs} from "../../common/Tabs";
import {PermissionsKeys} from "../../../static/PermissionsKeys";
import {useRecoilValue} from "recoil";
import {permissionsAtom} from "../../../state/permissions";
import {ForbiddenMessage} from "../common/ForbiddenMessage";

export const Settings = () => {
    const permissions = useRecoilValue(permissionsAtom);

    const tabs: TabData[] = []

    if (permissions.filter(p => p.name === PermissionsKeys.UpdateMainSettings || 1 == 1).length > 0) {
        tabs.push({route: 'main', resourcePath: 'settings:main_tab'});
    }
    
    if (permissions.filter(p => p.name === PermissionsKeys.GetBreaks).length > 0) {
        tabs.push({route: 'breaks', resourcePath: 'settings:breaks_tab'});
    }
    
    return (tabs.length > 0 ?
        <Tabs tabsData={tabs} leftNavRoute={"/settings"}/> :
        <ForbiddenMessage/>)

}