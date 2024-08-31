import {FieldDto} from "../offers/FieldDto";
import {AuthorizedCustomerDto} from "../customers/AuthorizedCustomerDto";

export interface BookingCheckoutDto {
    bookingNumber: string;
    day: string;
    stadiumName: string;
    tariffId: number;
    field: FieldDto;
    durationInventories: BookingCheckoutDurationInventoryDto[];
    durationAmounts: BookingCheckoutDurationAmountDto[];
    pointPrices: BookingCheckoutPointPriceDto[];
    customer?: AuthorizedCustomerDto;
}

export interface BookingCheckoutDurationAmountDto {
    duration: number;
    value: number;
}

export interface BookingCheckoutPointPriceDto {
    start: number;
    displayStart: string;
    end: number;
    displayEnd: string;
    value: number;
}

export interface BookingCheckoutDurationInventoryDto {
    duration: number;
    inventories: BookingCheckoutInventoryDto[];
}

export interface BookingCheckoutInventoryDto {
    id: number;
    name: string;
    quantity: number;
    price: number;
    image: string | null;
}