import React from 'react';
import {NavItem} from "reactstrap";
import {Link, NavLink} from "react-router-dom";

export const NavMenu = () => (
    <ul className="navbar-nav flex-grow">
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
    </ul>
);