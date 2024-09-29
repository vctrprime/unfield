import {BookingSource} from "./enums/BookingSource";
import {BookingStatus} from "./enums/BookingStatus";
import {BookingDto} from "./BookingDto";


export interface BookingListItemDto {
    id: number;
    number: string;
    source: BookingSource;
    day: Date | null;
    closedDay: Date | null;
    time: string;
    duration: string;
    customerName: string | null;
    customerPhoneNumber: string | null;
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
    originalData: BookingDto;
    start: Date;
    end: Date;
}