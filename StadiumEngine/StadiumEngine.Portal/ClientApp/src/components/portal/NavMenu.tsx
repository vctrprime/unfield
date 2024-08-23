import React, {useState} from 'react';
import {Collapse, Container, Navbar, NavbarBrand, NavbarToggler, NavItem, NavLink} from 'reactstrap';
import {Link} from 'react-router-dom';
import '../../css/portal/NavMenu.scss';
import logo from '../../img/logo/logo_icon_with_title.png';
import {t} from "i18next";
import {LanguageSelect} from "../common/LanguageSelect";
import {useRecoilValue} from "recoil";
import {envAtom} from "../../state/env";


export const NavMenu = () => {
    const [collapsed, setCollapsed] = useState(true);
    const env = useRecoilValue(envAtom);

    const toggleNavbar = () => {
        setCollapsed(!collapsed);
    }

    return (
        <header>
            <Navbar className="navbar-expand-sm navbar-toggleable-sm ng-white border-bottom box-shadow mb-3" light>
                <Container>
                    <NavbarBrand className={"navbar-brand-ext"} tag={Link} to={env?.portalHost}>
                        <img className={"logo"} alt={"Stadium Engine"} src={logo}/>
                    </NavbarBrand>

                    <div style={{display: 'flex', alignItems: 'center'}}>
                        <LanguageSelect withRequest={false} style={{marginRight: '5px'}}/>
                        <NavbarToggler onClick={toggleNavbar} className="mr-2"/>
                        <Collapse className="d-sm-inline-flex flex-sm-row-reverse" isOpen={!collapsed} navbar>
                            <ul className="navbar-nav flex-grow">
                                <NavItem>
                                    <NavLink tag={Link} className="btn btn-default lk-button"
                                             to={env?.extranetHost}>
                                        {t("portal:header:lk_button")}
                                    </NavLink>
                                </NavItem>
                            </ul>
                        </Collapse>

                    </div>

                </Container>
            </Navbar>
        </header>
    );
}
