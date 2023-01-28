import React, {useRef, useState} from 'react';
import { useSetRecoilState, useRecoilState } from 'recoil';
import { authAtom } from '../../../state/auth';
import {Link, NavLink, useNavigate} from "react-router-dom";
import {NavbarBrand} from "reactstrap";
import logo from "../../../img/logo/logo_icon_with_title.png";
import {AuthorizeUserCommand} from "../../../models/command/accounts/AuthorizeUserCommand";
import {AuthorizeUserDto} from "../../../models/dto/accounts/AuthorizeUserDto";
import { useInject } from 'inversify-hooks';
import {IAccountsService} from "../../../services/AccountsService";
import {t} from "i18next";
import i18n from "../../../i18n/i18n";
import PhoneInput from 'react-phone-input-2'
import ru from 'react-phone-input-2/lang/ru.json'
import 'react-phone-input-2/lib/style.css'

import back_balls from "../../../img/back-balls.png";
import cells from "../../../img/cells.jpeg";
import {loadingAtom} from "../../../state/loading";
import {LanguageSelect} from "../../common/LanguageSelect";
import {getTitle} from "../../../helpers/utils";


export const SignIn = () => {
    document.title = getTitle("accounts:sign_in:button")
    
    const setAuth = useSetRecoilState(authAtom);
    const setLoading = useSetRecoilState(loadingAtom);
    
    const [accountsService] = useInject<IAccountsService>('AccountsService');
    
    const [login, setLogin] = useState<string | undefined>();
    
    const navigate = useNavigate();
    
    const handleSubmit = (e: any) => {
        e.preventDefault();

        const { password } = document.forms[0];
        if (login !== undefined) {
            const command : AuthorizeUserCommand = { login, password: password.value };

            accountsService.authorize(command)
                .then((user: AuthorizeUserDto) => {
                    localStorage.setItem('user', JSON.stringify(user));
                    setAuth(user);
                    
                    const localStorageLanguage = localStorage.getItem('language') || 'en';
                    
                    if (localStorageLanguage !== user.language) {
                        accountsService.changeLanguage(localStorageLanguage).then(() => {});
                    }
                    
                    if (user.isAdmin) {
                        setLoading(false);
                        navigate("/admin");
                    }
                    else {
                        navigate("/lk");
                    }
                    
                });
        }
        
    };

    return (
        <div className="sign-in-container" style={{
            background: `linear-gradient( rgba(255, 255, 255, 0.9), rgba(255, 255, 255, 0.9) ), no-repeat center / cover url(${cells})`
        }}>
            <div className="color-block bottom-color-block" />
            <div className="color-block top-color-block" />

            <div className="logo-container">
                <NavbarBrand className={"navbar-brand-ext"} tag={Link} to="/">
                    <img className={"logo"} alt={"Stadium Engine"} src={logo}/>
                </NavbarBrand>
                <div className={"version-title"}>{process.env.REACT_APP_VERSION}</div>
            </div>

            
            <div style={{
                display: 'flex',
                flexDirection: 'column',
                width: '100%',
                justifyContent: 'center',
                alignItems: 'center'}} >

            <div className="balls balls-top"  style={{
                backgroundImage: `url(${back_balls})`
            }}/>
            <form
               
            >
                <div className="form-group">
                    <label className="form-control-label">{t("accounts:sign_in:login")}</label>
                    <PhoneInput
                        onlyCountries={['ru']} // Перчень стран в поиске 
                        country='ru'
                        containerStyle={{marginTop: '10px'}}
                        inputStyle={{width: '100%', height: 40}}
                        searchClass='search-class'
                        searchStyle={{margin: 0, width: '90%', height: '30px', fontFamily: 'inherit'}}
                        enableSearch={false}
                        placeholder={'+7 (123) 456-78-90'}
                        disableSearchIcon
                        value={login}
                        localization={i18n.language == 'ru' ? ru : undefined}
                        countryCodeEditable={false}
                        onChange={(phone) => setLogin(phone)}
                    />
                </div>

                <div className="form-group">
                    <label className="form-control-label">{t("accounts:sign_in:password")}</label>
                    <input
                        className="form-control password-input"
                        type="password"
                        name="password"
                    />


                    <button
                        type="submit"
                        onClick={handleSubmit}
                    >
                        {t("accounts:sign_in:button")}
                    </button>
                </div>
                
               
            </form>
            <div className="balls balls-bottom"  style={{
                backgroundImage: `url(${back_balls})`
            }}/>

            </div>

            <LanguageSelect withRequest={false} style={{position: 'absolute', top: 10, left: 20}}/>
            
            <div className="sign-in-right-container">
                
                <NavLink className="portal-button" to="/">
                    <svg xmlns="http://www.w3.org/2000/svg" width="32" height="32" fill="currentColor"
                         className="bi bi-house" viewBox="0 0 16 16">
                        <path
                            d="M8.707 1.5a1 1 0 0 0-1.414 0L.646 8.146a.5.5 0 0 0 .708.708L2 8.207V13.5A1.5 1.5 0 0 0 3.5 15h9a1.5 1.5 0 0 0 1.5-1.5V8.207l.646.647a.5.5 0 0 0 .708-.708L13 5.793V2.5a.5.5 0 0 0-.5-.5h-1a.5.5 0 0 0-.5.5v1.293L8.707 1.5ZM13 7.207V13.5a.5.5 0 0 1-.5.5h-9a.5.5 0 0 1-.5-.5V7.207l5-5 5 5Z"/>
                    </svg>
                </NavLink>
            </div>
            
           
        </div>
    );
}
    
    