import React from 'react';
import {t} from "i18next";
import {ResponsiveVictoryChart} from "../../common/ResponsiveVictoryChart";
import {VictoryAxis, VictoryBar, VictoryLabel} from "victory";
import {StadiumDashboardChartItemDto} from "../../../../models/dto/dashboard/StadiumDashboardDto";

export interface PopularInventoryProps {
    data: StadiumDashboardChartItemDto[]
}

export const PopularInventory = (props: PopularInventoryProps) => {
    const data = props.data.map(d => {
        return {
            x: d.category,
            y: d.value,
        }
    });

    const dy = data.length == 5 ? -10 :
        data.length == 4 ? -12 :
            data.length == 3 ? -14 :
                data.length == 2 ? -16 : -12;

    const maxValue = Math.max.apply(Math, props.data.map(d => d.value));

    return <div className="dashboard-section">
        <div className="section-name" style={{ lineHeight: '28px', paddingLeft: '30px'}}>
            {t('dashboard:stadium:sections:titles:popular_inventory')}
        </div>
        <div className="section-chart-cont">
            <ResponsiveVictoryChart padding={{left: 40, right: 0, bottom: 35, top: 30}}>
                <VictoryBar
                    domain={{x: [0, 2], y: [0, maxValue ? maxValue + 5 : 5]}}
                    alignment="middle"
                    labels={({datum}) => `${datum.x}: ${datum.y}`}
                    labelComponent={<VictoryLabel dy={dy} x={35}/>}
                    style={{
                        labels: {fill: "black", fontSize: 10},
                        data: {
                            fillOpacity: ({ index }) => index == data.length - 1 ? 1 :
                                index == data.length - 2 ? 0.9 :
                                    index == data.length - 3 ? 0.8 :
                                        index == data.length - 4 ? 0.7 : 0.6 
                        }
                    }}
                    barRatio={0.7}
                    horizontal
                    data={data}
                />
                <VictoryAxis tickCount={0} style={{tickLabels: {fill: "transparent"}}}/>
                <VictoryAxis dependentAxis/>
            </ResponsiveVictoryChart>
        </div>
    </div>
}