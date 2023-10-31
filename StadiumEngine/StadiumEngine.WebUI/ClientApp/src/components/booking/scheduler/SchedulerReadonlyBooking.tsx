import {BookingDto} from "../../../models/dto/booking/BookingDto";
import React, {useEffect} from "react";
import {BookingHeader} from "../common/BookingHeader";
import {BookingCheckoutDto} from "../../../models/dto/booking/BookingCheckoutDto";
import {Container} from "reactstrap";
import {
    dateFormatterByStringWithoutTime
} from "../../../helpers/date-formatter";
import {Checkbox, Dropdown, Form} from "semantic-ui-react";
import {t} from "i18next";
import {BookingDuration} from "../common/BookingDuration";
import {parseNumber} from "../../../helpers/time-point-parser";
import {
    getFieldAmountValueByBooking,
    getInventoryAmountByBooking, getPromoDiscount
} from "../../../helpers/booking-utils";
import {BookingFieldAmount} from "../common/BookingFieldAmount";
import {BookingInventory} from "../common/BookingInventory";
import {BookingTotalAmount} from "../common/BookingTotalAmount";
import {BookingCustomer} from "../common/BookingCustomer";

export interface SchedulerReadonlyBookingProps {
    booking: BookingDto;
    fromSearch?: boolean;
}

export const SchedulerReadonlyBooking = ({ booking, fromSearch } : SchedulerReadonlyBookingProps) => {
    
    useEffect(() => {
    })

    const getSchedulerBookingFieldAmount = () => {
        return getFieldAmountValueByBooking(booking);
    }

    const getSchedulerBookingInventoryAmount = () => {
        return getInventoryAmountByBooking(booking);
    }

    const getTotalAmountValue = () => {
        return getSchedulerBookingFieldAmount() + getSchedulerBookingInventoryAmount();
    }

    const getEventEachText = () => {
        const date = new Date(booking.day);
        return t(`schedule:scheduler:each:${date?.getDay()}`);
    }
    
    return <Container className="booking-checkout-container" style={{minHeight: "auto"}}>
        <Form style={{paddingBottom: '15px'}}>
            <BookingHeader data={{
                bookingNumber: booking.number,
                day: dateFormatterByStringWithoutTime(booking.day),
                stadiumName: '',
                field: booking.field
            } as BookingCheckoutDto} dayText={fromSearch ? booking.isWeekly ? getEventEachText(): null : null} withStadiumName={false} />
            <div className="booking-locker-room-weekly-row">
                <Checkbox
                    checked={booking.isWeekly}
                    disabled={true} 
                    label={t('schedule:scheduler:booking:is_weekly')} />
                {!booking.isWeekly && booking.lockerRoom && <div className="booking-locker-room-weekly-row-right">
                    <Dropdown
                        fluid
                        selection
                        disabled={true}
                        style={{width: "200px"}}
                        placeholder={t('schedule:scheduler:booking:locker_room')||''}
                        value={booking.lockerRoom?.id||''}
                        options={[{key: booking.lockerRoom?.id, value: booking.lockerRoom?.id, text: booking.lockerRoom?.name}]}
                    />
                </div>}
            </div>
            <div className="scheduler-booking-tariff-buttons">
                <span>{t("booking:field_card:tariff")}: <b>{booking.tariff.name}</b></span>
            </div>
            {booking.promo && <div style={{marginTop: '5px',
                fontSize: '12px',
                color: '#666'}}><i style={{ color: '#00d2ff'}} className="fa fa-exclamation-circle" aria-hidden="true"/> {t('schedule:scheduler:booking:promo_applied')}: <b style={{color: 'black'}}>{booking.promo.code} (-{getPromoDiscount(booking.promo, booking.totalAmountBeforeDiscount)})</b>.</div>}
            <BookingDuration
                isEditable={false}
                data={{
                    pointPrices: [{
                     displayStart: parseNumber(booking.startHour)   
                    }]
                } as BookingCheckoutDto}
                selectedDuration={booking.hoursCount}
                setSelectedDuration={() => null} />
            <BookingFieldAmount getFieldAmount={getSchedulerBookingFieldAmount} />
            <BookingInventory
                data={null}
                isEditable={false}
                selectedDuration={0}
                selectedInventories={[]}
                setSelectedInventories={() => null}
                getInventoryAmount={getSchedulerBookingInventoryAmount}
                bookingInventories={booking.inventories||[]}
                headerText={t("booking:scheduler:inventory_header")}/>
            <BookingTotalAmount getTotalAmountValue={getTotalAmountValue} promo={booking.promo} manualDiscount={booking.manualDiscount ?? undefined}/>
            {booking.manualDiscount && booking.manualDiscount > 0 && <div style={{marginTop: '5px',
                fontSize: '12px',
                textAlign: 'right',
                color: '#666'}}><i style={{ color: '#00d2ff'}} className="fa fa-exclamation-circle" aria-hidden="true"/> {t('schedule:scheduler:booking:manual_discount_applied')} <b style={{color: 'black'}}>(-{booking.manualDiscount})</b></div>}
            <BookingCustomer
                name={booking.customer.name ?? undefined}
                setName={() => null}
                phoneNumber={booking.customer.phoneNumber ?? undefined}
                setPhoneNumber={() => null}
                headerText={t("booking:scheduler:inputs_header")}
                isReadonly={true}
            />
        </Form>
    </Container>
}