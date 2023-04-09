import React, {useState} from 'react';
import {BookingFormFieldDto, BookingFormFieldSlotDto} from "../../models/dto/booking/BookingFormDto";
import {Col} from "reactstrap";
import {Carousel} from "react-responsive-carousel";
import "react-responsive-carousel/lib/styles/carousel.min.css";
import {SportKind} from "../../models/dto/offers/enums/SportKind";
import {t} from "i18next";
import {Button} from "semantic-ui-react";
import {FieldCoveringType} from "../../models/dto/offers/enums/FieldCoveringType";

export interface FieldCardProps {
    field: BookingFormFieldDto,
    selectedSlot: BookingFormFieldSlotDto|null,
    setSelectedSlot: any
}

export const FieldCard = (props: FieldCardProps) => {
    
    
    
    return <Col xs={12} sm={6} md={4} lg={3} style={{float: 'left'}}>
        <div className="booking-form-field-card">
            <div className="field-covering">{t("offers:coverings:" + FieldCoveringType[props.field.data.coveringType].toLowerCase())}</div>
            {props.field.data.images.length === 0 ? 
                <div className="field-carousel"></div> :
            <Carousel showThumbs={false} showStatus={false} className="field-carousel">
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
                <span style={{paddingLeft: '10px'}}>Виды спорта не указаны</span> :
                    props.field.data.sportKinds.map((s, i) => {
                        const value = SportKind[s];
                        const text = t("offers:sports:" + value.toLowerCase());
                        
                        return <div key={i} className="field-sport">{text}</div>;
                    })}
            </div>
            <div className="field-slots">
                {props.field.slots.map((s, i) => {
                    const titles = s.prices.map((p, i) => {
                        return `${p.tariffName}: ${p.value}/час`
                    })
                    const title = titles.join('\r\n')
                    
                    return <div 
                        onClick={() => props.setSelectedSlot(s)}
                        style={ props.selectedSlot === s ? { backgroundColor: '#3CB371'} : {}} key={i} title={title} className="field-slot">{s.name}</div>;
                })}
            </div>
            <div className="field-buttons">
                <div className="field-min-price">от {props.field.minPrice}/час</div>
                <Button disabled={props.selectedSlot === null || props.field.slots.indexOf(props.selectedSlot) === -1} style={{backgroundColor: '#3CB371', color: 'white'}}>Забронировать</Button>
            </div>
        </div>
    </Col>
    
}