import {PromoCodeDto, TariffDayIntervalDto} from "../../dto/rates/TariffDto";

export interface AddTariffCommand {
    name: string;
    description: string | null;
    isActive: boolean;
    dateStart: Date;
    dateEnd: Date | null;
    monday: boolean;
    tuesday: boolean;
    wednesday: boolean;
    thursday: boolean;
    friday: boolean;
    saturday: boolean;
    sunday: boolean;
    dayIntervals: TariffDayIntervalDto[],
    promoCodes: PromoCodeDto[]
}