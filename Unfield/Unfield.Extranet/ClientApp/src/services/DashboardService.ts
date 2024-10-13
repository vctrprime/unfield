import {BaseService} from "./BaseService";
import {StadiumDashboardDto} from "../models/dto/dashboard/StadiumDashboardDto";

export interface IDashboardService {
    getStadiumDashboard(): Promise<StadiumDashboardDto>;
}

export class DashboardService extends BaseService implements IDashboardService {
    constructor() {
        super("api/dashboard");
    }

    getStadiumDashboard(): Promise<StadiumDashboardDto> {
        return this.fetchWrapper.get({
            url: `${this.baseUrl}/stadium`
        })
    }
}