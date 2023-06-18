import {FieldDto} from "../offers/FieldDto";


export interface SchedulerFieldsDto {
    startHour: number;
    endHour: number;
    data: SchedulerFieldDto[]
}

export interface SchedulerFieldDto {
    field_id: number;
    name: string;
    data: FieldDto;
}