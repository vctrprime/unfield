import {t} from "i18next";
import React from "react";

export interface BookingFieldAmountProps {
    getFieldAmount: Function;
}

export const BookingFieldAmount = (props: BookingFieldAmountProps) => {
    
    return <div className="booking-checkout-amount">
        {t("booking:checkout:amount_field")}&nbsp;<span style={{fontWeight: 'bold'}}>{props.getFieldAmount()} {t("booking:checkout:rub")}</span>
    </div>
}