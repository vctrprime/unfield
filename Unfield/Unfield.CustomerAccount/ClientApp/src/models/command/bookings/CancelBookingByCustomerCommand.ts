export interface CancelBookingByCustomerCommand {
    bookingNumber: string;
    day: Date|null;
    reason?: string;
    cancelOneInRow: boolean;
}