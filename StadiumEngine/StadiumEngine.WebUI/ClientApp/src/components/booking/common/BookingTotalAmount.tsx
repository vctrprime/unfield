import {t} from "i18next";
import React from "react";

export interface BookingTotalAmountProps {
    getTotalAmount: Function
}

export const BookingTotalAmount = (props: BookingTotalAmountProps) => {
    return <div className="booking-checkout-amount" style={{ marginTop: '20px', borderTop: '1px solid #eee'}} >
        {t("booking:checkout:amount_total")} &nbsp;<span style={{fontWeight: 'bold'}}>{props.getTotalAmount()} {t("booking:checkout:rub")}</span>
    </div>
}