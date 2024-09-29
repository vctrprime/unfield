import React from "react";
import {t} from "i18next";
import {Dropdown, Input} from "semantic-ui-react";
import {BookingCheckoutDto} from "../../../../models/dto/bookings/BookingCheckoutDto";
import {getDurationText} from "../../../../helpers/utils";

export interface BookingDurationProps {
    data: BookingCheckoutDto;
    selectedDuration: number;
    setSelectedDuration: Function;
    isEditable: boolean;
}

export const BookingDuration = (props: BookingDurationProps) => {
    return <div className="booking-checkout-durations">
        <span style={{ fontSize: '16px'}}>{t("booking:checkout:from")} <span style={{fontWeight: 'bold'}}>{props.data.pointPrices[0].displayStart}</span> {t("booking:checkout:at")}  &nbsp;</span>
        {props.isEditable ? <Dropdown
        fluid
        style={{width: "115px"}}
        selection
        onChange={(e: any, {value}: any) => props.setSelectedDuration(value)}
        value={props.selectedDuration}
        options={props.data ? props.data.durationAmounts.map((a) => {
            return {
                key: a.duration,
                value: a.duration,
                text: getDurationText(a.duration)
            }
        }) : []}
    /> : <Input value={getDurationText(props.selectedDuration)} readOnly style={{ width: '100px'}}/>}
    </div>
}