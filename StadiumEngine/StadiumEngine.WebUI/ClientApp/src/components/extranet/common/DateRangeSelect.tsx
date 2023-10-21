import * as React from 'react'
import SemanticDatepicker from 'react-semantic-ui-datepickers';
import 'react-semantic-ui-datepickers/dist/react-semantic-ui-datepickers.css';
import {getLocale} from "../../../i18n/i18n";

export interface DateRangeSelectProps {
    value: Date[],
    onChange: any
}

export const DateRangeSelect = (props: DateRangeSelectProps) => {
    return <SemanticDatepicker 
        className="date-range-picker" 
        firstDayOfWeek={1} 
        datePickerOnly={true} 
        format={"DD.MM.YYYY"}
        value={props.value} 
        locale={getLocale()} onChange={props.onChange} type="range" />;
};