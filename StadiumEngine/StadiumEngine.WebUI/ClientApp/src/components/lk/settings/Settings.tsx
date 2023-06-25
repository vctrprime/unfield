import React from 'react';
import {TabData, Tabs} from "../../common/Tabs";

export const Settings = () => {
    const tabs: TabData[] = []

    tabs.push({route: 'main', resourcePath: 'settings:main_tab'});
    tabs.push({route: 'breaks', resourcePath: 'settings:breaks_tab'});
    
    return <Tabs tabsData={tabs} leftNavRoute={"/lk/settings"}/>;

}