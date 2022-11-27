import React, {useState} from 'react';
import { useRecoilState } from 'recoil';
import { authAtom } from '../../../state/auth';
import {Link, NavLink, useNavigate} from "react-router-dom";
import {NavbarBrand, NavItem} from "reactstrap";
import {useFetchWrapper} from "../../../helpers/fetch-wrapper";
import logo from "../../../img/logo/logo_icon_with_title.png";
export const SignIn = () => {
    const [auth, setAuth] = useRecoilState(authAtom);
    const fetchWrapper = useFetchWrapper();
    const navigate = useNavigate();

    const handleSubmit = (e) => {
        e.preventDefault();

        const { username, password } = document.forms[0];
        
        fetchWrapper.post(`api/account/login`, { username: username.value, password: password.value })
            .then(user => {
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

            
            
            <h1>Signin User</h1>
            <form
                style={{
                    display: "flex",
                    flexDirection: "column",
                    justifyContent: "center",
                    alignItems: "center",
                }}
            >
                <div className="form-group">
                    <label>Username</label>
                    <input
                        className="form-control"
                        type="text"
                        name="username"
                    />
                </div>

                <div className="form-group">
                    <label>Password</label>
                    <input
                        className="form-control"
                        type="password"
                        name="password"
                    />
                </div>
                <button
                    type="submit"
                    className="btn btn-primary"
                    onClick={handleSubmit}
                >
                    Submit
                </button>
                <NavLink className="btn btn-default portal-button" to="/">На портал</NavLink>
            </form>
           
        </div>
    );
}
    
    