import {UIMessageDto} from "../models/dto/notifications/UIMessageDto";
import {BaseService} from "./BaseService";


export interface INotificationsService {
    getUIMessages(): Promise<UIMessageDto[]>;
    setLastReadMessage( messageId: number): Promise<void>;
}

export class NotificationsService extends BaseService implements INotificationsService {
    constructor() {
        super("api/notifications");
    }

    getUIMessages(): Promise<UIMessageDto[]> {
        return this.fetchWrapper.get({
            url: `${this.baseUrl}/ui-messages`,
            withSpinner: false,
            hideSpinner: false,
            showErrorAlert: false
        })
    }

    setLastReadMessage(messageId: number): Promise<void> {
        return this.fetchWrapper.post({
            url: `${this.baseUrl}/ui-messages/read`,
            body: {
                messageId
            },
            withSpinner: false,
            hideSpinner: false,
            showErrorAlert: false
        })
    }
}