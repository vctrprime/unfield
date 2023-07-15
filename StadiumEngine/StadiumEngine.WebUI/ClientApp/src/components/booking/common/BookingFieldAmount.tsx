import {t} from "i18next";
import React from "react";
import {BookingCheckoutDto} from "../../../models/dto/booking/BookingCheckoutDto";

export interface BookingFieldAmountProps {
    getFieldAmountValue: Function;
    selectedDuration: number;
    data: BookingCheckoutDto;
    isEditable: boolean;
}

export const BookingFieldAmount = (props: BookingFieldAmountProps) => {

    const getFieldAmount = () => {
        if (!props.isEditable) {
            return <span style={{fontWeight: 'bold'}}>{props.getFieldAmountValue()} руб.</span>;
        }
        if (props.data) {
            const amount = props.getFieldAmountValue();
            const durationAmount = props.data.durationAmounts.find( x => x.duration === props.selectedDuration);

            if (durationAmount) {
                if (durationAmount.value > amount) {
                    return <span style={{fontWeight: 'bold'}}>{amount} <span style={{textDecoration: 'line-through'}}>{durationAmount.value}</span> руб.</span>
                }

                return <span style={{fontWeight: 'bold'}}>{amount} руб.</span>
            }

            return <span/>;
        }
        return <span/>;
    }
    
    return <div className="booking-checkout-amount">
        {t("booking:checkout:amount_field")}&nbsp; {getFieldAmount()}
    </div>
}