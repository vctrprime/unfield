import {BaseEntityDto} from "../BaseEntityDto";
import {FieldDto} from "../offers/FieldDto";

export interface BreakDto extends BaseEntityDto {
    id: number,
    name: string,
    description: string | null,
    isActive: boolean,
    dateStart: Date,
    dateEnd: Date | null,
    startHour: string,
    endHour: string,
    fields: FieldDto[],
    fieldsCount: number
}