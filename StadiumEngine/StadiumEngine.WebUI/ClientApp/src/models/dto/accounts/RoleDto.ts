import {BaseEntityDto} from "../BaseEntityDto";

export interface RoleDto extends BaseEntityDto {
    id: number;
    name: string;
    description: string;
    usersCount: number;
}