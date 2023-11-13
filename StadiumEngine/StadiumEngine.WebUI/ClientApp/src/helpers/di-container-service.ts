import {container} from "inversify-hooks";
import {AccountsService, IAccountsService} from "../services/AccountsService";
import {AdminService, IAdminService} from "../services/AdminService";
import {IOffersService, OffersService} from "../services/OffersService";
import {IRatesService, RatesService} from "../services/RatesService";
import {ISettingsService, SettingsService} from "../services/SettingsService";
import {BookingService, IBookingService} from "../services/BookingService";
import {IScheduleService, ScheduleService} from "../services/ScheduleService";
import {INotificationsService, NotificationsService} from "../services/NotificationsService";

export function registerServices() {
    container.addRequest<IAccountsService>(AccountsService, 'AccountsService');
    container.addRequest<IAdminService>(AdminService, 'AdminService');
    container.addRequest<IOffersService>(OffersService, 'OffersService');
    container.addRequest<IRatesService>(RatesService, 'RatesService');
    container.addRequest<ISettingsService>(SettingsService, 'SettingsService');
    container.addRequest<IBookingService>(BookingService, 'BookingService');
    container.addRequest<IScheduleService>(ScheduleService, 'ScheduleService');
    container.addRequest<INotificationsService>(NotificationsService, 'NotificationsService');
}