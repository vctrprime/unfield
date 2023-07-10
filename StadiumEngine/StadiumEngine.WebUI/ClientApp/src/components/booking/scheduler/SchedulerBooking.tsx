import React, {useEffect, useState} from "react";
import {SchedulerBookingDto} from "../../../models/dto/booking/SchedulerBookingDto";
import {Container} from "reactstrap";
import {Form} from "semantic-ui-react";
import {BookingHeader} from "../common/BookingHeader";
import {BookingInventory, SelectedInventory} from "../common/BookingInventory";
import {BookingDto, BookingPromoDto} from "../../../models/dto/booking/BookingDto";
import {useInject} from "inversify-hooks";
import {IBookingService} from "../../../services/BookingService";
import {BookingCheckoutDto} from "../../../models/dto/booking/BookingCheckoutDto";
import {CheckoutDiscount} from "../checkout/BookingCheckoutButtons";
import {calculateDiscounts, getFieldAmountValue, getInventoryAmount} from "../../../helpers/booking-utils";
import {BookingFieldAmount} from "../common/BookingFieldAmount";
import {t} from "i18next";
import {BookingTotalAmount} from "../common/BookingTotalAmount";
import {BookingCustomer} from "../common/BookingCustomer";
import {BookingDuration} from "../common/BookingDuration";
import {BookingFormFieldSlotPriceDto} from "../../../models/dto/booking/BookingFormDto";

export interface SchedulerBooking {
    bookingData: BookingDto;
    slotPrices: BookingFormFieldSlotPriceDto[];
}

export const SchedulerBooking = (props: SchedulerBooking) => {
    const [data, setData] = useState<SchedulerBookingDto|null>(null);
    
    const isNew = props.bookingData.id === 0;

    const [promo, setPromo] = useState<BookingPromoDto|null>(isNew ? null : props.bookingData.promo);
    const [discounts, setDiscounts] = useState<CheckoutDiscount[]>([]);

    const [selectedDuration, setSelectedDuration] = useState<number>(isNew ? 1 : props.bookingData.hoursCount);
    const [selectedInventories, setSelectedInventories] = useState<SelectedInventory[]>(isNew ? [] : props.bookingData.inventories.map((i) => {
        return {
            id: i.inventory.id,
            quantity: i.quantity,
            price: i.price
        } as SelectedInventory
    }));

    const [phoneNumber, setPhoneNumber] = useState<string | undefined>(isNew ? undefined : props.bookingData.customer?.phoneNumber || undefined);
    const [name, setName] = useState<string | undefined>(isNew ? undefined : props.bookingData.customer?.name || undefined);

    const [bookingService] = useInject<IBookingService>('BookingService');

    useEffect(() => {
        if (data?.checkoutData) {
            calculateDiscounts(promo, data?.checkoutData, setDiscounts);
        }
    }, [data])
    
    useEffect(() => {
        bookingService.getBookingCheckout(props.bookingData.number, !isNew).then((response: BookingCheckoutDto) => {
            setData({
                checkoutData: response
            })
        })
    }, [])

    const getSchedulerBookingFieldAmountValue = () => {
        return getFieldAmountValue(selectedDuration, data?.checkoutData||null, discounts);
    }

    const getSchedulerBookingInventoryAmount = () => {
        return getInventoryAmount(selectedDuration, selectedInventories);
    }

    const getTotalAmount = () => {
        return getSchedulerBookingFieldAmountValue() + getSchedulerBookingInventoryAmount();
    }
    
    return data === null  ? null :  <Container className="booking-checkout-container">
        <Form style={{paddingBottom: '10px'}}>
            <BookingHeader data={data.checkoutData} withStadiumName={false} />
            {props.slotPrices.map((p) => {
                return <p key={p.tariffId}>{p.tariffName}</p>
                })}
            <BookingDuration data={data.checkoutData} selectedDuration={selectedDuration} setSelectedDuration={setSelectedDuration} />
            <BookingFieldAmount
                getFieldAmountValue={getSchedulerBookingFieldAmountValue}
                selectedDuration={selectedDuration}
                data={data.checkoutData} />
            <BookingInventory
                data={data.checkoutData}
                selectedDuration={selectedDuration}
                selectedInventories={selectedInventories}
                setSelectedInventories={setSelectedInventories}
                getInventoryAmount={getSchedulerBookingInventoryAmount}
                headerText={t("booking:scheduler:inventory_header")}/>
            <BookingTotalAmount getTotalAmount={getTotalAmount}/>
            <BookingCustomer
                name={name}
                setName={setName}
                phoneNumber={phoneNumber}
                setPhoneNumber={setPhoneNumber}
                headerText={t("booking:scheduler:inputs_header")} />
        </Form>
    </Container>
}