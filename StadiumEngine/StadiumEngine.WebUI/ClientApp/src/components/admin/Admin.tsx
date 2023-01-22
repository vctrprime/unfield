import React from 'react';
import {TabData, Tabs} from "../common/Tabs";
import {useRecoilValue} from "recoil";
import {AuthorizeUserDto} from "../../models/dto/accounts/AuthorizeUserDto";
import {authAtom} from "../../state/auth";
import {ForbiddenMessage} from "../lk/common/ForbiddenMessage";

export const Admin = () => {
    const auth = useRecoilValue<AuthorizeUserDto | null>(authAtom);
    
    if (auth === null || !auth.isAdmin) return <ForbiddenMessage/>
    
    const tabs: TabData[] = [
        {route: "", resourcePath: "admin:legals_tab"}
    ]
    
    return ( <Tabs tabsData={tabs} leftNavRoute={"/admin"}/>)
}