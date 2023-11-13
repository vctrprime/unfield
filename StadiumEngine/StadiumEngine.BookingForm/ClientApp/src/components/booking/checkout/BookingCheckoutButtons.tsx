import {Button} from "semantic-ui-react";
import {
    FillBookingDataCommandCost,
    FillBookingDataCommandInventory, FillBookingDataCommandPromo
} from "../../../models/command/booking/FillBookingDataCommand";
import {t} from "i18next";
import React, {useState} from "react";
import {useInject} from "inversify-hooks";
import {IBookingService} from "../../../services/BookingService";
import {BookingCancelModal} from "../common/BookingCancelModal";
import {PromoCodeDto} from "../../../models/dto/rates/TariffDto";
import {BookingCheckoutDto} from "../../../models/dto/booking/BookingCheckoutDto";
import {SelectedInventory} from "../common/BookingInventory";
import {useNavigate} from "react-router-dom";
import {getPromoDiscount} from "../../../helpers/booking-utils";

export type CheckoutDiscount = {
    duration: number,
    value: number
}

export interface BookingCheckoutButtonsProps {
    backPath: string;
    bookingNumber: string|undefined;
    data: BookingCheckoutDto;
    selectedDuration: number;
    promo: PromoCodeDto|null;
    name: string|undefined;
    phoneNumber: string|undefined;
    selectedInventories: SelectedInventory[];
    totalAmount: number;
}

export const BookingCheckoutButtons = (props: BookingCheckoutButtonsProps) => {
    const [cancelModal, setCancelModal] = useState<boolean>(false)
    
    const [bookingService] = useInject<IBookingService>('BookingService');

    const navigate = useNavigate();

    const confirmButtonDisabled = () => {
        return !((props.name?.length || 0 ) > 1 && props.phoneNumber?.length === 11);
    }
    
    return <div className="booking-checkout-buttons">
        <BookingCancelModal backPath={props.backPath} openModal={cancelModal} setOpenModal={setCancelModal} bookingNumber={props.bookingNumber} />
        <Button
            disabled={confirmButtonDisabled()}
            style={{backgroundColor: '#3CB371', color: 'white'}} onClick={() => {
            bookingService.fillBookingData({
                bookingNumber: props.bookingNumber||'',
                hoursCount: props.selectedDuration,
                promo: props.promo ? {
                    code: props.promo.code,
                    type:  props.promo.type,
                    value:  props.promo.value
                } as FillBookingDataCommandPromo : null,
                promoDiscount: props.promo ? getPromoDiscount(props.promo, props.totalAmount) : null,
                customer: {
                    name: props.name||'',
                    phoneNumber: props.phoneNumber||''
                },
                language: localStorage.getItem('language') || 'ru',
                costs: props.data.pointPrices.slice(0, props.selectedDuration/0.5).map((p) => {
                    return {
                        startHour: p.start,
                        endHour: p.end,
                        cost: p.value
                    } as FillBookingDataCommandCost
                }),
                inventories: props.selectedInventories.map((inv, i) => {
                    return {
                        inventoryId: inv.id,
                        price: inv.price,
                        quantity: inv.quantity,
                        amount: inv.price * inv.quantity * props.selectedDuration,
                    } as FillBookingDataCommandInventory
                })
            }).then(() => {
                navigate("/confirm",  {
                    state: {
                        bookingNumber: props.bookingNumber,
                        backPath: props.backPath
                    }
                });
            }).catch((error) => {
                if (error === 'booking:booking_intersection') {
                    navigate(props.backPath);
                }
            })
        }}>{t("booking:checkout:booking_button")}</Button>
        <Button style={{backgroundColor: '#CD5C5C', color: 'white'}} onClick={() => {
            setCancelModal(true);
        }}>{t("booking:checkout:cancel_button")}</Button>
    </div>
}