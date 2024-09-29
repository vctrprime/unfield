import {PriceOfferDto} from "./PriceOfferDto";

export interface InventoryDto extends PriceOfferDto {
    quantity: number;
}