import React from 'react';
import {StadiumDashboardAverageBillDto} from "../../../../models/dto/dashboard/StadiumDashboardDto";
import {t} from "i18next";

export interface AverageBillProps {
    data: StadiumDashboardAverageBillDto
}

export const AverageBill = (props: AverageBillProps) => {
    return <div className="dashboard-section" style={{ paddingLeft: '40px', paddingBottom: '15px', paddingTop: '10px'}}>
        <div className="section-chart-cont" style={{display: 'flex', height: '100%', flexDirection: 'column'}}>
            <div className="average-bill-row">
                <div className="average-bill-column">
                    <div className="average-bill-column-name">
                        {t('dashboard:stadium:sections:average_bill:field')}
                    </div>
                    <div className="average-bill-column-value">
                        {props.data.fieldValue}
                    </div>
                </div>
                <div className="average-bill-column">
                    <div className="average-bill-column-name">
                        {t('dashboard:stadium:sections:average_bill:inventory')}
                    </div>
                    <div className="average-bill-column-value">
                        {props.data.inventoryValue}
                    </div>
                </div>
            </div>
            <div className="average-bill-row">
                <div className="average-bill-column">
                    <div className="average-bill-column-name">
                        {t('dashboard:stadium:sections:average_bill:total')}
                    </div>
                    <div className="average-bill-column-value">
                        {props.data.totalValue}
                    </div>
                </div>
            </div>
        </div>
    </div>
}