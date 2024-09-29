import {OfferDto} from "./OfferDto";
import {Currency} from "./enums/Currency";

export interface PriceOfferDto extends OfferDto {
    price: number;
    currency: Currency;
}