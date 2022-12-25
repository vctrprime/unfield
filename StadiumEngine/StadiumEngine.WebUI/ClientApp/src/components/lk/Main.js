import React, {useEffect, useState} from 'react';
import {useRecoilValue} from "recoil";
import {stadiumAtom} from "../../state/stadium";
import {useFetchWrapper} from "../../helpers/fetch-wrapper";

export const Main = () => {
    const stadium = useRecoilValue(stadiumAtom);
    const [data, setData] = useState(null);
    
    const fetchWrapper = useFetchWrapper();
    
    /*useEffect(() => {
        loadData();
    }, [stadium])
    
    const loadData = () => {
        fetchWrapper.get({url: 'api/account/stadiums'}).then((result) => {
            setData(JSON.stringify(result))
        })
    }*/
    
    
    return (<span>Основные настройки {data}</span>);
}