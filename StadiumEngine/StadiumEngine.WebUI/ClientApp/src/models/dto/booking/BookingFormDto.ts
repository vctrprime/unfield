import {FieldDto} from "../offers/FieldDto";

export interface BookingFormDto {
    fields: BookingFormFieldDto[];
}

export interface BookingFormFieldDto {
    data: FieldDto;
    minPrice: number;
    slots: BookingFormFieldSlotDto[];
}

export interface BookingFormFieldSlotDto {
    name: string;
    prices: BookingFormFieldSlotPriceDto[];
}

export interface BookingFormFieldSlotPriceDto {
    tariffName: string;
    value: number;
}