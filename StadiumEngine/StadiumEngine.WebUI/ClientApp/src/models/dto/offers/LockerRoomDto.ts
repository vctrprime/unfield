import {LockerRoomGender} from "./enums/LockerRoomGender";

export interface LockerRoomDto {
    id: number,
    name: string,
    description: string,
    gender: LockerRoomGender,
    isActive: boolean,
}