import React, {useEffect, useState} from "react";
import {SchedulerBookingDto} from "../../../models/dto/booking/SchedulerBookingDto";
import {Container} from "reactstrap";
import {Form} from "semantic-ui-react";
import {BookingHeader} from "../common/BookingHeader";
import {BookingInventory, SelectedInventory} from "../common/BookingInventory";
import {BookingDto, BookingPromoDto} from "../../../models/dto/booking/BookingDto";
import {useInject} from "inversify-hooks";
import {IBookingFormService} from "../../../services/BookingFormService";
import {BookingCheckoutDto} from "../../../models/dto/booking/BookingCheckoutDto";
import {CheckoutDiscount} from "../checkout/BookingCheckoutButtons";
import {calculateDiscounts, getFieldAmountValue, getInventoryAmount} from "../../../helpers/booking-utils";
import {BookingFieldAmount} from "../common/BookingFieldAmount";
import {t} from "i18next";
import {BookingTotalAmount} from "../common/BookingTotalAmount";
import {BookingCustomer} from "../common/BookingCustomer";
import {BookingDuration} from "../common/BookingDuration";

export interface SchedulerBooking {
    bookingData: BookingDto;
}

export const SchedulerBooking = (props: SchedulerBooking) => {
    const [data, setData] = useState<SchedulerBookingDto|null>(null);

    const [promo, setPromo] = useState<BookingPromoDto|null>(props.bookingData.promo);
    const [discounts, setDiscounts] = useState<CheckoutDiscount[]>([]);

    const [selectedDuration, setSelectedDuration] = useState<number>(props.bookingData.hoursCount ?? 1);
    const [selectedInventories, setSelectedInventories] = useState<SelectedInventory[]>(props.bookingData.inventories.map((i) => {
        return {
            id: i.inventory.id,
            quantity: i.quantity,
            price: i.price
        } as SelectedInventory
    }));

    const [phoneNumber, setPhoneNumber] = useState<string | undefined>(props.bookingData.customer?.phoneNumber || undefined);
    const [name, setName] = useState<string | undefined>(props.bookingData.customer?.name || undefined);

    const [bookingFormService] = useInject<IBookingFormService>('BookingFormService');

    useEffect(() => {
        if (data?.checkoutData) {
            calculateDiscounts(promo, data?.checkoutData, setDiscounts);
        }
    }, [data])
    
    useEffect(() => {
        bookingFormService.getBookingCheckout(props.bookingData.number, true).then((response: BookingCheckoutDto) => {
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