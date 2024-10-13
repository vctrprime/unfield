import {container} from "inversify-hooks";
import {BookingService, IBookingService} from "../services/BookingService";
import {GeoService, IGeoService} from "../services/GeoService";
import {CommonService, ICommonService} from "../services/CommonService";
import {AuthorizeService, IAuthorizeService} from "../services/AuthorizeService";

export function registerServices() {
    container.addRequest<IGeoService>(GeoService, 'GeoService');
    container.addRequest<IBookingService>(BookingService, 'BookingService');
    container.addRequest<ICommonService>(CommonService, 'CommonService');
    container.addRequest<IAuthorizeService>(AuthorizeService, 'AuthorizeService');
}