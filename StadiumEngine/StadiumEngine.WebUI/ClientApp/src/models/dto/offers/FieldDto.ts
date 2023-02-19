import {FieldCoveringType} from "./enums/FieldCoveringType";
import {FieldSportKind} from "./enums/FieldSportKind";
import {BaseEntityDto} from "../BaseEntityDto";

export interface FieldDto extends BaseEntityDto {
    id: number;
    name: string;
    description: string|null;
    images: string[];
    width: number;
    length: number;
    parentFieldId: number | null;
    coveringType: FieldCoveringType;
    sportKinds: FieldSportKind[];
    isActive: boolean;
    IsLastChild: boolean;
}