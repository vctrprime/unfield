import React, {useState} from "react";
import {useLocation, useNavigate, useParams} from "react-router-dom";
import {getTitle} from "../../../helpers/utils";
import {useInject} from "inversify-hooks";
import {IBookingFormService} from "../../../services/BookingFormService";
import {Container} from "reactstrap";
import '../../../css/booking/BookingCheckout.scss';
import { Button, Form } from "semantic-ui-react";
import {t} from "i18next";
import {BookingCancelModal} from "../common/BookingCancelModal";

type ConfirmLocationState = {
    bookingNumber: string;
    backPath: string;
}

export const BookingCheckoutConfirm = () => {
    document.title = getTitle("booking:confirm_title");
    
    const location = useLocation();
    const params = useParams();
    const navigate = useNavigate();
    
    let bookingNumber = (location.state as ConfirmLocationState)?.bookingNumber  || params["bookingNumber"];
    let backPath = (location.state as ConfirmLocationState)?.backPath || '/booking';

    const [bookingFormService] = useInject<IBookingFormService>('BookingFormService');
    
    const [code, setCode] = useState<string | undefined>();

    const confirmButtonDisabled = () => {
        return !(code?.length === 4);
    }

    const [cancelModal, setCancelModal] = useState<boolean>(false)
    
    return <Container className="booking-checkout-container" style={{paddingTop: '50px'}}>
        <BookingCancelModal backPath={backPath} openModal={cancelModal} setOpenModal={setCancelModal} bookingNumber={bookingNumber} />
        <Form>
            <div className="booking-checkout-header" style={{width: '100%', justifyContent: 'center'}}>
                <span>{t("booking:checkout:confirm_header")} {bookingNumber}</span>
            </div>
            <div className="booking-checkout-inputs">
                <Form.Field style={{width: '100%', textAlign: 'center'}}>
                    <label style={{
                        width: '90%',
                        fontWeight: 'normal',
                        marginLeft: '5%',
                        marginBottom: '20px'
                    }}>{t("booking:checkout:confirm_code_text")}</label>
                    <input
                        style={{ maxWidth: '400px'}}
                        value={code||''}
                        onChange={(e) => setCode(e.target.value)}
                        placeholder={t("booking:checkout:confirm_code_placeholder")||''}/>
                </Form.Field>
            </div>
            <div className="booking-checkout-buttons" style={{ justifyContent: 'center'}}>
                <Button 
                    disabled={confirmButtonDisabled()}
                    style={{backgroundColor: '#3CB371', color: 'white'}} onClick={() => {
                    bookingFormService.confirmBooking({
                        bookingNumber: bookingNumber||'',
                        accessCode: code||'',
                        language: localStorage.getItem('language') || 'ru',
                    }).then(() => {
                        navigate(backPath);
                    }).catch((error) => {
                        if (error === 'booking:booking_intersection') {
                            navigate(backPath);
                        }
                    })
                }}>{t("booking:checkout:confirm_button")}</Button>
                <Button style={{backgroundColor: '#CD5C5C', color: 'white'}} onClick={() => {
                    setCancelModal(true);
                }}>{t("booking:checkout:cancel_button")}</Button>
            </div>
        </Form>
        <div className="booking-form-footer" style={{ position: 'fixed', bottom: 0}}>{t('common:footer')}</div>
    </Container>
}