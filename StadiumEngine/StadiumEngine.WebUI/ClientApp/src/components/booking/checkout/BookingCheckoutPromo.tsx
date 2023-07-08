import {Form, Icon} from "semantic-ui-react";
import {t} from "i18next";
import React, {useRef, useState} from "react";
import {BookingCheckoutDto} from "../../../models/dto/booking/BookingCheckoutDto";
import {PromoCodeDto} from "../../../models/dto/rates/TariffDto";
import {useInject} from "inversify-hooks";
import {IBookingFormService} from "../../../services/BookingFormService";

export interface BookingCheckoutPromoProps {
    data: BookingCheckoutDto;
    promo: PromoCodeDto|null;
    setPromo: Function;
}

type PromoMessage = {
    success: boolean;
    message: string;
}

export const BookingCheckoutPromo = (props: BookingCheckoutPromoProps) => {
    const promoInput = useRef<any>();

    const [bookingFormService] = useInject<IBookingFormService>('BookingFormService');
    
    const [promoMessage, setPromoMessage] = useState<PromoMessage|null>(null);
    
    return <>
        <Form.Field className="booking-checkout-promo">
            <input
                placeholder={t("booking:checkout:promo_placeholder")||''}
                disabled={props.promo !== null}
                ref={promoInput}/>
            <Icon
                name='check'
                disabled={props.promo !== null}
                onClick={() => {

                    if (promoInput.current?.value?.length === 0) {
                        setPromoMessage({
                            message: t("booking:checkout:promo_need"),
                            success: false
                        });
                        return;
                    }

                    bookingFormService.checkPromoCode( props.data?.tariffId, promoInput.current?.value).then((result) => {
                        const tariffPromo = result;
                        setPromoMessage(tariffPromo ? {
                            message: t("booking:checkout:promo_success"),
                            success: true
                        }: {
                            message: t("booking:checkout:promo_not_found"),
                            success: false
                        });

                        props.setPromo(tariffPromo ? tariffPromo : null);
                    }).catch(() => {
                        props.setPromo(null);
                    })
                }} />
            <Icon
                name='repeat'
                disabled={props.promo === null}
                onClick={() => {
                    setPromoMessage(null)
                    props.setPromo(null);
                    promoInput.current.value = '';
                }} />
        </Form.Field>
        {promoMessage && <label className="booking-checkout-promo-message" style={promoMessage.success ? { color: '#3CB371'} : {color: '#CD5C5C'}}>{promoMessage.message}</label>}
    </>
}