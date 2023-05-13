import {TariffDto} from "../rates/TariffDto";
import {FieldDto} from "../offers/FieldDto";
import {InventoryDto} from "../offers/InventoryDto";


export interface BookingCheckoutDto {
    bookingNumber: string;
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
    end: number;
    value: number;
}