import React, {useState} from 'react';
import { useRecoilState } from 'recoil';
import { authAtom } from '../../../state/auth';
import {Link, NavLink} from "react-router-dom";
import {NavItem} from "reactstrap";

export const SignIn = () => {
    const [errorMessage, setErrorMessage] = useState({ value: "" });
    const [auth, setAuth] = useRecoilState(authAtom);

    const handleSubmit = (e) => {
        e.preventDefault();

        const { username, password } = document.forms[0];
        
        //if username or password field is empty, return error message
        if (username.value === "" || password.value === "") {
            setErrorMessage((prevState) => ({
                value: "Empty username/password field",
            }));
        } else if (username.value === "admin" && password.value === "123456") {
            //Signin Success
          
            const user = {username: username.value}
            localStorage.setItem('user', JSON.stringify(user));
            setAuth(user);

            //const { from } = history.location.state || { from: { pathname: '/lk' } };
            //history.push(from);
            window.location.pathname = "/lk";
        } else {
            //If credentials entered is invalid
            setErrorMessage((prevState) => ({ value: "Invalid username/password" }));
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
    
    