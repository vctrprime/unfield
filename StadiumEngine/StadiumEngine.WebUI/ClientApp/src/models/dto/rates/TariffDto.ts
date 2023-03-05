import {BaseEntityDto} from "../BaseEntityDto";

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
    dayIntervals: string[][]
}