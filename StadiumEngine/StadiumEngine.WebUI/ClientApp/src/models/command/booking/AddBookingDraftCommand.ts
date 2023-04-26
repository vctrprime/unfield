export interface AddBookingDraftCommand {
    day: Date;
    slot: string;
    fieldId: number;
    tariffId: number;
}