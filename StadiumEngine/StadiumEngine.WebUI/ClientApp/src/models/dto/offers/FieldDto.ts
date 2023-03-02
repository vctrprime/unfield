import {FieldCoveringType} from "./enums/FieldCoveringType";
import {SportKind} from "./enums/SportKind";
import {OfferDto} from "./OfferDto";

export interface FieldDto extends OfferDto {
    width: number;
    length: number;
    parentFieldId: number | null;
    coveringType: FieldCoveringType;
    isLastChild: boolean;
    childNames: string[];
    priceGroupId: number | null;
    priceGroupName: string | null;
}