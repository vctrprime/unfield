import React, {useEffect, useRef, useState} from 'react';
import {useNavigate, useParams} from "react-router-dom";
import {PromoCodeDto, TariffDayIntervalDto, TariffDto} from "../../../models/dto/rates/TariffDto";
import {useInject} from "inversify-hooks";
import {IRatesService} from "../../../services/RatesService";
import {getDataTitle, getTitle, StringFormat, validateInputs} from "../../../helpers/utils";
import {ActionButtons} from "../../common/actions/ActionButtons";
import {PermissionsKeys} from "../../../static/PermissionsKeys";
import {t} from "i18next";
import {Button, Checkbox, Form, Modal} from "semantic-ui-react";
import {DateRangeSelect} from "../common/DateRangeSelect";
import {TariffInterval} from "./TariffInterval";
import {AddTariffCommand} from "../../../models/command/rates/AddTariffCommand";
import {UpdateTariffCommand} from "../../../models/command/rates/UpdateTariffCommand";
import {ISettingsService} from "../../../services/SettingsService";
import {PromoCodeType} from "../../../models/dto/rates/enums/PromoCodeType";
import {TariffPromoCode} from "./TariffPromoCode";

const ReactNotifications = require('react-notifications');
const {NotificationManager} = ReactNotifications;


export const Tariff = () => {
    let {id} = useParams();

    const [data, setData] = useState<TariffDto>({
        isActive: true,
        dateStart: new Date(),
        dayIntervals: [] as TariffDayIntervalDto[],
        promoCodes: [] as PromoCodeDto[]
    } as TariffDto);
    const [isError, setIsError] = useState<boolean>(false);
    const [tariffId, setTariffId] = useState(parseInt(id || "0"))
    
    const [intervalPoints, setIntervalPoints] = useState<string[]>([]);
    const [validateIntervalModal, setValidateIntervalModal] = useState<boolean>();
    
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
        settingsService.getMainSettings().then((result) => {
            const points: string[] = [];
            for (let i = result.openTime; i <= result.closeTime; i++) {
                points.push(i.toString().length === 1 ? `0${i}:00`: `${i}:00`);
                if (i < result.closeTime) {
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
        if (validateInputs([nameInput]) && validatePromoCodes()) {
            validateIntervalsAndSaveTariff();
        }
    }
    
    const sendSaveTariffRequest = () => {
        if (id === "new") {
            addTariff();
        }
        else {
            updateTariff();
        }
        setValidateIntervalModal(false);
    }

    const addTariff = () => {
        if (validateInputs([nameInput]) && validatePromoCodes()) {
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
                dayIntervals: data.dayIntervals,
                promoCodes: data.promoCodes
            }
            ratesService.addTariff(command).then(() => {
                navigate("/extranet/rates/tariffs");
            })
        }
    }

    const updateTariff = () => {
        if (validateInputs([nameInput]) && validatePromoCodes()) {
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
                dayIntervals: data.dayIntervals,
                promoCodes: data.promoCodes
            }
            ratesService.updateTariff(command).then(() => {
                navigate("/extranet/rates/tariffs");
            })
        }
    }
    
    const validatePromoCodes = (): boolean => {
        const sameCodes: string[] = [];
        let result = true;
        data.promoCodes.forEach((p) => {
            if (p.code.length < 3) {
                NotificationManager.error(t('errors:rates:promocode_min_length'), t('common:error_request_title'), 5000);
                result = false;
            }
            if (p.value <= 0) {
                NotificationManager.error(t('errors:rates:promocode_min_value'), t('common:error_request_title'), 5000);
                result = false;
            }
            if (p.type === PromoCodeType.Percent && p.value > 99) {
                NotificationManager.error(t('errors:rates:promocode_max_value'), t('common:error_request_title'), 5000);
                result = false;
            }
            const sameCode = data.promoCodes.find(c => c !== p && c.code.toLowerCase() === p.code.toLowerCase());
            if (sameCode !== undefined && sameCodes.indexOf(sameCode.code.toLowerCase()) === -1) {
                sameCodes.push(sameCode.code.toLowerCase());
                NotificationManager.error(t('errors:rates:promocode_same_codes'), t('common:error_request_title'), 5000);
                result = false;
            }
        })
        
        return result;
    }
    
    const validateIntervalsAndSaveTariff = () => {
        const indexes: any = {};
        let lastEndIndex : number|null = null;
        
        data.dayIntervals.forEach((int) => {
            const startIndex = intervalPoints.indexOf(int.interval[0]);
            const endIndex = intervalPoints.indexOf(int.interval[1]);
            
            if (lastEndIndex === null) {
                lastEndIndex = endIndex;
            }
            
            for (let i = startIndex; i <= endIndex; i++) {
                if (startIndex == lastEndIndex) {
                    lastEndIndex = endIndex;
                    continue;
                }
                
                const val = indexes[i];
                if (val === undefined) {
                    indexes[`${i}`] = 1
                }
                else {
                    indexes[`${i}`] = indexes[`${i}`] + 1;
                }
            }
        })
        
        if (Object.keys(indexes).find(a => indexes[a] > 1)) {
            NotificationManager.error(t('errors:rates:cross_intervals'), t('common:error_request_title'), 5000);
            return;
        }
        
        if (Object.keys(indexes).length < intervalPoints.length) {
            setValidateIntervalModal(true);
            return;
        }
        
        sendSaveTariffRequest();
    }

    const deleteTariff = () => {
        ratesService.deleteTariff(tariffId).then(() => {
            navigate("/extranet/rates/tariffs");
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
        const newIntervals: TariffDayIntervalDto[] = [];
        data?.dayIntervals?.forEach((i) => {
            if (i.interval !== interval) {
                newIntervals.push(i);
            }
            else {
                if (!isRemove) {
                    newIntervals.push({
                        interval: [start, end]
                    });
                }
            }
        });
        
        if (interval === null) {
            newIntervals.push({
                interval: [start, end]
            });
        }
        
        setData({
            ...data,
            dayIntervals: newIntervals
        });
    }

    
    const setPromoCode = (changedPromoCode: PromoCodeDto, currentPromoCode: PromoCodeDto|null = null, isRemove: boolean = false) => {
        const newPromoCodes: PromoCodeDto[] = [];
        data?.promoCodes?.forEach((p) => {
            if (p !== currentPromoCode) {
                newPromoCodes.push(p);
            }
            else {
                if (!isRemove) {
                    newPromoCodes.push(changedPromoCode);
                }
            }
        })

        if (currentPromoCode === null) {
            newPromoCodes.push(changedPromoCode);
        }

        setData({
            ...data,
            promoCodes: newPromoCodes
        });
    }
    
    return isError ? <span/> : (<div>
        <Modal
            dimmer='blurring'
            size='small'
            open={validateIntervalModal}>
            <Modal.Header>{t('rates:tariffs_grid:tariff_intervals_warning_header')}</Modal.Header>
            <Modal.Content>
                <p>{t('rates:tariffs_grid:tariff_intervals_warning')}</p>
            </Modal.Content>
            <Modal.Actions>
                <Button style={{backgroundColor: '#CD5C5C', color: 'white'}}
                        onClick={() => setValidateIntervalModal(false)}>{t('common:no_button')}</Button>
                <Button style={{backgroundColor: '#3CB371', color: 'white'}}
                        onClick={sendSaveTariffRequest}>{t('common:yes_button')}</Button>
            </Modal.Actions>
        </Modal>
        <ActionButtons
            savePermission={id === "new" ? PermissionsKeys.InsertTariff : PermissionsKeys.UpdateTariff}
            deletePermission={PermissionsKeys.DeleteTariff}
            title={id === "new" ? t('rates:tariffs_grid:adding') : t('rates:tariffs_grid:editing')}
            saveAction={saveTariff}
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
                                           interval={v.interval}
                                           index={i}
                                           points={intervalPoints}
                                           setInterval={setInterval}/>
                    }
                )}
            </Form.Field>
            <Form.Field>
                <label>{t("rates:tariffs_grid:promo_codes")}</label>
                <div style={{fontSize: '12px', lineHeight: '12px'}}>{t("rates:tariffs_grid:promo_codes_hint")}</div>
                <Button style={{marginTop: "10px"}} onClick={() => setPromoCode({
                    code: 'NEWPROMO' + (data.promoCodes.length + 1),
                    type: PromoCodeType.Percent,
                    value: 10
                })}>{t('rates:tariffs_grid:add_promo_code')}</Button>
                {data?.promoCodes?.map((p, i) => {
                    return <TariffPromoCode key={i} promoCode={p} setPromoCode={setPromoCode} />
                    }
                )}
            </Form.Field>
        </Form>
    </div>);
}