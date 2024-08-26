import React, {useEffect, useState} from 'react';
import '../../../css/extranet/Dashboard.scss';
import {getTitle} from "../../../helpers/utils";
import {TimeChart} from "./sections/TimeChart";
import {FieldDistribution} from "./sections/FieldDistribution";
import {AverageBill} from "./sections/AverageBill";
import {YearChart} from "./sections/YearChart";
import {PopularInventory} from "./sections/PopularInventory";
import {useRecoilValue} from "recoil";
import {stadiumAtom} from "../../../state/stadium";
import {useInject} from "inversify-hooks";
import {IDashboardService} from "../../../services/DashboardService";
import {StadiumDashboardDto} from "../../../models/dto/dashboard/StadiumDashboardDto";
import {t} from "i18next";

export const Dashboard = () => {
    document.title = getTitle("common:lk_navbar:dashboard")

    const stadium = useRecoilValue(stadiumAtom);
    
    const [data, setData] = useState<StadiumDashboardDto|null>(null);

    const [dashboardService] = useInject<IDashboardService>('DashboardService');

    const fetchDashboard = () => {
        dashboardService.getStadiumDashboard().then((result: StadiumDashboardDto) => {
            setData(result);
        })
    }

    useEffect(() => {
        fetchDashboard();
    }, [stadium])

    return <div className="dashboard-container">
        {data && 
            <div className="dashboard-calc-date">{t('dashboard:stadium:calc_date') + ": " + new Date(data.calculationDate).toLocaleString()}</div>}
        <div className="dashboard-row">
                <div className="dashboard-column" style={{flex: 2}}>
                    {data && <YearChart data={data.yearChart} />}
                </div>
                <div className="dashboard-column">
                    {data && <FieldDistribution data={data.fieldDistribution} />}
                </div>
            </div>
            <div className="dashboard-row">
                <div className="dashboard-column">
                    <div className="dashboard-row" style={{maxHeight: '37%'}}>
                        {data && <AverageBill data={data.averageBill}/>}
                    </div>
                    <div className="dashboard-row" style={{maxHeight: '63%'}}>
                        {data && <PopularInventory data={data.popularInventory}/>}
                    </div>
                </div>
                <div className="dashboard-column" style={{flex: 3}}>
                    {data && <TimeChart data={data.timeChart}/>}
                </div>
            </div>
    </div>
}