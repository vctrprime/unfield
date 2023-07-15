import {BookingCheckoutDto, BookingCheckoutDurationAmountDto} from "../models/dto/booking/BookingCheckoutDto";
import {CheckoutDiscount} from "../components/booking/checkout/BookingCheckoutButtons";
import {SelectedInventory} from "../components/booking/common/BookingInventory";
import {PromoCodeType} from "../models/dto/rates/enums/PromoCodeType";
import {PromoCodeDto} from "../models/dto/rates/TariffDto";
import {BookingDto, BookingPromoDto} from "../models/dto/booking/BookingDto";


export const calculateDiscounts = (promo: PromoCodeDto|BookingPromoDto|null, data: BookingCheckoutDto, setDiscounts: Function) => {
    if (promo == null) {
        setDiscounts([]);
        return;
    }
    
    const calculatedDiscounts = [] as CheckoutDiscount[];
    data?.durationAmounts.map((a: BookingCheckoutDurationAmountDto) => {
        let discountValue = 0;
        switch (promo.type) {
            case PromoCodeType.Fixed:
                discountValue = promo.value;
                break;
            case PromoCodeType.Percent:
                discountValue = a.value * (promo.value/100);
                break;
        }
        calculatedDiscounts.push({
            duration: a.duration,
            value: discountValue
        });
    })

    setDiscounts(calculatedDiscounts);
}

export const getFieldAmountValue = (selectedDuration: number, data: BookingCheckoutDto|null, discounts: CheckoutDiscount[]) => {
    if (data) {
        const discount = discounts.find( x=> x.duration == selectedDuration);
        const durationAmount = data.durationAmounts.find( x => x.duration === selectedDuration);

        if (durationAmount) {
            if (discount && discount.value > 0) {
                return durationAmount.value - discount.value;
            }

            return durationAmount.value;
        }

        return 0;
    }

    return 0;
}

export const getFieldAmountValueByBooking = (booking: BookingDto) => {
    return booking.amount - getInventoryAmountByBooking(booking);
}

export const getInventoryAmount = (selectedDuration: number, selectedInventories: SelectedInventory[]) => {
    let result = 0;
    selectedInventories.map((inv) => {
        result += (inv.price * inv.quantity * selectedDuration);
    });

    return result;
}

export const getInventoryAmountByBooking = (booking: BookingDto) => {
    let result = 0;
    if (booking.inventories) {
        booking.inventories.map((inv) => {
            result += inv.amount;
        });
    }
    return result;
}