import {t} from "i18next";
import React from "react";
import {PromoCodeDto} from "../../../models/dto/rates/TariffDto";
import {getPromoDiscount} from "../../../helpers/booking-utils";

export interface BookingTotalAmountProps {
    getTotalAmountValue: Function;
    promo: PromoCodeDto|null;
    manualDiscount?: number; 
}

export const BookingTotalAmount = (props: BookingTotalAmountProps) => {

    const getAmount = () => {
        let amount = props.getTotalAmountValue();
        
        if (props.promo  || (props.manualDiscount||0) > 0) {
            const promoValue = getPromoDiscount(props.promo, amount);
            return <span style={{fontWeight: 'bold'}}>{amount - promoValue - (props.manualDiscount||0)} <span style={{textDecoration: 'line-through'}}>{amount}</span> {t("booking:checkout:rub")}</span>
        }
        return <span style={{fontWeight: 'bold'}}>{amount} {t("booking:checkout:rub")}</span>
    }
    
    return <div className="booking-checkout-amount" style={{ marginTop: '20px', borderTop: '1px solid #eee'}} >
        {t("booking:checkout:amount_total")}&nbsp; {getAmount()}
    </div>
}