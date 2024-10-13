import {PromoCodeType} from "../models/dto/rates/enums/PromoCodeType";
import {PromoCodeDto} from "../models/dto/rates/TariffDto";
import {BookingCheckoutDto} from "../models/dto/bookings/BookingCheckoutDto";
import {BookingDto} from "../models/dto/bookings/BookingDto";
import {SelectedInventory} from "../components/account/bookings/common/BookingInventory";

export const getPromoDiscount = (promo: PromoCodeDto|null, value: number) => {
    if (promo) {
        switch (promo.type) {
            case PromoCodeType.Fixed:
                return promo.value;
            case PromoCodeType.Percent:
                return value * (promo.value/100);
        }
        return 0;
    }
    return 0;
}

export const getFieldAmount = (selectedDuration: number, data: BookingCheckoutDto|null) => {
    if (data) {
        const durationAmount = data.durationAmounts.find( x => x.duration === selectedDuration);

        if (durationAmount) {
            return durationAmount.value;
        }

        return 0;
    }

    return 0;
}

export const getFieldAmountValueByBooking = (booking: BookingDto) => {
    return booking.fieldAmount;
}

export const getInventoryAmount = (selectedDuration: number, selectedInventories: SelectedInventory[]) => {
    let result = 0;
    selectedInventories.map((inv) => {
        result += (inv.price * inv.quantity * selectedDuration);
    });

    return result;
}

export const getInventoryAmountByBooking = (booking: BookingDto) => {
    return booking.inventoryAmount;
}