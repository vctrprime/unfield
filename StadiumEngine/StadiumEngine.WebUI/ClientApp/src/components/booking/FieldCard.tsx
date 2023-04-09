import React from 'react';
import {BookingFormFieldDto} from "../../models/dto/booking/BookingFormDto";

export interface FieldCardProps {
    field: BookingFormFieldDto
}

export const FieldCard = (props: FieldCardProps) => {
    return <div>{props.field.data.name}</div>
}