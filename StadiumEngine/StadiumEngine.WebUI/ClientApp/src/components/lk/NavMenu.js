import React from 'react';
import {Button, NavItem} from "reactstrap";
import {Link, NavLink} from "react-router-dom";
import {useFetchWrapper} from "../../helpers/fetch-wrapper";
import {useRecoilState} from "recoil";
import {authAtom} from "../../state/auth";

export const NavMenu = () => {
    const fetchWrapper = useFetchWrapper();
    const [auth, setAuth] = useRecoilState(authAtom);
    
    const logout = () => {
        fetchWrapper.delete(`api/account/logout`)
            .finally(() => {
                localStorage.removeItem('user');
                setAuth(null);
                window.location.pathname = "/";
            });
    }
    
    
    return (<ul className="navbar-nav flex-grow">
        <NavItem>
            <NavLink tag={Link} className="text-dark" to="/">Home</NavLink>
        </NavItem>
        <NavItem>
            <NavLink tag={Link} className="text-dark" to="/lk">Main</NavLink>
        </NavItem>
        <NavItem>
            <NavLink tag={Link} className="text-dark" to="/lk/actives">Actives</NavLink>
        </NavItem>
        <NavItem>
            <NavLink tag={Link} className="text-dark" to="/lk/schedule">Schedule</NavLink>
        </NavItem>
        <NavItem>
            <Button onClick={logout}>Выйти</Button>
        </NavItem>
    </ul>)
}
    
    