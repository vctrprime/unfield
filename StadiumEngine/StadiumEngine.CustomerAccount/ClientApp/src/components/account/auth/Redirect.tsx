import React, {useEffect} from "react";
import {useNavigate, useParams} from "react-router-dom";
import back_balls from "../../../img/back-balls.png";
import {IAccountsService} from "../../../services/AccountsService";
import {useInject} from "inversify-hooks";
import {authAtom} from "../../../state/auth";
import {useSetRecoilState} from "recoil";
import {AuthorizeCustomerDto} from "../../../models/dto/accounts/AuthorizeCustomerDto";

export const Redirect = () => {
    let { lng, token } = useParams();

    const setAuth = useSetRecoilState(authAtom);
    const [accountsService] = useInject<IAccountsService>('AccountsService');
    const navigate = useNavigate();
    
    useEffect(() => {
        accountsService.authorizeByRedirect({
            token: token ?? '',
            language: lng ?? 'ru',
        }).then((result) => {
            const customer: AuthorizeCustomerDto = {
                firstName: result.firstName,
                lastName: result.lastName,
                language: result.language,
                phoneNumber: result.phoneNumber,
            }
            localStorage.setItem('customer', JSON.stringify(customer));
            setAuth(customer);
            
            navigate(`/${result.booking?.stadiumToken}/bookings/${result.booking?.number}`);
        })
    }, [])

    return <div className="sign-in-container">
        <div className="color-block bottom-color-block"/>
        <div className="color-block top-color-block"/>

        <div style={{
            display: 'flex',
            flexDirection: 'column',
            width: '100%',
            justifyContent: 'center',
            alignItems: 'center',
            height: '100%'
        }}>

            <div className="balls balls-top" style={{
                backgroundImage: `url(${back_balls})`
            }}/>
          
            <div className="balls balls-bottom" style={{
                backgroundImage: `url(${back_balls})`
            }}/>

        </div>
    </div>;
}
        