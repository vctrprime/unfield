import {FieldDto} from "../offers/FieldDto";

export interface BookingFormDto {
    fields: BookingFormFieldDto[];
}

export interface BookingFormFieldDto {
    data: FieldDto;
    minPrice: number;
    stadiumName: string|null;
    slots: BookingFormFieldSlotDto[];
}

export interface BookingFormFieldSlotDto {
    name: string;
    prices: BookingFormFieldSlotPriceDto[];
    enabled: boolean;
    disabledByMinDuration: boolean;
}

export interface BookingFormFieldSlotPriceDto {
    tariffId: number;
    tariffName: string;
    value: number;
}