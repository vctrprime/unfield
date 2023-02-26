import {Dropdown, Form} from "semantic-ui-react";
import {t} from "i18next";
import React from "react";
import {SportKind} from "../../../models/dto/offers/enums/SportKind";
import {OfferDto} from "../../../models/dto/offers/OfferDto";

export interface SportKindSelectProps<T> {
    data: T,
    setData:  React.Dispatch<React.SetStateAction<T>>
}

export const SportKindSelect = <T extends OfferDto,>(props: SportKindSelectProps<T>) => {
    const sportKindsAll = () => {
        const result = [];
        for (let item in SportKind) {
            if (!isNaN(Number(item))) {
                const value = SportKind[item];
                const text = t("offers:sports:" + value.toLowerCase());

                result.push({
                    key: item,
                    value: Number(item),
                    text: text
                })
            }
        }

        return result;
    }
    
    const changeSportKinds = (e : any, { value }: any) => {
        props.setData({
            ...props.data,
            sportKinds: value
        });
    }
    
    return  <Form.Field >
        <label>{t("offers:sports:title")}</label>
        <Dropdown
            placeholder={t("offers:sports:title")||''}
            fluid
            search
            multiple
            style={{width: "500px"}}
            selection
            onChange={changeSportKinds}
            value={props.data.sportKinds}
            options={sportKindsAll()}
        />
    </Form.Field>
}