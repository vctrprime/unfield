import {LockerRoomGender} from "./enums/LockerRoomGender";
import {BaseEntityDto} from "../BaseEntityDto";

export interface LockerRoomDto extends BaseEntityDto {
    id: number,
    name: string,
    description: string,
    gender: LockerRoomGender,
    isActive: boolean,
}