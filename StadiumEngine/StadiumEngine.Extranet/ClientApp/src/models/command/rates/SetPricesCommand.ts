import {PriceDto} from "../../dto/rates/PriceDto";

export interface SetPricesCommand {
    prices: PriceDto[];
}