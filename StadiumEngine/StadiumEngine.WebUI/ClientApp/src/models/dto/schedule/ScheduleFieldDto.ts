import {FieldDto} from "../offers/FieldDto";

export interface ScheduleFieldDto {
    field_id: number;
    name: string;
    data: FieldDto;
}