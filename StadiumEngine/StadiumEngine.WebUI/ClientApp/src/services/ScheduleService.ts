import {BaseService} from "./BaseService";
import {SchedulerEventDto} from "../models/dto/schedule/SchedulerEventDto";
import {SchedulerFieldsDto} from "../models/dto/schedule/SchedulerFieldsDto";

export interface IScheduleService {
    getFields(): Promise<SchedulerFieldsDto>;
    getEvents(start: Date, end: Date): Promise<SchedulerEventDto[]>;
}

export class ScheduleService extends BaseService implements IScheduleService {
    constructor() {
        super("api/schedule");
    }

    getFields(): Promise<SchedulerFieldsDto> {
        return this.fetchWrapper.get({
            url: `${this.baseUrl}/fields`
        })
    }

    getEvents(start: Date, end: Date): Promise<SchedulerEventDto[]> {
        return this.fetchWrapper.get({
            url: `${this.baseUrl}/events?start=${start.toJSON()}&end=${end.toJSON()}&language=${localStorage.getItem('language') || 'ru'}`
        })
    }
}