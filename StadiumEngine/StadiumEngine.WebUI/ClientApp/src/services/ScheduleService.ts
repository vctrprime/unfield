import {ScheduleFieldDto} from "../models/dto/schedule/ScheduleFieldDto";
import {BaseService} from "./BaseService";

export interface IScheduleService {
    getFields(): Promise<ScheduleFieldDto[]>;
}

export class ScheduleService extends BaseService implements IScheduleService {
    constructor() {
        super("api/schedule");
    }

    getFields(): Promise<ScheduleFieldDto[]> {
        return this.fetchWrapper.get({
            url: `${this.baseUrl}/fields`
        })
    }
}