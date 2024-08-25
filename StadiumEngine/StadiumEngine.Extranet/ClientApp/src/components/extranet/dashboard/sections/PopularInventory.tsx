import React from 'react';
import {t} from "i18next";

export const PopularInventory = () => {
    return <div className="dashboard-section">
        <div className="section-name" style={{ lineHeight: '30px'}}>
            {t('dashboard:stadium:sections:titles:popular_inventory')}
        </div>
    </div>
}