import React from "react";
import {t} from "i18next";

export interface SimpleAlertProps {
    message: string;
    style?: any;
}

export const SimpleAlert = ({message, style} : SimpleAlertProps) => {
    return (<div style={style ? style : { width: '100%', textAlign: 'left', fontSize: 12}}>
        <i style={{ color: '#00d2ff', marginRight: 3}} className="fa fa-exclamation-circle" aria-hidden="true"/>
        {t(message||'')}
    </div>)
}