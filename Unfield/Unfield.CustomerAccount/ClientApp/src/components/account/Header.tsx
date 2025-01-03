import React, {useEffect, useState} from "react";
import AppBar from '@mui/material/AppBar';
import Box from '@mui/material/Box';
import Toolbar from '@mui/material/Toolbar';
import IconButton from '@mui/material/IconButton';
import Typography from '@mui/material/Typography';
import Menu from '@mui/material/Menu';
import MenuIcon from '@mui/icons-material/Menu';
import Container from '@mui/material/Container';
import Avatar from '@mui/material/Avatar';
import Button from '@mui/material/Button';
import Tooltip from '@mui/material/Tooltip';
import MenuItem from '@mui/material/MenuItem';
import {useRecoilState, useRecoilValue} from "recoil";
import {stadiumAtom} from "../../state/stadium";
import {getTitle} from "../../helpers/utils";
import i18n from "../../i18n/i18n";
import {useInject} from "inversify-hooks";
import {IAccountsService} from "../../services/AccountsService";
import {authAtom} from "../../state/auth";
import {NavLink, useNavigate, useParams} from "react-router-dom";
import {t} from "i18next";
import {Dropdown, Popup} from "semantic-ui-react";
import {LanguageSelect} from "../common/LanguageSelect";
import {StadiumDto} from "../../models/dto/accounts/StadiumDto";

export const Header = () => {
    const { stadiumToken } = useParams();

    const [anchorElNav, setAnchorElNav] = React.useState<null | HTMLElement>(null);
    const [anchorElUser, setAnchorElUser] = React.useState<null | HTMLElement>(null);

    const [stadiums, setStadiums] = useState<StadiumDto[]>([])
    const [stadium, setStadium] = useRecoilState(stadiumAtom);
    const [accountsService] = useInject<IAccountsService>('AccountsService');
    
    const [auth, setAuth] = useRecoilState(authAtom);
    
    useEffect(() => {
        if ( stadium == null || stadiums.length == 0 ) {
            accountsService.authorize().then((result) => {
                setStadiums(result.stadiums);
                const current = result.stadiums.find(s => s.token == stadiumToken);
                if (current) {
                    setStadium(current);
                    document.title = getTitle(current.name)
                }
            })
        }
    }, []);
    
    
    const navigate = useNavigate();

    const handleOpenNavMenu = (event: React.MouseEvent<HTMLElement>) => {
        setAnchorElNav(event.currentTarget);
    };
    const handleOpenUserMenu = (event: React.MouseEvent<HTMLElement>) => {
        setAnchorElUser(event.currentTarget);
    };

    const handleCloseNavMenu = () => {
        setAnchorElNav(null);
    };

    const handleCloseUserMenu = () => {
        setAnchorElUser(null);
    };

    const getNavLinkClassName = ({isActive}: any) => {
        return isActive ? "nav-link activeClicked" : "nav-link";
    }

    const logout = () => {
        accountsService.logout().finally(() => {
            setAuth(null);
            localStorage.removeItem('customer');
            navigate(`sign-in`);
        });
    }

    const changeStadium = (e: any, {value}: any) => {
        window.location.href = `/${value}/bookings/future`
    }

    return <div style={{ height: '48px'}}>
        <AppBar position="fixed" sx={{backgroundColor: '#354650'}}>
            <Container maxWidth="xl">
                <Toolbar sx={{ minHeight: '48px !important', maxHeight: '48px !important'}} disableGutters>
                    

                    <Box sx={{flexGrow: 1, display: {xs: 'flex', md: 'none'}}}>
                        <IconButton
                            size="medium"
                            aria-label="account of current user"
                            aria-controls="menu-appbar"
                            aria-haspopup="true"
                            onClick={handleOpenNavMenu}
                            color="inherit"
                        >
                            <MenuIcon/>
                        </IconButton>
                        <Menu
                            id="menu-appbar"
                            anchorEl={anchorElNav}
                            anchorOrigin={{
                                vertical: 'bottom',
                                horizontal: 'left',
                            }}
                            keepMounted
                            transformOrigin={{
                                vertical: 'top',
                                horizontal: 'left',
                            }}
                            open={Boolean(anchorElNav)}
                            onClose={handleCloseNavMenu}
                            sx={{display: {xs: 'block', md: 'none'}}}
                            slotProps={{
                                paper: {
                                    sx: {
                                        backgroundColor: '#354650',
                                    }
                                }
                            }}
                        >
                            <MenuItem key={'past'} onClick={handleCloseNavMenu}>
                                <NavLink className={getNavLinkClassName} to={`bookings/past`}>
                                    {t('common:header:past_bookings')}
                                </NavLink>
                            </MenuItem>
                            <MenuItem key={'future'} onClick={handleCloseNavMenu}>
                                <NavLink className={getNavLinkClassName} to={`bookings/future`}>
                                    {t('common:header:future_bookings')}
                                </NavLink>
                            </MenuItem>
                        </Menu>
                    </Box>
                    
                    <Box sx={{flexGrow: 1, display: {xs: 'none', md: 'flex'}}}>
                        <NavLink className={getNavLinkClassName} to={`/${stadiumToken}/bookings/past`}>
                            {t('common:header:past_bookings')}
                        </NavLink>
                        <NavLink className={getNavLinkClassName} to={`/${stadiumToken}/bookings/future`}>
                            {t('common:header:future_bookings')}
                        </NavLink>
                    </Box>
                    <Box sx={{ marginRight: 1}}>
                        {stadium && <Dropdown
                            onChange={changeStadium}
                            inline
                            options={stadiums.map((s) => {
                                return {key: s.token, value: s.token, text: s.name}
                            })}
                            defaultValue={stadium?.token||''}
                        />}
                    </Box>
                    <Box sx={{flexGrow: 0, display: 'flex'}}>
                        <Popup
                            trigger={<Avatar sx={{ width: 32, height: 32 }} style={{fontSize: '14px', cursor: 'pointer'}} alt={auth?.displayName} src="/static/images/avatar/no.jpg"/>} flowing hoverable>
                            <div className="customer-account-menu">
                                <div className="profile-menu-item" onClick={() =>
                                    navigate(`profile`)
                                }>{t('accounts:customer:menu:profile')}</div>
                                <div className="profile-menu-item" onClick={() =>
                                    navigate(`change-password`)
                                }>{t('accounts:customer:menu:change_password')}</div>
                                <div className="logout" onClick={logout}>{t('accounts:customer:menu:logout')}</div>
                            </div>
                        </Popup>
                        <LanguageSelect withRequest={true} style={{ marginTop: 4, marginLeft: 8, color: '#444'}}/>
                    </Box>
                </Toolbar>
            </Container>
        </AppBar>
    </div>;
}