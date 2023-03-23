import React, {useEffect, useState} from 'react';
import {PermissionsKeys} from "../../../static/PermissionsKeys";
import {t} from "i18next";
import {getTitle} from "../../../helpers/utils";
import {FieldDto} from "../../../models/dto/offers/FieldDto";
import {TariffDto} from "../../../models/dto/rates/TariffDto";
import {PriceDto} from "../../../models/dto/rates/PriceDto";
import {useRecoilValue, useSetRecoilState} from "recoil";
import {stadiumAtom} from "../../../state/stadium";
import {useInject} from "inversify-hooks";
import {IRatesService} from "../../../services/RatesService";
import {IOffersService} from "../../../services/OffersService";
import {loadingAtom} from "../../../state/loading";
import {Button, Icon, Input} from "semantic-ui-react";
import {permissionsAtom} from "../../../state/permissions";
import {PromoCodeType} from "../../../models/dto/rates/enums/PromoCodeType";
import {Tr} from "react-flags-select";

export interface HeaderField {
    id: number;
    name: string;
    priceGroupId?: number;
    hint?: string
}

export const Prices = () => {
    document.title = getTitle("rates:prices_tab")

    const permissions = useRecoilValue(permissionsAtom);

    const stadium = useRecoilValue(stadiumAtom);
    const setLoading = useSetRecoilState(loadingAtom);
    
    const [isError, setIsError] = useState<boolean>(false);
    
    const [fields, setFields] = useState<HeaderField[]>([]);
    const [tariffs, setTariffs] = useState<TariffDto[]>([]);
    const [prices, setPrices] = useState<PriceDto[]>([]);
    const [initialPrices, setInitialPrices] = useState<PriceDto[]>([]);

    const [offersService] = useInject<IOffersService>('OffersService');
    const [ratesService] = useInject<IRatesService>('RatesService');
    
    const [promoCode, setPromoCode] = useState<string>('');
    
    const fieldsIds: number[] = []
    
    useEffect(() => {
        setLoading(true);
        offersService.getFields().then((responseFields: FieldDto[]) => {
            processFields(responseFields);
            ratesService.getTariffs().then((responseTariffs: TariffDto[]) => {
                setTariffs(responseTariffs);
                ratesService.getPrices().then((responsePrices: PriceDto[]) => {
                    setPrices(responsePrices.filter(p => fieldsIds.indexOf(p.fieldId) !== -1));
                    setInitialPrices(responsePrices.filter(p => fieldsIds.indexOf(p.fieldId) !== -1));
                    setLoading(false);
                })
            })
        })
    }, [stadium])
    
    const processFields = (responseFields: FieldDto[]) => {
        const headersFields: HeaderField[] = [];
        responseFields.filter(f => f.isActive).forEach((f) => {
            if (f.priceGroupId === null) {
                fieldsIds.push(f.id);
                headersFields.push({
                    id: f.id,
                    name: f.name,
                })
            }
            else {
                if (headersFields.filter(hf => hf.priceGroupId === f.priceGroupId).length === 0 ) {
                    const children = responseFields.filter(x => x.priceGroupId === f.priceGroupId && x.isActive).map(x => x.name).join('\r\n');
                    fieldsIds.push(f.id);
                    headersFields.push({
                        id: f.id,
                        name: f.priceGroupName||f.name,
                        priceGroupId: f.priceGroupId,
                        hint: `${t('rates:prices:price_group_hint')}\r\n${children}`
                    })
                }
            }
        })
        setFields(headersFields);
    }
    
    const changePrice = (valueStr: string, intervalId: number, fieldId: number) => {
        if (permissions.filter(p => p.name === PermissionsKeys.SetPrices).length === 0) {
            return;
        }
        
        const value = parseInt(valueStr);
        const currentPrice = prices.find(p => p.tariffDayIntervalId === intervalId && p.fieldId === fieldId);
        if (value) {
            const newPrices = prices.filter(p => p !== currentPrice);
            newPrices.push({
                value: value,
                tariffDayIntervalId: intervalId,
                fieldId: fieldId
            })
            setPrices(newPrices);
        }
        else {
            if (currentPrice) {
                setPrices(prices.filter(p => p !== currentPrice));
            }
        }
    }
    
    const savePrices = () => {
        ratesService.setPrices({
            prices: prices
        }).then(() => {
            setInitialPrices(prices);
        });
    }
    
    const resetPrices = () => {
        setPrices(initialPrices);
    }
    
    const hasChanged = () => {
        let result = false;
        if (prices.length !== initialPrices.length) {
            return true;
        }
        prices.forEach(p => {
            const initialPrice = initialPrices.find(ip => ip.fieldId === p.fieldId && ip.tariffDayIntervalId === p.tariffDayIntervalId);
            if (initialPrice?.value !== p.value) {
                result = true;
            }
        })
        
        return result;
    }

    const promoCodesForTariffsText = () => {
        let result: string[] = [];
        tariffs.filter(s => s.isActive).forEach((tariff) => {
            if (tariff.promoCodes.length > 0) {
                const promoCodes = tariff.promoCodes.map(p => p.code).join('\r\n');
                result.push(`${t('rates:prices:promocodes_for_tariff')} "${tariff.name}":\r\n${promoCodes}`);
            }
            else {
                result.push(`${t('rates:prices:promocodes_for_tariff')} "${tariff.name}":\r\n-`);
            }
        })
        return result.join('\r\n\r\n');
    }


    return isError ? <span/> : (<div className="prices-container">
        <label className="box-shadow" style={{padding: '8px', marginLeft: '8px', width: 'calc(100% - 16px)', borderRadius: '10px', backgroundColor: 'white'}}>
            <i style={{color: '#00d2ff'}} className="fa fa-exclamation-circle" aria-hidden="true"/> {t('rates:prices:message_line1')} <br/>
            {t('rates:prices:message_line2')} <br/> <br/>
            {t('rates:prices:message_line3')}
        </label>
        <div className="fields-header">
            <div className="top-left-cell" />
            {fields.map((f) => {
                const name = f.priceGroupId === undefined ? f.name : `${t('rates:prices:price_group_short')}: ${f.name}`
                
                return <div key={f.id} className="fields-header-cell">
                    <span>{name}</span>
                    {f.hint && <Icon name='question circle outline' style={{ fontSize: '11px', marginLeft: '4px', height: '100%'}} title={f.hint} />}
                </div>
            })}
        </div>
        {tariffs.filter(s => s.isActive).map((tariff) => {
            return <div key={tariff.id} className="tariff-block">
                <div className="tariff-name">{tariff.name}</div>
                {tariff.dayIntervals.map((i) => {
                    return <div key={i.tariffDayIntervalId} className="tariff-interval-row">
                        <div className="tariff-interval-name">{i.interval[0]}-{i.interval[1]}</div>
                        {fields.map((f) => {
                            const price = prices.find(p => p.tariffDayIntervalId == i.tariffDayIntervalId && p.fieldId == f.id);
                            
                            const tariffPromoCode = tariff.promoCodes.find(p => p.code.toLowerCase() == promoCode.toLowerCase());
                            
                            let promocodePriceValue = price?.value;
                            if (tariffPromoCode !== undefined && promocodePriceValue !== undefined) {
                                if (tariffPromoCode.type === PromoCodeType.Percent) {
                                    promocodePriceValue = promocodePriceValue * ((100 - tariffPromoCode.value) / 100);
                                }
                                if (tariffPromoCode.type === PromoCodeType.Fixed) {
                                    promocodePriceValue = promocodePriceValue - tariffPromoCode.value;
                                }
                            }
                            
                            const initialPrice = initialPrices.find(p => p.tariffDayIntervalId == i.tariffDayIntervalId && p.fieldId == f.id);
                            const title = initialPrice?.value !== price?.value ? `${t('rates:prices:initial')}: ${initialPrice?.value === undefined ? '-' : initialPrice?.value}` : null;
                            
                            return <div title={title||''} style={ title !== null ? { backgroundColor: "rgba(102, 205, 170, 0.5)"} : {}} key={f.id} className="tariff-interval-value">
                                <Input
                                    transparent onChange={(e, {value}) => changePrice(value, i.tariffDayIntervalId||0, f.id)}
                                       type="number"
                                       value={price?.value || ''}/>
                                {promocodePriceValue !== undefined && tariffPromoCode !== undefined && 
                                    <span title={t('rates:prices:promocode_value')||''} style={{ position: 'absolute', right: '5px',
                                        fontSize: '12px', cursor: 'pointer'}}>{promocodePriceValue}</span>
                                }
                            </div>
                        })}
                    </div>
                })}
            </div>
        })}
        <label style={{marginTop: '10px', marginLeft: '10px'}}>{t("rates:prices:promo_code_hint")}<Icon name='question circle outline' style={{ fontSize: '11px', marginLeft: '4px', height: '100%'}} title={promoCodesForTariffsText()} /></label> <br />
        <Input style={{ width: '200px', marginLeft: '10px'}} onChange={(event) => setPromoCode(event.target.value)} placeholder={t("rates:prices:promo_code_title") || ''}
               defaultValue={promoCode}/>
        {permissions.filter(p => p.name === PermissionsKeys.SetPrices).length > 0 &&
            <div style={{marginTop: '10px'}}>
                <Button disabled={!hasChanged()} style={{ marginLeft: '5px', backgroundColor: '#3CB371', color: 'white'}} onClick={savePrices}>{t('rates:prices:save')}</Button>
                <Button disabled={!hasChanged()} style={{ marginLeft: '5px', backgroundColor: '#CD5C5C', color: 'white'}} onClick={resetPrices}>{t('rates:prices:reset')}</Button>
            </div>
            }
    </div>);
}