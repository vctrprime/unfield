import React from 'react';
import {StadiumDashboardChartItemDto} from "../../../../models/dto/dashboard/StadiumDashboardDto";
import {t} from "i18next";
import {ResponsiveVictoryChart} from "../../common/ResponsiveVictoryChart";
import {VictoryArea, VictoryAxis, VictoryVoronoiContainer} from "victory";

export interface TimeChartProps {
    data: StadiumDashboardChartItemDto[]
}

export const TimeChart = (props: TimeChartProps) => {
    const maxValue = Math.max.apply(Math, props.data.map(d => d.value));
    
    return <div className="dashboard-section">
        <div className="section-name">
            {t('dashboard:stadium:sections:titles:time_chart')}
        </div>
        <div className="section-chart-cont">
            <ResponsiveVictoryChart containerComponent={<VictoryVoronoiContainer
                labels={({ datum }) => `${datum.x}: ${datum.y}`}
            />}>
                <VictoryArea
                    domain={{ y: [0, maxValue ? maxValue + 10 : 10]}}
                    interpolation="natural"
                    style={{
                        data: {
                            fill: "#00d2ff",
                            fillOpacity: 0.7
                        },
                        labels: {
                            fontFamily: "Stadium Engine Sans Pro",
                            fill: "#354650"
                        }
                    }}
                    data={props.data.map(d => {
                        return {
                            x: d.category,
                            y: d.value
                        }
                    })
                    }
                />
                <VictoryAxis 
                    tickCount={props.data.length/2}
                />
                <VictoryAxis dependentAxis />
            </ResponsiveVictoryChart>
        </div>
    </div>
}