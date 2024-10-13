import {BaseService} from "./BaseService";

export interface IAuthorizeService {
    logout(): Promise<void>;
}

export class AuthorizeService extends BaseService implements IAuthorizeService {
    constructor() {
        super("api/authorize");
    }
    
    logout(): Promise<void> {
        return this.fetchWrapper.delete({
            url: `${this.baseUrl}/logout`
        });
    }
}

