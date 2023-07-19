import {BaseEntityDto} from "../BaseEntityDto";


export interface UserDto extends BaseEntityDto {
    id: number;
    name: string;
    lastName: string;
    phoneNumber: string;
    roleId: number | null;
    roleName: string;
    lastLoginDate: string | null;
    stadiumsCount: number;
}
