import {BaseEntityDto} from "../BaseEntityDto";
import {Nu} from "react-flags-select";
import {PromoCodeType} from "./enums/PromoCodeType";

export interface TariffDto extends BaseEntityDto {
    id: number,
    name: string,
    description: string | null,
    isActive: boolean,
    dateStart: Date,
    dateEnd: Date | null,
    monday: boolean,
    tuesday: boolean,
    wednesday: boolean,
    thursday: boolean,
    friday: boolean,
    saturday: boolean,
    sunday: boolean,
    dayIntervals: TariffDayIntervalDto[],
    promoCodes: PromoCodeDto[]
}

export interface TariffDayIntervalDto {
    tariffDayIntervalId?: number;
    interval: string[]
}

export interface PromoCodeDto {
    code: string,
    type: PromoCodeType,
    value: number
}