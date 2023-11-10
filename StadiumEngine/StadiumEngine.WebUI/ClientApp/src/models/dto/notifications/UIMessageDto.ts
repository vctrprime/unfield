import {UIMessageType} from "./enums/UIMessageType";

export interface UIMessageDto {
    id: number;
    messageType: UIMessageType;
    isRead: boolean;
    dateCreated: Date;
    dateModified: Date|null;
    texts: UIMessageTextDto[]
}

export interface UIMessageTextDto {
    index: number;
    text: string;
}
