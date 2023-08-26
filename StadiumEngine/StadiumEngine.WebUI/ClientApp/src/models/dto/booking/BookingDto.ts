import {FieldDto} from "../offers/FieldDto";
import {TariffDto} from "../rates/TariffDto";
import {LockerRoomDto} from "../offers/LockerRoomDto";
import {BookingSource} from "./enums/BookingSource";
import {InventoryDto} from "../offers/InventoryDto";
import {PromoCodeType} from "../rates/enums/PromoCodeType";

export interface BookingDto {
    id: number;
    number: string;
    source: BookingSource;
    day: string;
    inventoryAmount: number;
    fieldAmount: number;
    totalAmountBeforeDiscount: number;
    totalAmountAfterDiscount: number;
    startHour: number;
    hoursCount: number;
    isDraft: boolean;
    isConfirmed: boolean;
    isCanceled: boolean;
    promo: BookingPromoDto | null;
    promoDiscount: number | null;
    manualDiscount: number | null;
    isWeekly: boolean;
    note: string | null;
    field: FieldDto;
    tariff: TariffDto;
    lockerRoom?: LockerRoomDto | null;
    customer: BookingCustomerDto;
    costs: BookingCostDto[];
    inventories: BookingInventoryDto[];
}

export interface BookingCustomerDto {
    id: number;
    name: string | null;
    phoneNumber: string | null;
}

export interface BookingCostDto {
    id: number;
    startHour: number;
    endHour: number;
    cost: number;
}

export interface BookingInventoryDto {
    id: number;
    price: number;
    quantity: number;
    amount: number;
    inventory: InventoryDto;
}

export interface BookingPromoDto {
    id: number;
    code: string;
    type: PromoCodeType;
    value: number;
}