import React from 'react';
import {useRecoilValue} from "recoil";
import {permissionsAtom} from "../../../state/permissions";
import {TabData, Tabs} from "../../common/Tabs";
import {ForbiddenMessage} from "../common/ForbiddenMessage";

export const Rates = () => {
    const permissions = useRecoilValue(permissionsAtom);

    const tabs: TabData[] = []

    if (permissions.filter(p => p.name === 'get-price-groups').length > 0) {
        tabs.push({ route: 'price-groups', resourcePath: 'rates:price_groups_tab'});
    }
    
    return ( tabs.length > 0 ?
        <Tabs tabsData={tabs} leftNavRoute={"/lk/rates"}/> :
        <ForbiddenMessage/>)

}