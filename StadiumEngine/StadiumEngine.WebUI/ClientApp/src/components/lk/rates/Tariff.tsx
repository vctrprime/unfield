import React, {useEffect, useRef, useState} from 'react';
import {useNavigate, useParams} from "react-router-dom";
import {TariffDto} from "../../../models/dto/rates/TariffDto";
import {useInject} from "inversify-hooks";
import {IRatesService} from "../../../services/RatesService";
import {getDataTitle, getTitle, StringFormat, validateInputs} from "../../../helpers/utils";
import {ActionButtons} from "../../common/actions/ActionButtons";
import {PermissionsKeys} from "../../../static/PermissionsKeys";
import {t} from "i18next";
import {Button, Checkbox, Form} from "semantic-ui-react";
import {DateRangeSelect} from "../common/DateRangeSelect";
import {TariffInterval} from "./TariffInterval";
import {AddTariffCommand} from "../../../models/command/rates/AddTariffCommand";
import {UpdateTariffCommand} from "../../../models/command/rates/UpdateTariffCommand";
import {ISettingsService} from "../../../services/SettingsService";


export const Tariff = () => {
    let {id} = useParams();

    const [data, setData] = useState<TariffDto>({
        isActive: true,
        dateStart: new Date()
    } as TariffDto);
    const [isError, setIsError] = useState<boolean>(false);
    const [tariffId, setTariffId] = useState(parseInt(id || "0"))
    
    const [intervalPoints, setIntervalPoints] = useState<string[]>([]);
    
    const [ratesService] = useInject<IRatesService>('RatesService');
    const [settingsService] = useInject<ISettingsService>('SettingsService');

    const navigate = useNavigate();

    const fetchTariff = () => {
        if (tariffId > 0) {
            ratesService.getTariff(tariffId).then((result: TariffDto) => {
                setData(result);
            }).catch(() => setIsError(true));
        }
    }
    
    useEffect(() => {
        settingsService.getStadiumMainSettings().then((result) => {
            const points: string[] = [];
            for (let i = result.openTime; i <= result.closeTime; i++) {
                if (i === result.closeTime) {
                    points.push(i.toString().length === 1 ? `0${i}:00`: `${i}:00`);
                }
                else {
                    points.push(i.toString().length === 1 ? `0${i}:00`: `${i}:00`);
                    points.push(i.toString().length === 1 ? `0${i}:30`: `${i}:30`);
                }
            }
            setIntervalPoints(points);
        })
    }, [])

    useEffect(() => {
        fetchTariff();
    }, [tariffId])

    useEffect(() => {
        setTariffId(parseInt(id || "0"));
    }, [id])

    useEffect(() => {
        if (data.name !== undefined && data.name !== null) {
            document.title = getDataTitle(data.name);
        } else {
            document.title = getTitle("rates:tariffs_tab")
        }
    }, [data])

    const nameInput = useRef<any>();
    const descriptionInput = useRef<any>();


    const saveTariff = () => {
        if (validateInputs([nameInput])) {
            const command: AddTariffCommand = {
                name: nameInput.current?.value,
                description: descriptionInput.current?.value,
                isActive: data.isActive,
                dateStart: data.dateStart,
                dateEnd: data.dateEnd,
                monday: data.monday,
                tuesday: data.tuesday,
                wednesday: data.wednesday,
                thursday: data.thursday,
                friday: data.friday,
                saturday: data.saturday,
                sunday: data.sunday,
                dayIntervals: data.dayIntervals
            }
            ratesService.addTariff(command).then(() => {
                navigate("/lk/rates/tariffs");
            })
        }
    }

    const updateTariff = () => {
        if (validateInputs([nameInput])) {
            const command: UpdateTariffCommand = {
                id: tariffId,
                name: nameInput.current?.value,
                description: descriptionInput.current?.value,
                isActive: data.isActive,
                dateStart: data.dateStart,
                dateEnd: data.dateEnd,
                monday: data.monday,
                tuesday: data.tuesday,
                wednesday: data.wednesday,
                thursday: data.thursday,
                friday: data.friday,
                saturday: data.saturday,
                sunday: data.sunday,
                dayIntervals: data.dayIntervals
            }
            ratesService.updateTariff(command).then(() => {
                navigate("/lk/rates/tariffs");
            })
        }
    }

    const deleteTariff = () => {
        ratesService.deleteTariff(tariffId).then(() => {
            navigate("/lk/rates/tariffs");
        })
    }

    const changeIsActive = () => {
        setData({
            ...data,
            isActive: !data.isActive
        });
    }

    const toggleDay = (day: string) => {
        setData({
            ...data,
            [day]: !(data[day as keyof TariffDto] as boolean)
        });
    }
    
    const periodValue = () => {
        let result = [];
        if (data?.dateEnd) {
            result = [new Date(data?.dateStart), new Date(data?.dateEnd)]
        }
        else {
            result = [new Date(data?.dateStart)];
        }
        return result;
    }
    const changePeriod = (event: any, dates: any) => {
        let startDate = new Date();
        if (dates.value !== null && dates.value.length > 0) {
            startDate = dates.value[0];
        }
        let endDate = null;
        if (dates.value !== null && dates.value.length > 1) {
            endDate = dates.value[1]
        }
        
        setData({
            ...data,
            dateStart: startDate,
            dateEnd: endDate
        });
    }
    
    const setInterval = (start: string, end: string, interval: string[]|null = null, isRemove: boolean = false ) => {
        const newIntervals: string[][] = [];
        data?.dayIntervals?.forEach((i) => {
            if (i !== interval) {
                newIntervals.push(i);
            }
            else {
                if (!isRemove) {
                    newIntervals.push([start, end]);
                }
            }
        });
        
        if (interval === null) {
            newIntervals.push([start, end]);
        }
        
        setData({
            ...data,
            dayIntervals: newIntervals
        });
    }
    
    return isError ? <span/> : (<div>
        <ActionButtons
            savePermission={id === "new" ? PermissionsKeys.InsertTariff : PermissionsKeys.UpdateTariff}
            deletePermission={PermissionsKeys.DeleteTariff}
            title={id === "new" ? t('rates:tariffs_grid:adding') : t('rates:tariffs_grid:editing')}
            saveAction={id === "new" ? saveTariff : updateTariff}
            deleteAction={id === "new" ? null : deleteTariff}
            deleteHeader={id === "new" ? null : t('rates:tariffs_grid:delete:header')}
            deleteQuestion={id === "new" ? null : StringFormat(t('rates:tariffs_grid:delete:question'), data?.name || '')}
        />
        <Form className="tariff-form">
            <Form.Field style={{marginBottom: 0}}>
                <label>{t("rates:tariffs_grid:is_active")}</label>
                <Checkbox toggle checked={data.isActive} onChange={() => changeIsActive()}/>
            </Form.Field>
            <Form.Field>
                <label>{t("rates:tariffs_grid:name")}</label>
                <input id="name-input" ref={nameInput} placeholder={t("rates:tariffs_grid:name") || ''}
                       defaultValue={data?.name || ''}/>
            </Form.Field>
            <Form.Field>
                <label>{t("rates:tariffs_grid:description")}</label>
                <textarea id="description-input" ref={descriptionInput} rows={4}
                          placeholder={t("rates:tariffs_grid:description") || ''}
                          defaultValue={data?.description || ''}/>
            </Form.Field>
            <Form.Field>
                <label>{t("rates:tariffs_grid:period")}</label>
                <DateRangeSelect value={periodValue()} onChange={changePeriod}/>
            </Form.Field>
            <Form.Field>
                <label>{t("rates:tariffs_grid:days")}</label>
                {['monday', 'tuesday', 'wednesday', 'thursday', 'friday', 'saturday', 'sunday'].map((day) => {
                    return <Checkbox
                        key={day}
                        checked={data[day as keyof TariffDto] as boolean}
                        onChange={(e, data) => toggleDay(day)}
                        label={t(`rates:tariffs_grid:${day}`)}/>
                })}
            </Form.Field>
            <Form.Field>
                <label>{t("rates:tariffs_grid:intervals")}</label>
                <div style={{fontSize: '12px', lineHeight: '12px'}}>{t("rates:tariffs_grid:intervals_hint")}</div>
                <Button style={{marginTop: "10px"}} onClick={() => {
                    setInterval("08:00", "09:00");
                }}>{t('rates:tariffs_grid:add_interval')}</Button>
                {data?.dayIntervals?.map((v, i) => {
                    return <TariffInterval key={i} 
                                           interval={v}
                                           index={i}
                                           points={intervalPoints}
                                           setInterval={setInterval}/>
                    }
                )}
            </Form.Field>
        </Form>
    </div>);
}