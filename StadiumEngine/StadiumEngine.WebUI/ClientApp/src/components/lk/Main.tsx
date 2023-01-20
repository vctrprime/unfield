import React from 'react';
import {useRecoilValue} from "recoil";
import {stadiumAtom} from "../../state/stadium";

export const Main = () => {
    const stadium = useRecoilValue(stadiumAtom);
    
    return (<span>Основные настройки {stadium}</span>);
}