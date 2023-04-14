import {BaseService} from "./BaseService";
import {CityDto} from "../models/dto/geo/CityDto";

export interface IGeoService {
    getCities(q: string|null): Promise<CityDto[]>;
}

export class GeoService extends BaseService implements IGeoService {
    constructor() {
        super("api/geo");
    }

    getCities(q: string|null): Promise<CityDto[]> {
        let params = ``;
        if (q !== null) {
            params += `?q=${q}`
        }

        return this.fetchWrapper.get({
            url: `${this.baseUrl}/cities${params}`,
            withSpinner: false,
            hideSpinner: false
        })
    }
}