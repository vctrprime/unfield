import {FieldCoveringType} from "./enums/FieldCoveringType";
import {SportKind} from "./enums/SportKind";
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
    sportKinds: SportKind[];
    isActive: boolean;
    IsLastChild: boolean;
    childNames: string[];
}