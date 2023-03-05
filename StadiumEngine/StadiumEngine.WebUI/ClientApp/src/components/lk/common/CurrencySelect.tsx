import {Dropdown, Form} from "semantic-ui-react";
import {t} from "i18next";
import React from "react";
import {PriceOfferDto} from "../../../models/dto/offers/PriceOfferDto";
import {Currency} from "../../../models/dto/offers/enums/Currency";

export interface CurrencySelectProps<T> {
    data: T,
    setData: React.Dispatch<React.SetStateAction<T>>
}

export const CurrencySelect = <T extends PriceOfferDto, >(props: CurrencySelectProps<T>) => {
    const currenciesAll = () => {
        const result = [];
        for (let item in Currency) {
            if (!isNaN(Number(item))) {
                const value = Currency[item];
                const text = t("offers:currencies:" + value.toLowerCase());

                result.push({
                    key: item,
                    value: Number(item),
                    text: text
                })
            }
        }

        return result;
    }

    const changeCurrency = (e: any, {value}: any) => {
        props.setData({
            ...props.data,
            currency: value
        });
    }

    return <Form.Field>
        <label>{t("offers:currencies:title")}</label>
        <Dropdown
            placeholder={t("offers:currencies:title") || ''}
            fluid
            search
            selection
            onChange={changeCurrency}
            value={props.data.currency}
            options={currenciesAll()}
        />
    </Form.Field>
}