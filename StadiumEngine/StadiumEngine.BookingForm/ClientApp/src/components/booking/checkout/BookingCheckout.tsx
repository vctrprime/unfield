import React, {useEffect, useState} from "react";
import {useLocation, useNavigate, useParams} from "react-router-dom";
import {
    BookingCheckoutDto
} from "../../../models/dto/booking/BookingCheckoutDto";
import {useInject} from "inversify-hooks";
import {IBookingService} from "../../../services/BookingService";
import {Container} from "reactstrap";
import '../../../css/booking/BookingCheckout.scss';
import {PromoCodeDto} from "../../../models/dto/rates/TariffDto";
import {getTitle} from "../../../helpers/utils";
import {Form} from "semantic-ui-react";
import {t} from "i18next";
import {BookingHeader} from "../common/BookingHeader";
import {BookingDuration} from "../common/BookingDuration";
import {BookingFieldAmount} from "../common/BookingFieldAmount";
import {BookingCheckoutPromo} from "./BookingCheckoutPromo";
import {BookingInventory, SelectedInventory} from "../common/BookingInventory";
import {BookingTotalAmount} from "../common/BookingTotalAmount";
import {BookingCustomer} from "../common/BookingCustomer";
import {BookingCheckoutButtons, CheckoutDiscount} from "./BookingCheckoutButtons";
import {
    getFieldAmount,
    getInventoryAmount
} from "../../../helpers/booking-utils";

type CheckoutLocationState = {
    bookingNumber: string;
    backPath: string;
}



export const BookingCheckout = () => {
    document.title = getTitle("booking:checkout_title");
    
    const location = useLocation();
    const params = useParams();
    
    let bookingNumber = (location.state as CheckoutLocationState)?.bookingNumber || params["bookingNumber"]
    let backPath = (location.state as CheckoutLocationState)?.backPath || '/booking';
    
    const [bookingService] = useInject<IBookingService>('BookingService');
    
    const [data, setData] = useState<BookingCheckoutDto|null>(null);
    
    const [promo, setPromo] = useState<PromoCodeDto|null>(null);
    
    const [selectedDuration, setSelectedDuration] = useState<number>(1);
    const [selectedInventories, setSelectedInventories] = useState<SelectedInventory[]>([]);
    
    const navigate = useNavigate();
    
    useEffect(() => {
        bookingService.getBookingCheckout(bookingNumber as string, false).then((response: BookingCheckoutDto) => {
            setData(response);
        })
            .catch((error) => {
                navigate(backPath);
            })
    }, [])
    
    const getCheckoutFieldAmount = () => {
        return getFieldAmount(selectedDuration, data);
    }
    
    const getCheckoutInventoryAmount = () => {
       return getInventoryAmount(selectedDuration, selectedInventories);
    }
    
    const getTotalAmountValue = () => {
        return getCheckoutFieldAmount() + getCheckoutInventoryAmount();
    }

    const [phoneNumber, setPhoneNumber] = useState<string | undefined>();
    const [name, setName] = useState<string | undefined>();
    
    return data === null  ? null :  <Container className="booking-checkout-container">
        <Form style={{paddingBottom: '10px', minHeight: 'calc(100vh - 30px)'}}>
            <BookingHeader data={data} withStadiumName={true} />
            <BookingDuration 
                isEditable={true}
                data={data} 
                selectedDuration={selectedDuration} 
                setSelectedDuration={setSelectedDuration} />
            <BookingFieldAmount getFieldAmount={getCheckoutFieldAmount}/>
            <BookingInventory 
                data={data} 
                isEditable={true}
                selectedDuration={selectedDuration} 
                selectedInventories={selectedInventories} 
                setSelectedInventories={setSelectedInventories} 
                getInventoryAmount={getCheckoutInventoryAmount}
                bookingInventories={[]}
                headerText={t("booking:checkout:inventory_header")}/>
            <BookingTotalAmount getTotalAmountValue={getTotalAmountValue} promo={promo}/>
            <BookingCheckoutPromo data={data} promo={promo} setPromo={setPromo} />
            <BookingCustomer 
                name={name} 
                setName={setName} 
                phoneNumber={phoneNumber} 
                setPhoneNumber={setPhoneNumber} 
                headerText={t("booking:checkout:inputs_header")} />
            <BookingCheckoutButtons 
                backPath={backPath} 
                bookingNumber={bookingNumber} 
                data={data} 
                selectedDuration={selectedDuration}
                promo={promo} 
                name={name}
                phoneNumber={phoneNumber} 
                selectedInventories={selectedInventories} 
                totalAmount={getTotalAmountValue()}/>
        </Form>
        <div className="booking-form-footer">{t('common:footer')}</div>
    </Container>
}