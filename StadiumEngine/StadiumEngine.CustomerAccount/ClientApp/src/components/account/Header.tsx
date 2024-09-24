import React, {useEffect} from "react";
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
import {NavLink, useParams} from "react-router-dom";
import {t} from "i18next";

export const Header = () => {
    const { stadiumToken } = useParams();
    
    const pages = ['Products', 'Pricing', 'Blog'];
    const settings = ['Profile', 'Account', 'Dashboard', 'Logout'];

    const [anchorElNav, setAnchorElNav] = React.useState<null | HTMLElement>(null);
    const [anchorElUser, setAnchorElUser] = React.useState<null | HTMLElement>(null);

    const [stadium, setStadium] = useRecoilState(stadiumAtom);
    const [accountsService] = useInject<IAccountsService>('AccountsService');
    
    const auth = useRecoilValue(authAtom);
    
    useEffect(() => {
        if ( stadium == null ) {
            accountsService.getStadium().then((result) => {
                setStadium(result);
                document.title = getTitle(result.name)
            })
        }
    }, []);

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

    return <div style={{ height: '48px'}}>
        <AppBar position="static" sx={{backgroundColor: '#354650'}}>
            <Container maxWidth="xl">
                <Toolbar sx={{ minHeight: '48px !important', maxHeight: '48px !important'}} disableGutters>
                    <Typography
                        variant="h6"
                        noWrap
                        component="span"
                        sx={{
                            mr: 2,
                            display: {xs: 'none', md: 'flex'},
                            fontFamily: 'Stadium Engine Sans Pro',
                            fontWeight: 700,
                            color: 'inherit',
                            textDecoration: 'none',
                        }}
                    >
                        {stadium?.name}
                    </Typography>

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
                                <NavLink className={getNavLinkClassName} to={`/${stadiumToken}/bookings/past`}>
                                    {t('common:header:past_bookings')}
                                </NavLink>
                            </MenuItem>
                            <MenuItem key={'future'} onClick={handleCloseNavMenu}>
                                <NavLink className={getNavLinkClassName} to={`/${stadiumToken}/bookings/future`}>
                                    {t('common:header:future_bookings')}
                                </NavLink>
                            </MenuItem>
                        </Menu>
                    </Box>
                    <Typography
                        variant="h5"
                        noWrap
                        component="span"
                        sx={{
                            mr: 2,
                            display: {xs: 'flex', md: 'none'},
                            flexGrow: 1,
                            fontFamily: 'Stadium Engine Sans Pro',
                            fontWeight: 700,
                            color: 'inherit',
                            textDecoration: 'none',
                        }}
                    >
                        {stadium?.name}
                    </Typography>
                    <Box sx={{flexGrow: 1, display: {xs: 'none', md: 'flex'}}}>
                        <NavLink className={getNavLinkClassName} to={`/${stadiumToken}/bookings/past`}>
                            {t('common:header:past_bookings')}
                        </NavLink>
                        <NavLink className={getNavLinkClassName} to={`/${stadiumToken}/bookings/future`}>
                            {t('common:header:future_bookings')}
                        </NavLink>
                    </Box>
                    <Box sx={{flexGrow: 0}}>
                        <Tooltip title="Open settings">
                            <IconButton onClick={handleOpenUserMenu} sx={{p: 0}}>
                                <Avatar sx={{ width: 32, height: 32 }} style={{fontSize: '14px'}} alt={auth?.displayName} src="/static/images/avatar/no.jpg"/>
                            </IconButton>
                        </Tooltip>
                        <Menu
                            sx={{mt: '45px'}}
                            id="menu-appbar"
                            anchorEl={anchorElUser}
                            anchorOrigin={{
                                vertical: 'top',
                                horizontal: 'right',
                            }}
                            keepMounted
                            transformOrigin={{
                                vertical: 'top',
                                horizontal: 'right',
                            }}
                            open={Boolean(anchorElUser)}
                            onClose={handleCloseUserMenu}
                        >
                            {settings.map((setting) => (
                                <MenuItem key={setting} onClick={handleCloseUserMenu}>
                                    <Typography sx={{textAlign: 'center'}}>{setting}</Typography>
                                </MenuItem>
                            ))}
                        </Menu>
                    </Box>
                </Toolbar>
            </Container>
        </AppBar>
    </div>;
}