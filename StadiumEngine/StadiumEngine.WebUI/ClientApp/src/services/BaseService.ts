import {useFetchWrapper} from "../helpers/fetch-wrapper";

export class BaseService {
    fetchWrapper: any;
    baseUrl: string;

    constructor(baseUrl: string) {
        this.fetchWrapper = useFetchWrapper();
        this.baseUrl = baseUrl;
    }
}