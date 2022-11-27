import React, {useState} from 'react';
import { useRecoilState } from 'recoil';
import { authAtom } from '../../../state/auth';
import {Link, NavLink, useNavigate} from "react-router-dom";
import {NavItem} from "reactstrap";
import {useFetchWrapper} from "../../../helpers/fetch-wrapper";
import { NotificationManager} from 'react-notifications';

export const SignIn = () => {
    const [auth, setAuth] = useRecoilState(authAtom);
    const fetchWrapper = useFetchWrapper();
    const navigate = useNavigate();

    const handleSubmit = (e) => {
        e.preventDefault();

        const { username, password } = document.forms[0];
        
        fetchWrapper.post(`api/account/login`, { username: username.value, password: password.value })
            .then(user => {
                // store user details and jwt token in local storage to keep user logged in between page refreshes
                localStorage.setItem('user', JSON.stringify(user));
                setAuth(user);

                // get return url from location state or default to home page
                //const { from } = history.location.state || { from: { pathname: '/lk' } };
                //history.push(from);
                navigate("/lk");
            }).catch((error) => {
                NotificationManager.error(error);
            });
    };

    return (
        <div className="text-center">
            <NavItem>
                <NavLink tag={Link} className="text-dark" to="/">Home</NavLink>
            </NavItem>
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
            </form>
        </div>
    );
}
    
    