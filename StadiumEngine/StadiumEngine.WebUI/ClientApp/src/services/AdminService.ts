import {LegalDto} from "../models/dto/admin/LegalDto";
import {BaseService} from "./BaseService";
import {AuthorizeUserDto} from "../models/dto/accounts/AuthorizeUserDto";

export interface IAdminService {
    getLegals(q: string|null) : Promise<LegalDto[]>;
    changeLegal(legalId: number) : Promise<AuthorizeUserDto>;
}

export class AdminService extends BaseService implements IAdminService {
    constructor() {
        super("api/admin");
    }

    getLegals(q: string | null): Promise<LegalDto[]> {
        const endpoint = (q === null ? 
            "/legals" :
            `/legals?q=${q}`);
        
        return this.fetchWrapper.get({
            url: `${this.baseUrl}${endpoint}`,
            withSpinner: false,
            hideSpinner: false
        })
    }

    changeLegal(legalId: number): Promise<AuthorizeUserDto> {
        return this.fetchWrapper.put({
            url: `${this.baseUrl}/change-legal/` + legalId
        })
    }
}