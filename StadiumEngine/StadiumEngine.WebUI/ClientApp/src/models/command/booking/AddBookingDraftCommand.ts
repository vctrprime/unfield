import {BookingSource} from "../../dto/booking/enums/BookingSource";

export interface AddBookingDraftCommand {
    day: string;
    source: BookingSource;
    hour: number;
    fieldId: number;
    tariffId: number;
}