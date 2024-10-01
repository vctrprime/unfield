import {BookingSource} from "./enums/BookingSource";
import {BookingStatus} from "./enums/BookingStatus";

export interface CustomerBookingListItemDto {
    id: number;
    number: string;
    source: BookingSource;
    day: Date | null;
    closedDay: Date | null;
    time: string;
    duration: string;
    tariffName: string;
    fieldName: string;
    isWeekly: boolean;
    lockerRoomName: string | null;
    promoCode: string | null;
    promoValue: number | null;
    manualDiscount: number | null;
    totalAmountBeforeDiscount: number;
    totalAmountAfterDiscount: number;
    status: BookingStatus;
    start: Date;
    end: Date;
}