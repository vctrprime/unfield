import {t} from "i18next";

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