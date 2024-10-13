import {t} from "i18next";
import {BookingDto} from "../models/dto/bookings/BookingDto";
import {dateFormatterByDateWithoutTime} from "./date-formatter";

export function getTitle(path: string): string {
    return t(path) + " - Личный кабинет";
}

export function getAuthUrl(baseUrl: string, 
                           withoutName: string|null,
                           lng: string|null,
                           source: string|null) {
    let url = baseUrl;

    const params = new URLSearchParams();
    if (withoutName) params.append('withoutName', withoutName);
    if (lng) params.append('lng', lng);
    if (source) params.append('source', source);

    if ( params.size > 0 ) url += '?';
    
    return url + params.toString();
}


export const StringFormat = (str: string, ...args: string[]) =>
    str.replace(/{(\d+)}/g, (match, index) => args[index] || '')


export const validateInputs = (inputs: any[]) => {
    let hasErrors = false;
    inputs.forEach((input) => {
        if (!input.current?.value) {
            input.current.style.border = "1px solid red";
            setTimeout(() => {
                input.current.style.border = "";
            }, 2000);
            hasErrors = true;
        }
    })
    return !hasErrors;
}

export const getDurationText = (duration: number) => {
    let result = '';

    const str = duration.toString().replaceAll(",", ".");
    if (str.indexOf(".5") !== -1) {
        result += `${str.split(".")[0]} ${t("common:hour_short")} 30 ${t("common:minute_short")}`;
    }
    else {
        result += `${str.split(".")[0]} ${t("common:hour_short")}`;
    }

    return result;
}

export const getWeeklyClosedText = (booking: BookingDto) => {
    const dates = [];
    if (booking.isWeeklyStoppedDate) {
        dates.push(booking.isWeeklyStoppedDate);
    }

    if (booking.tariff.dateEnd) {
        dates.push(booking.tariff.dateEnd)
    }

    if (dates.length === 0) {
        return '';
    }

    const min = dates.reduce(function (a, b) { return a < b ? a : b; });

    return t("common:to") + ' ' + dateFormatterByDateWithoutTime(min);

}