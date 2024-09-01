import {StadiumGroupDto} from "../models/dto/admin/StadiumGroupDto";
import {BaseService} from "./BaseService";
import {AuthorizeUserDto} from "../models/dto/accounts/AuthorizeUserDto";

export interface IAdminService {
    getStadiumGroups(q: string | null): Promise<StadiumGroupDto[]>;

    changeStadiumGroup(stadiumGroupId: number): Promise<AuthorizeUserDto>;
}

export class AdminService extends BaseService implements IAdminService {
    constructor() {
        super("api/admin");
    }

    getStadiumGroups(q: string | null): Promise<StadiumGroupDto[]> {
        const endpoint = (q === null ?
            "/stadium-groups" :
            `/stadium-groups?q=${q}`);

        return this.fetchWrapper.get({
            url: `${this.baseUrl}${endpoint}`,
            withSpinner: false,
            hideSpinner: false
        })
    }

    changeStadiumGroup(stadiumGroupId: number): Promise<AuthorizeUserDto> {
        return this.fetchWrapper.put({
            url: `${this.baseUrl}/change-stadium-group/` + stadiumGroupId
        })
    }
}