import {BaseEntityDto} from "../BaseEntityDto";

export interface BreakDto extends BaseEntityDto {
    id: number,
    name: string,
    description: string | null,
    isActive: boolean,
    dateStart: Date,
    dateEnd: Date | null,
    startHour: string,
    endHour: string,
    selectedFields: number[],
    fieldsCount: number
}