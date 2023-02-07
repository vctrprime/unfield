import React from 'react';
import {getTitle} from "../../../helpers/utils";
import {useRecoilValue} from "recoil";
import {permissionsAtom} from "../../../state/permissions";
import {TabData, Tabs} from "../../common/Tabs";
import {ForbiddenMessage} from "../common/ForbiddenMessage";

export const Offers = () => {
    const permissions = useRecoilValue(permissionsAtom);

    const tabs: TabData[] = []

    if (permissions.filter(p => p.name === 'get-fields').length > 0) {
        tabs.push({ route: 'fields', resourcePath: 'offers:fields_tab'});
    }
    if (permissions.filter(p => p.name === 'get-locker-rooms').length > 0) {
        tabs.push({ route: 'locker-rooms', resourcePath: 'offers:locker_rooms_tab'});
    }

    return ( tabs.length > 0 ?
        <Tabs tabsData={tabs} leftNavRoute={"/lk/offers"}/> :
        <ForbiddenMessage/>)
    
}