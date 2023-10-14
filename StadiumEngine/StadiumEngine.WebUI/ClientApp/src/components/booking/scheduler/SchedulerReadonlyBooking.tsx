import {BookingDto} from "../../../models/dto/booking/BookingDto";
import React, {useEffect} from "react";
import {BookingHeader} from "../common/BookingHeader";
import {BookingCheckoutDto} from "../../../models/dto/booking/BookingCheckoutDto";
import {Container} from "reactstrap";
import {
    dateFormatter,
    dateFormatterByStringWithoutTime,
    dateFormatterWithoutTime
} from "../../../helpers/date-formatter";
import {Checkbox, Dropdown} from "semantic-ui-react";
import {t} from "i18next";
import {SchedulerBookingTariffButton} from "./SchedulerBookingTariffButton";
import {BookingDuration} from "../common/BookingDuration";
import {parseNumber} from "../../../helpers/time-point-parser";

export interface SchedulerReadonlyBookingProps {
    booking: BookingDto;
}

export const SchedulerReadonlyBooking = ({ booking } : SchedulerReadonlyBookingProps) => {
    
    useEffect(() => {
    })
    
    return <Container className="booking-checkout-container" style={{minHeight: "auto"}}>
        <BookingHeader data={{
            bookingNumber: booking.number,
            day: dateFormatterByStringWithoutTime(booking.day),
            stadiumName: '',
            field: booking.field
        } as BookingCheckoutDto} withStadiumName={false} />
        <div className="booking-locker-room-weekly-row">
            <Checkbox
                checked={booking.isWeekly}
                disabled={true} 
                label={t('schedule:scheduler:booking:is_weekly')} />
            {!booking.isWeekly && booking.lockerRoom && <div className="booking-locker-room-weekly-row-right">
                <Dropdown
                    fluid
                    style={{width: "200px"}}
                    disabled={true}
                    placeholder={t('schedule:scheduler:booking:locker_room')||''}
                    value={booking.lockerRoom?.id||''}
                    options={[{key: booking.lockerRoom?.id, value: booking.lockerRoom?.id, text: booking.lockerRoom?.name}]}
                />
            </div>}
        </div>
        <div className="scheduler-booking-tariff-buttons">
            <span>{t("booking:field_card:tariff")}: <b>{booking.tariff.name}</b></span>
        </div>
        <BookingDuration
            isEditable={false}
            data={{
                pointPrices: [{
                 displayStart: parseNumber(booking.startHour)   
                }]
            } as BookingCheckoutDto}
            selectedDuration={booking.hoursCount}
            setSelectedDuration={() => null} />
    </Container>
}