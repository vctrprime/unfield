export interface RegisterCustomerCommand {
    phoneNumber: string;
    firstName?: string;
    lastName?: string;
    language: string;
}