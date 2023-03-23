import React from 'react';
import {PromoCodeDto} from "../../../models/dto/rates/TariffDto";
import {t} from "i18next";
import {Dropdown, Icon} from "semantic-ui-react";
import {FieldCoveringType} from "../../../models/dto/offers/enums/FieldCoveringType";
import {PromoCodeType} from "../../../models/dto/rates/enums/PromoCodeType";

export interface TariffPromoCodeProps {
    promoCode: PromoCodeDto;
    setPromoCode: any;
}

export const TariffPromoCode = (props: TariffPromoCodeProps) => {

    const promoCodesTypesAll = () => {
        const result = [];
        for (let item in PromoCodeType) {
            if (!isNaN(Number(item))) {
                const value = PromoCodeType[item];
                const text = t("rates:promo_code_types:" + value.toLowerCase());

                result.push({
                    key: item,
                    value: Number(item),
                    text: text
                })
            }
        }
        return result;
    }
    
    const onChangeCode = (event: any) => {
       const promoCode = {
           code: event.target.value,
           type: props.promoCode.type,
           value: props.promoCode.value
       } as PromoCodeDto;
       props.setPromoCode(promoCode, props.promoCode);
    }

    const onChangeValue = (event: any) => {
        const promoCode = {
            code: props.promoCode.code,
            type: props.promoCode.type,
            value: event.target.value
        } as PromoCodeDto;
        props.setPromoCode(promoCode, props.promoCode);
    }

    const onChangeType= (e: any, {value}: any) => {
        const promoCode = {
            code: props.promoCode.code,
            type: value,
            value: props.promoCode.value
        } as PromoCodeDto;
        props.setPromoCode(promoCode, props.promoCode);
    }
    
    const remove = () => {
        props.setPromoCode({}, props.promoCode, true);
    }
    
    return <div style={{marginTop: "10px", display: 'flex', alignItems: 'center', flexDirection: 'row'}}>
        <label style={{marginBottom: 0}}>{t("rates:tariffs_grid:promo_code_title")}</label>
        <input style={{ width: '200px', marginLeft: '5px'}} onChange={onChangeCode} id="code-input" placeholder={t("rates:tariffs_grid:promo_code_title") || ''}
               defaultValue={props.promoCode.code || ''}/>
        <label style={{marginLeft: '10px', marginBottom: 0}}>{t("rates:tariffs_grid:promo_code_type")}</label>
        <Dropdown
            placeholder={t("rates:tariffs_grid:promo_code_type") || ''}
            fluid
            search
            style={{width: "300px", marginLeft: '5px'}}
            selection
            onChange={onChangeType}
            value={props.promoCode.type}
            options={promoCodesTypesAll()}
        />
        <label style={{marginLeft: '10px', marginBottom: 0}}>{t("rates:tariffs_grid:promo_code_value")}</label>
        <input style={{ width: '200px', marginLeft: '5px'}} onChange={onChangeValue} type='number' id="code-input" placeholder={t("rates:tariffs_grid:promo_code_value") || ''}
               defaultValue={props.promoCode.value || ''}/>
        <Icon style={{marginLeft: '10px', cursor: 'pointer', fontSize: 18}} name='trash alternate' title={t('common:delete_button')}
              onClick={remove}/>
    </div>
}