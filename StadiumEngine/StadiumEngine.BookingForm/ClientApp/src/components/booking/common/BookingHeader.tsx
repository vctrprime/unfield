import React from "react";
import {t} from "i18next";
import {SportKind} from "../../../models/dto/offers/enums/SportKind";
import {BookingCheckoutDto} from "../../../models/dto/booking/BookingCheckoutDto";

export interface BookingHeaderProps {
    data: BookingCheckoutDto;
    withStadiumName: boolean;
    dayText?: string|null;
    withCurrentDate?: boolean;
}

export const BookingHeader = (props: BookingHeaderProps) => {
    return <>
        <div className="booking-checkout-header">
            <span>№ {props.data.bookingNumber}</span>
            <div style={{display: "flex", flexDirection: 'column', alignItems: 'flex-end'}}>
                <span>{props.dayText ? props.dayText : props.data.day}</span>
                {props.withCurrentDate && props.dayText && <span style={{ fontSize: 10, marginTop: -7}}>{props.data.day}</span>}
            </div>
        </div>
        {props.withStadiumName && <div className="booking-checkout-stadium">
            {props.data.stadiumName}
        </div>}
        <div className="booking-checkout-field">
            {props.data.field.images.length ?
                <img src={"/legal-images/" + props.data.field.images[0]}/>  : <span/>
            }
            <div className="booking-checkout-field-text">
                <div className="booking-checkout-field-name">{props.data.field.name}</div>
                <div className="booking-checkout-field-description">{props.data.field.description}</div>
                <div className="booking-checkout-field-sports">
                    {props.data.field.sportKinds.length === 0 ?
                        <span style={{paddingLeft: '10px', fontSize: '12px', fontWeight: "bold"}}>{t("booking:field_card:no_sports")}</span> :
                        props.data.field.sportKinds.map((s, i) => {
                            const value = SportKind[s];
                            const text = t("offers:sports:" + value.toLowerCase());

                            return <div style={ i === 0 ? { marginLeft: 0} : {}} key={i} className="field-sport">{text}</div>;
                        })}
                </div>
            </div>
        </div>
    </>
}