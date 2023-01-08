import moment from "moment";


export function dateFormatter(params) {
    if (params.value === null) return null;
    return moment(params.value).format('DD.MM.YYYY HH:mm');
    
}