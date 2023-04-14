import React, {useState} from 'react';
import {BookingFormFieldDto, BookingFormFieldSlotDto} from "../../models/dto/booking/BookingFormDto";
import {Col} from "reactstrap";
import {Carousel} from "react-responsive-carousel";
import "react-responsive-carousel/lib/styles/carousel.min.css";
import {SportKind} from "../../models/dto/offers/enums/SportKind";
import {t} from "i18next";
import {Button, Grid, Header, Popup, SemanticWIDTHS} from "semantic-ui-react";
import {FieldCoveringType} from "../../models/dto/offers/enums/FieldCoveringType";

export interface FieldCardProps {
    field: BookingFormFieldDto
}

interface PopupSlotProps {
    slot: BookingFormFieldSlotDto
}

const PopupSlot = (props: PopupSlotProps) => (
    <Popup
        trigger={<div className="field-slot">{props.slot.name}</div>} flowing hoverable>
        <Grid centered divided columns={props.slot.prices.length as SemanticWIDTHS}>
            {props.slot.prices.map((p, i) => {
                return <Grid.Column key={i} textAlign='center'>
                    <Header as='h6' className="slot-popup-header">{p.tariffName}</Header>
                    <span style={{fontWeight: 'bold'}}>{props.slot.name}</span>
                    <p className="slot-popup-value">
                        {p.value}{t("booking:field_card:per_hour")}
                    </p>
                    <Button style={{backgroundColor: '#354650', color: 'white'}}>{t("booking:field_card:book")}</Button>
                </Grid.Column>
            })}
        </Grid>
    </Popup>
)

export const FieldCard = (props: FieldCardProps) => {
        return <Col xs={12} sm={12} md={6} lg={3} style={{float: 'left'}}>
        <div className="booking-form-field-card">
            <div className="field-covering">{t("offers:coverings:" + FieldCoveringType[props.field.data.coveringType].toLowerCase())}</div>
            {props.field.stadiumName !== null && <div className="field-stadium">{props.field.stadiumName}</div>}
            <div className="field-min-price">{t("booking:field_card:from")} {props.field.minPrice}{t("booking:field_card:per_hour")}</div>
            {props.field.data.images.length === 0 ? 
                <div className="field-carousel"></div> :
            <Carousel autoPlay={true} infiniteLoop={true} showThumbs={false} showStatus={false} className="field-carousel">
                {props.field.data.images.map((img, i) => {
                    return <img key={i} src={"/legal-images/" + img} />
                })}
            </Carousel>}
            <div className="field-name">
                {props.field.data.name} ({props.field.data.length}x{props.field.data.width})
            </div>
            <div className="field-description">{props.field.data.description}</div>
            <div className="field-sports">
                {props.field.data.sportKinds.length === 0 ? 
                <span style={{paddingLeft: '10px', fontSize: '12px', fontWeight: "bold"}}>{t("booking:field_card:no_sports")}</span> :
                    props.field.data.sportKinds.map((s, i) => {
                        const value = SportKind[s];
                        const text = t("offers:sports:" + value.toLowerCase());
                        
                        return <div key={i} className="field-sport">{text}</div>;
                    })}
            </div>
            <div className="field-slots">
                {props.field.slots.map((s, i) => {
                    return <PopupSlot slot={s} key={i} />;
                })}
            </div>
        </div>
    </Col>
    
}