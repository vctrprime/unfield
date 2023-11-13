import {PromoCodeType} from "../../dto/rates/enums/PromoCodeType";

export interface FillBookingDataCommand {
    bookingNumber: string;
    hoursCount: number;
    promoDiscount: number | null;
    language: string;
    customer: FillBookingDataCommandCustomer;
    promo: FillBookingDataCommandPromo|null;
    costs: FillBookingDataCommandCost[];
    inventories: FillBookingDataCommandInventory[];
}

export interface FillBookingDataCommandCost {
    startHour: number;
    endHour: number;
    cost: number;
}

export interface FillBookingDataCommandInventory {
    inventoryId: number;
    price: number;
    quantity: number;
    amount: number;
}

export interface FillBookingDataCommandCustomer {
    name: string;
    phoneNumber: string;
}

export interface FillBookingDataCommandPromo {
    code: string;
    type: PromoCodeType;
    value: number;
}