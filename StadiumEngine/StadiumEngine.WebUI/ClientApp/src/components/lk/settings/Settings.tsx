import React from 'react';
import {TabData, Tabs} from "../../common/Tabs";
import {PermissionsKeys} from "../../../static/PermissionsKeys";
import {useRecoilValue} from "recoil";
import {permissionsAtom} from "../../../state/permissions";

export const Settings = () => {
    const permissions = useRecoilValue(permissionsAtom);

    const tabs: TabData[] = []

    tabs.push({route: 'main', resourcePath: 'settings:main_tab'});

    if (permissions.filter(p => p.name === PermissionsKeys.GetBreaks).length > 0) {
        tabs.push({route: 'breaks', resourcePath: 'settings:breaks_tab'});
    }
    
    return <Tabs tabsData={tabs} leftNavRoute={"/lk/settings"}/>;

}