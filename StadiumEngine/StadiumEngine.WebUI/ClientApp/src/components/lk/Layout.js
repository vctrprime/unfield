import React from 'react';
import {Container, NavItem} from "reactstrap";
import {Link, NavLink, Outlet} from "react-router-dom";

export const Layout = () => {
    return (
        <div>
            <ul className="navbar-nav flex-grow">
                <NavItem>
                    <NavLink tag={Link} className="text-dark" to="/">Home</NavLink>
                </NavItem>
                <NavItem>
                    <NavLink tag={Link} className="text-dark" to="/lk/actives">Actives</NavLink>
                </NavItem>
                <NavItem>
                    <NavLink tag={Link} className="text-dark" to="/lk/schedule">Schedule</NavLink>
                </NavItem>
            </ul>
            <Container>
                <Outlet />
            </Container>
        </div>
    );
}