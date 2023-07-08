import React from 'react';
import {useInject} from "inversify-hooks";
import {IBookingFormService} from "../../../services/BookingFormService";
import {Button, Modal} from "semantic-ui-react";
import {t} from "i18next";
import {useNavigate} from "react-router-dom";

export const BookingCancelModal = ({openModal, setOpenModal, bookingNumber, backPath}: any) => {
    
    const [bookingFormService] = useInject<IBookingFormService>('BookingFormService');
    const navigate = useNavigate();
    
    return <Modal
        dimmer='blurring'
        size='small'
        open={openModal}>
        <Modal.Header>{t('booking:cancel:header')}</Modal.Header>
        <Modal.Content>
            <p>{t('booking:cancel:question')}</p>
        </Modal.Content>
        <Modal.Actions>
            <Button style={{backgroundColor: '#CD5C5C', color: 'white'}} onClick={() => {
                setOpenModal(false);
            }}>{t('common:no_button')}</Button>
            <Button style={{backgroundColor: '#3CB371', color: 'white'}} onClick={() => {
                setOpenModal(false);
                bookingFormService.cancelBooking({
                    bookingNumber: bookingNumber||''
                }).finally(() => {
                    navigate(backPath);
                });
            }}>{t('common:yes_button')}</Button>
        </Modal.Actions>
    </Modal>
}