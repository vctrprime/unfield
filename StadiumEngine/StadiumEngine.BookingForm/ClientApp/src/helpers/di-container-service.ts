import {container} from "inversify-hooks";
import {BookingService, IBookingService} from "../services/BookingService";
import {GeoService, IGeoService} from "../services/GeoService";

export function registerServices() {
    container.addRequest<IGeoService>(GeoService, 'GeoService');
    container.addRequest<IBookingService>(BookingService, 'BookingService');
}