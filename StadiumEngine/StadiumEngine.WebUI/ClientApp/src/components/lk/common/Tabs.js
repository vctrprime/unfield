import React, {useEffect} from 'react';
import {NavLink, Outlet, useNavigate} from "react-router-dom";

export const Tabs = ({ tabsData, leftNavRoute }) => {
    const navigate = useNavigate();

    useEffect(() => {
        if (tabsData.filter(t => `${leftNavRoute}/${t.route}` === window.location.pathname).length === 0) {
            navigate(tabsData[0].route);
        }
    }, [])
    
    
    
    return (
        <div className="tabs-container">
            <div className="tabs-links">
                {tabsData.map((tab, i) => {
                    return (
                        <NavLink key={i} exact="true" to={tab.route} end>
                            <div className="tabs-link">{tab.name}</div>
                        </NavLink>
                    )
                })}
            </div>
            <div className="tabs-content">
                <Outlet />
            </div>

        </div>)
}