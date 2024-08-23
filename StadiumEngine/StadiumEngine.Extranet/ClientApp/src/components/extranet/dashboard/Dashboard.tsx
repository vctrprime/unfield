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
export const Dashboard = () => {
    document.title = getTitle("common:lk_navbar:dashboard")

    const stadium = useRecoilValue(stadiumAtom);
    
    const [data, setData] = useState<StadiumDashboardDto>({} as StadiumDashboardDto);

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
        <div className="dashboard-row">
                <div className="dashboard-column" style={{flex: 2}}>
                    <YearChart/>
                </div>
                <div className="dashboard-column">
                    <FieldDistribution/>
                </div>
            </div>
            <div className="dashboard-row">
                <div className="dashboard-column">
                    <div className="dashboard-row">
                        <AverageBill/>
                    </div>
                    <div className="dashboard-row">
                        <PopularInventory/>
                    </div>
                </div>
                <div className="dashboard-column" style={{flex: 2}}>
                    <TimeChart/>
                </div>
            </div>
    </div>
}