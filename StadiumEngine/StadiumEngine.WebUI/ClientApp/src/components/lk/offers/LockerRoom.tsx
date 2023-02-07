import React, {useEffect, useState} from 'react';
import {getDataTitle, getTitle} from "../../../helpers/utils";
import {useParams} from "react-router-dom";
import {LockerRoomDto} from "../../../models/dto/offers/LockerRoomDto";
import {useInject} from "inversify-hooks";
import {IOffersService} from "../../../services/OffersService";

export const LockerRoom = () => {
    let { id } = useParams();
    const [data, setData] = useState<LockerRoomDto|null>(null);
    
    const [offersService] = useInject<IOffersService>('OffersService');

    const fetchLockerRoom = () => {
        offersService.getLockerRoom(parseInt(id||"0")).then((result: LockerRoomDto) => {
            setData(result);
        })
    }

    useEffect(() => {
        fetchLockerRoom();
    }, [])
    
    useEffect(() => {
        if (data !== null) {
            document.title = getDataTitle(data.name);
        }
        else {
            document.title = getTitle("offers:locker_rooms_tab")
        }
    }, [data])
    
    return (data === null ? <span/> :
        <div>{data.name}</div>);
}