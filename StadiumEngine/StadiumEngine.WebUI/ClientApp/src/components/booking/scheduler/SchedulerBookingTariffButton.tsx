import {BookingFormFieldSlotPriceDto} from "../../../models/dto/booking/BookingFormDto";
import React from "react";

export interface  SchedulerBookingTariffButtonProps {
    slotPrice: BookingFormFieldSlotPriceDto;
    isCurrent: boolean;
    action: any;
}

export const SchedulerBookingTariffButton = (props:  SchedulerBookingTariffButtonProps) => {
    return <div 
        onClick={props.action} 
        className={"scheduler-booking-tariff-button" + (props.isCurrent ? " current" : "")}>{props.slotPrice.tariffName}
    </div>
}