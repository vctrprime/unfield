import React from 'react';
import {useRecoilValue} from "recoil";
import {stadiumAtom} from "../../state/stadium";
import {t} from "i18next";
import {getTitle} from "../../helpers/utils";

export const Main = () => {
    document.title = getTitle("common:lk_navbar:main_settings")
    
    const stadium = useRecoilValue(stadiumAtom);
    
    return (<span>Основные настройки {stadium}</span>);
}