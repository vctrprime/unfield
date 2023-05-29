export interface FillBookingDataCommand {
    bookingNumber: string;
    hoursCount: number;
    amount: number;
    promoCode: string | null;
    discount: number | null;
    customer: FillBookingDataCommandCustomer;
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