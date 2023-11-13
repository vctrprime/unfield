import {BaseEntityDto} from "../BaseEntityDto";

export interface PriceGroupDto extends BaseEntityDto {
    id: number,
    name: string,
    description: string,
    isActive: boolean,
    fieldNames: string[]
}