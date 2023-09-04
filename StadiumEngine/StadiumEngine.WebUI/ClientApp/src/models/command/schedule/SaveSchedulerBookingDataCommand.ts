export interface SaveSchedulerBookingDataCommand {
    isNew: boolean;
    autoLockerRoom: boolean;
    bookingNumber: string;
    hoursCount: number;
    manualDiscount: number | null;
    language: string;
    isWeekly: boolean;
    editOneInRow: boolean;
    lockerRoomId: number | null;
    tariffId: number;
    day: string;
    customer: SaveSchedulerBookingDataCommandCustomer;
    costs: SaveSchedulerBookingDataCommandCost[];
    inventories: SaveSchedulerBookingDataCommandInventory[];
}

export interface SaveSchedulerBookingDataCommandCost {
    startHour: number;
    endHour: number;
    cost: number;
}

export interface SaveSchedulerBookingDataCommandInventory {
    inventoryId: number;
    price: number;
    quantity: number;
    amount: number;
}

export interface SaveSchedulerBookingDataCommandCustomer {
    name: string;
    phoneNumber: string;
}