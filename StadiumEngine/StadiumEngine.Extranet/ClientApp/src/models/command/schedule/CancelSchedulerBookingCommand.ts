export interface CancelSchedulerBookingCommand {
    bookingNumber: string;
    day: string;
    reason?: string;
    cancelOneInRow: boolean;
}