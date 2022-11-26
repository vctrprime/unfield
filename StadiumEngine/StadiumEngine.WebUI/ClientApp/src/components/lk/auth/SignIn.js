import React, {useState} from 'react';
import { useRecoilState } from 'recoil';
import { authAtom } from '../../../state/auth';
import {Link, NavLink} from "react-router-dom";
import {NavItem} from "reactstrap";
import {useFetchWrapper} from "../../../helpers/fetch-wrapper";

export const SignIn = () => {
    const [errorMessage, setErrorMessage] = useState({ value: "" });
    const [auth, setAuth] = useRecoilState(authAtom);
    const fetchWrapper = useFetchWrapper();

    const handleSubmit = (e) => {
        e.preventDefault();

        const { username, password } = document.forms[0];
        
        //if username or password field is empty, return error message
        if (username.value === "" || password.value === "") {
            setErrorMessage((prevState) => ({
                value: "Empty username/password field",
            }));
        }  else {

            fetchWrapper.post(`api/account/login`, { username: username.value, password: password.value })
                .then(user => {
                    // store user details and jwt token in local storage to keep user logged in between page refreshes
                    localStorage.setItem('user', JSON.stringify(user));
                    setAuth(user);

                    // get return url from location state or default to home page
                    //const { from } = history.location.state || { from: { pathname: '/lk' } };
                    //history.push(from);
                    window.location.pathname = "/lk";
                }).catch((error) => {
                    setErrorMessage((prevState) => ({ value: error }));
            });
            
        }
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

                {errorMessage.value && (
                    <p className="text-danger"> {errorMessage.value} </p>
                )}
            </form>
        </div>
    );
}
    
    