import {BaseService} from "./BaseService";
import {EnvDataDto} from "../models/dto/EnvDataDto";

export interface ICommonService {
    getEnvData(): Promise<EnvDataDto>;
}

export class CommonService extends BaseService implements ICommonService {
    constructor() {
        super("api");
    }

    getEnvData(): Promise<EnvDataDto> {
        return this.fetchWrapper.get({
            url: `${this.baseUrl}/env`
        })
    }
}