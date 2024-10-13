import {t} from "i18next";
import {Col} from "reactstrap";
import {Form} from "semantic-ui-react";
import PhoneInput from "react-phone-input-2";
import i18n from "../../../i18n/i18n";
import React from "react";
import ru from 'react-phone-input-2/lang/ru.json'

export interface BookingCustomerProps {
    name: string | undefined;
    setName: Function;
    phoneNumber: string | undefined;
    setPhoneNumber: Function;
    headerText: string;
    isReadonly?: boolean;
}

export const BookingCustomer = (props: BookingCustomerProps) => {
    return <div className="booking-checkout-inputs">
        <label>{props.headerText}</label>
        <Col xs={12} sm={7} md={7} lg={7} style={{float: 'left', padding: '0 5px'}}>
            <Form.Field style={{width: '100%'}}>
                <input
                    value={props.name||''}
                    readOnly={props.isReadonly}
                    onChange={(e) => props.setName(e.target.value)}
                    placeholder={t("booking:checkout:surname")||''}/>
            </Form.Field>
        </Col>
        <Col xs={12} sm={5} md={5} lg={5} style={{float: 'left', padding: '0 5px'}}>
            <Form.Field style={{width: '100%'}}>
                <PhoneInput
                    onlyCountries={['ru']}
                    country='ru'
                    disabled={props.isReadonly}
                    containerStyle={{width: '100%'}}
                    inputStyle={{width: '100%', height: 38, paddingLeft: '42px', fontFamily: 'inherit'}}
                    placeholder={'+7 (123) 456-78-90'}
                    value={props.phoneNumber}
                    onChange={(phone) => props.setPhoneNumber(phone)}
                    localization={i18n.language === 'ru' ? ru : undefined}
                    countryCodeEditable={false}
                />
            </Form.Field>
        </Col>
    </div>
}