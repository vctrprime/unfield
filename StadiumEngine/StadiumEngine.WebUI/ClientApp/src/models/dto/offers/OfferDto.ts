import {BaseEntityDto} from "../BaseEntityDto";
import {SportKind} from "./enums/SportKind";

export interface OfferDto extends BaseEntityDto {
    id: number;
    name: string;
    description: string|null;
    images: string[];
    sportKinds: SportKind[];
    isActive: boolean;
}