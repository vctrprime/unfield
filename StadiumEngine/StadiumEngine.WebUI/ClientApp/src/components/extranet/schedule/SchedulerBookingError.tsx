import React from "react";
import {Icon} from "semantic-ui-react";

interface SchedulerBookingErrorProps {
    message: string|null;
}

export const SchedulerBookingError = ({message}: SchedulerBookingErrorProps) => {
    return <div style={{
        width: '100%', 
        height: 120, 
        backgroundColor: 'rgba(245, 245, 245, 0.3)', 
        display: "flex", 
        justifyContent: 'center', 
        alignItems: 'center',
        flexDirection: 'column'}}>
        <Icon name='warning circle' style={{fontSize: 40, color: '#CD5C5C'}}/>
        <span style={{marginTop: 10, textAlign: "center", fontSize: 15}}>{message}</span>
    </div>
}