
export interface AuthorizeCustomerDto {
    firstName?: string;
    lastName?: string;
    phoneNumber: string;
    language: string;
    bookingNumber?: string;
}