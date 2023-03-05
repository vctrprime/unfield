import moment from "moment";

interface GridDateValueParams {
    value: Date
}

export function dateFormatter(params: GridDateValueParams) {
    if (params.value === null) return null;
    return moment(params.value).format('DD.MM.YYYY HH:mm');

}