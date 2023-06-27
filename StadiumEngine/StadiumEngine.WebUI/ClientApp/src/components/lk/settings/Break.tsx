import React, {useEffect, useState} from 'react';
import {useParams} from "react-router-dom";
import {getDataTitle, getTitle} from "../../../helpers/utils";
import {BreakDto} from "../../../models/dto/settings/BreakDto";
import {useInject} from "inversify-hooks";
import {ISettingsService} from "../../../services/SettingsService";

export const Break = () => {
    let {id} = useParams();

    const [data, setData] = useState<BreakDto>({
        isActive: true,
        dateStart: new Date()
    } as BreakDto);
    const [isError, setIsError] = useState<boolean>(false);
    const [breakId, setBreakId] = useState(parseInt(id || "0"))

    const [settingsService] = useInject<ISettingsService>('SettingsService');

    const fetchBreak = () => {
        if (breakId > 0) {
            settingsService.getBreak(breakId).then((result: BreakDto) => {
                setData(result);
            }).catch(() => setIsError(true));
        }
    }
    useEffect(() => {
        fetchBreak();
    }, [breakId])

    useEffect(() => {
        setBreakId(parseInt(id || "0"));
    }, [id])
    
    useEffect(() => {
        if (data.name !== undefined && data.name !== null) {
            document.title = getDataTitle(data.name);
        } else {
            document.title = getTitle("settings:breaks_tab")
        }
    }, [data])

    return isError ? <span/> : (<div>{data.name}</div>)
}
