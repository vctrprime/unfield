import React from 'react';
import { useSetRecoilState } from 'recoil';
import { authAtom } from '../../../state/auth';
import {Link, NavLink, useNavigate} from "react-router-dom";
import {NavbarBrand} from "reactstrap";
import {useFetchWrapper} from "../../../helpers/fetch-wrapper";
import logo from "../../../img/logo/logo_icon_with_title.png";
import {AuthorizeUserCommand} from "../../../models/command/accounts/AuthorizeUserCommand";
import {AuthorizeUserDto} from "../../../models/dto/accounts/AuthorizeUserDto";

export const SignIn = () => {
    const setAuth = useSetRecoilState(authAtom);
    const fetchWrapper = useFetchWrapper();
    const navigate = useNavigate();

    const handleSubmit = (e: any) => {
        e.preventDefault();

        const { login, password } = document.forms[0];
        const data : AuthorizeUserCommand = { login: login.value, password: password.value };
        
        fetchWrapper.post({
            url: `api/accounts/login`,
            body: data,
            hideSpinner: false
        })
            .then((user: AuthorizeUserDto) => {
                localStorage.setItem('user', JSON.stringify(user));
                setAuth(user);
                navigate("/lk");
            });
    };

    return (
        <div className="sign-in-container">
            <div className="color-block bottom-color-block" />
            <div className="color-block top-color-block" />

            <div className="logo-container">
                <NavbarBrand className={"navbar-brand-ext"} tag={Link} to="/">
                    <img className={"logo"} alt={"Stadium Engine"} src={logo}/>
                </NavbarBrand>
                <div className={"version-title"}>{process.env.REACT_APP_VERSION}</div>
            </div>

            
            
            
            <form
                style={{
                    display: "flex",
                    flexDirection: "column",
                    justifyContent: "center",
                    alignItems: "center",
                }}
            >
                <div className="form-group">
                    <label className="form-control-label">Логин</label>
                    <input
                        className="form-control"
                        type="text"
                        name="login"
                    />
                </div>

                <div className="form-group">
                    <label className="form-control-label">Пароль</label>
                    <input
                        className="form-control"
                        type="password"
                        name="password"
                    />


                    <button
                        type="submit"
                        onClick={handleSubmit}
                    >
                        Войти
                    </button>
                </div>
                
               
            </form>

            <NavLink className="portal-button" to="/">
                <svg xmlns="http://www.w3.org/2000/svg" width="32" height="32" fill="currentColor"
                     className="bi bi-house" viewBox="0 0 16 16">
                    <path
                        d="M8.707 1.5a1 1 0 0 0-1.414 0L.646 8.146a.5.5 0 0 0 .708.708L2 8.207V13.5A1.5 1.5 0 0 0 3.5 15h9a1.5 1.5 0 0 0 1.5-1.5V8.207l.646.647a.5.5 0 0 0 .708-.708L13 5.793V2.5a.5.5 0 0 0-.5-.5h-1a.5.5 0 0 0-.5.5v1.293L8.707 1.5ZM13 7.207V13.5a.5.5 0 0 1-.5.5h-9a.5.5 0 0 1-.5-.5V7.207l5-5 5 5Z"/>
                </svg>
            </NavLink>
           
        </div>
    );
}
    
    