import {ProcessedEvent} from "react-scheduler/types";
import {BookingDto} from "../booking/BookingDto";

export interface SchedulerEventDto extends ProcessedEvent {
    data?: BookingDto | null;
    sourceBooking: number;
}