import React, {useState} from "react";
import {AuthorizedCustomerDto} from "../../models/dto/customers/AuthorizedCustomerDto";
import {envAtom} from "../../state/env";
import {useRecoilValue} from "recoil";
import {Button, Modal, Popup} from "semantic-ui-react";
import {t} from "i18next";
import i18n from "../../i18n/i18n";
import {useInject} from "inversify-hooks";
import {IAuthorizeService} from "../../services/AuthorizeService";
import {Avatar, Box, IconButton, Menu, MenuItem, Tooltip, Typography} from "@mui/material";

export interface CustomerAccountButtonProps {
    stadiumToken?: string;
    customer?: AuthorizedCustomerDto;
    setChangeCustomer: Function;
}

export const CustomerAccountButton = (props: CustomerAccountButtonProps) => {
    const env = useRecoilValue(envAtom);
    const [signInModal, setSignInModal] = useState<boolean>(false);
    
    const lng = i18n.language;

    //@ts-ignore
    const signInCallback = (event) => {
        if (event.origin !== env?.customerAccountHost) return;
        
        if ( signInModal ) {
            props.setChangeCustomer(true);
            setSignInModal(false);
        }
    }
    
    window.addEventListener("message", signInCallback, false);

    const [authorizeService] = useInject<IAuthorizeService>('AuthorizeService');
    const logout = () => {
        authorizeService.logout().then(() => {
            props.setChangeCustomer(true);
        })
    }
    
    return props.stadiumToken ? 
        <div className="customer-account-button">
            <Modal
                dimmer='blurring'
                size='small'
                open={signInModal}>
                <Modal.Header>{t('booking:customer:sign_in:header')}</Modal.Header>
                <Modal.Content className="iframe-modal-content">
                    <iframe style={{height: '350px', width: '350px'}} src={`${env?.customerAccountHost}/${props.stadiumToken}/sign-in?withoutName=true&lng=${lng}&source=booking-form`}/>
                </Modal.Content>
                <Modal.Actions>
                <Button onClick={() => setSignInModal(false)}>{t('common:close_button')}</Button>
                </Modal.Actions>
            </Modal>
            
            {props.customer ?
                <Popup
                    trigger={<Avatar sx={{ width: 32, height: 32 }} style={{fontSize: '14px', cursor: 'pointer'}} alt={props.customer?.displayName} src="/static/images/avatar/no.jpg" />} flowing hoverable>
                    <div className="customer-account-menu">
                        <div className="go-to-account" onClick={() =>
                            window.open(`${env?.customerAccountHost}/${props.stadiumToken}/bookings/future`)
                        }>{t('booking:customer:go_to_account')}</div>
                        <div className="logout" onClick={logout}>{t('booking:customer:logout')}</div>
                    </div>
                </Popup>
                    : 
             <span className="sign-in" onClick={() => setSignInModal(true)}>{t('booking:customer:sign_in:button')}</span>}
        </div> : 
        <span />;

}