import React, {useEffect, useState} from 'react';
import {ActionButtons} from "../../common/actions/ActionButtons";
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

export interface HeaderField {
    id: number;
    name: string;
    priceGroupId?: number;
    children?: string[]
}

export const Prices = () => {
    document.title = getTitle("rates:prices_tab")

    const stadium = useRecoilValue(stadiumAtom);
    const setLoading = useSetRecoilState(loadingAtom);
    
    const [isError, setIsError] = useState<boolean>(false);
    
    const [fields, setFields] = useState<HeaderField[]>([]);
    const [tariffs, setTariffs] = useState<TariffDto[]>([]);
    const [prices, setPrices] = useState<PriceDto[]>([]);

    const [offersService] = useInject<IOffersService>('OffersService');
    const [ratesService] = useInject<IRatesService>('RatesService');
    
    useEffect(() => {
        offersService.getFields().then((responseFields: FieldDto[]) => {
            const headersFields: HeaderField[] = [];
            responseFields.forEach((f) => {
                if (f.priceGroupId === null) {
                    headersFields.push({
                        id: f.id,
                        name: f.name,
                    })
                }
                else {
                    if (headersFields.filter(hf => hf.priceGroupId === f.priceGroupId).length === 0 ) {
                        headersFields.push({
                            id: f.id,
                            name: f.priceGroupName||f.name,
                            priceGroupId: f.priceGroupId,
                            children: responseFields.map((rf) => rf.name)
                        })
                    }
                }
            })
            setFields(headersFields);
            /*ratesService.getTariffs().then((responseTariffs: TariffDto[]) => {
                setTariffs(responseTariffs);
                ratesService.getPrices().then((responsePrices: PriceDto[]) => {
                    setPrices(responsePrices);
                }).finally(() => setLoading(false))
            })
                .catch(() => {
                    setLoading(false);
                })*/
        })
    }, [stadium])
    
    
    
    return isError ? <span/> : (<div className="prices-container">
        123
    </div>);
}