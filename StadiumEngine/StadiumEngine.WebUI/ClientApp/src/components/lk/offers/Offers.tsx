import React from 'react';
import {getTitle} from "../../../helpers/utils";
import {useRecoilValue} from "recoil";
import {permissionsAtom} from "../../../state/permissions";
import {TabData, Tabs} from "../../common/Tabs";
import {ForbiddenMessage} from "../common/ForbiddenMessage";
import {PermissionsKeys} from "../../../static/PermissionsKeys";
import {Ge} from "react-flags-select";

export const Offers = () => {
    const permissions = useRecoilValue(permissionsAtom);

    const tabs: TabData[] = []

    if (permissions.filter(p => p.name === PermissionsKeys.GetFields).length > 0) {
        tabs.push({ route: 'fields', resourcePath: 'offers:fields_tab'});
    }
    if (permissions.filter(p => p.name === PermissionsKeys.GetLockerRooms).length > 0) {
        tabs.push({ route: 'locker-rooms', resourcePath: 'offers:locker_rooms_tab'});
    }
    if (permissions.filter(p => p.name === PermissionsKeys.GetInventories).length > 0) {
        tabs.push({ route: 'inventories', resourcePath: 'offers:inventories_tab'});
    }

    return ( tabs.length > 0 ?
        <Tabs tabsData={tabs} leftNavRoute={"/lk/offers"}/> :
        <ForbiddenMessage/>)
    
}