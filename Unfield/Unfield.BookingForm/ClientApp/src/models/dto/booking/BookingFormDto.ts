import {FieldDto} from "../offers/FieldDto";
import {AuthorizedCustomerDto} from "../customers/AuthorizedCustomerDto";

export interface BookingFormDto {
    fields: BookingFormFieldDto[];
    customer?: AuthorizedCustomerDto;
}

export interface BookingFormFieldDto {
    data: FieldDto;
    minPrice: number;
    stadiumName: string|null;
    slots: BookingFormFieldSlotDto[];
}

export interface BookingFormFieldSlotDto {
    name: string;
    hour: number;
    prices: BookingFormFieldSlotPriceDto[];
    enabled: boolean;
    disabledByMinDuration: boolean;
}

export interface BookingFormFieldSlotPriceDto {
    tariffId: number;
    tariffName: string;
    value: number;
}