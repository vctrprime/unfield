import {TariffDto} from "../rates/TariffDto";
import {FieldDto} from "../offers/FieldDto";
import {InventoryDto} from "../offers/InventoryDto";


export interface BookingCheckoutDto {
    bookingNumber: string;
    day: string;
    stadiumName: string;
    tariff: TariffDto;
    field: FieldDto;
    inventories: InventoryDto[];
    durationAmounts: BookingCheckoutDurationAmountDto[];
    pointPrices: BookingCheckoutPointPriceDto[];
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