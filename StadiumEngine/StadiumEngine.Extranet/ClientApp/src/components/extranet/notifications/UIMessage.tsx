import React from 'react';
import {UIMessageDto} from "../../../models/dto/notifications/UIMessageDto";
import {UIMessageType} from "../../../models/dto/notifications/enums/UIMessageType";
import {t} from "i18next";
import {dateFormatterByDate} from "../../../helpers/date-formatter";

export interface UIMessageProps {
    message: UIMessageDto
}

const NewBookingMessage = ({message}: UIMessageProps) => {
    return <div style={{display: 'flex', flexDirection: 'column', padding: 4, marginTop: -10}}>
        <span>{t('notifications:ui_messages:new_booking:header')} <b>{message.texts[0].text}</b></span>
        <span>{t('notifications:ui_messages:new_booking:date')} <b>{message.texts[1].text}</b></span>
    </div>
}

const CancelBookingMessage = ({message}: UIMessageProps) => {
    return <div style={{display: 'flex', flexDirection: 'column', padding: 4, marginTop: -10}}>
        <span>{t('notifications:ui_messages:cancel_booking_by_customer:header_' + message.texts[3].text)} <b>{message.texts[0].text}</b></span>
        <span>{t('notifications:ui_messages:cancel_booking_by_customer:date')} <b>{message.texts[1].text}</b></span>
        <span>{t('notifications:ui_messages:cancel_booking_by_customer:reason')} <b>{message.texts[2].text}</b></span>
    </div>
}

export const UIMessage = ({message}: UIMessageProps) => {
    return <div className="ui-message" style={ message.isRead ? {} : { backgroundColor: 'rgba(0 ,210, 255, 0.05)'}}>
        <div className="ui-message-date">{dateFormatterByDate(message.dateCreated)}</div>
        {message.messageType === UIMessageType.BookingFromForm && <NewBookingMessage message={message}/> }
        {message.messageType === UIMessageType.CancelBookingByCustomer && <CancelBookingMessage message={message}/> }
    </div>
}