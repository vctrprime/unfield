import React from 'react';
import {t} from "i18next";
import {LineSegment, VictoryAxis, VictoryLegend, VictoryPie} from "victory";
import {ResponsiveVictoryChart} from "../../common/ResponsiveVictoryChart";
import {StadiumDashboardChartItemDto} from "../../../../models/dto/dashboard/StadiumDashboardDto";

export interface FieldDistributionProps {
    data: StadiumDashboardChartItemDto[]
}

export const FieldDistribution = (props: FieldDistributionProps) => {
    return <div className="dashboard-section">
        <div className="section-name">
            {t('dashboard:stadium:sections:titles:field_distribution')}
        </div>
        <div className="section-chart-cont">
            <ResponsiveVictoryChart>
                <VictoryPie
                    colorScale={["#00d2ff", "orange", "gold", "cyan", "navy", "black", "#666" ]}
                    labelPosition="endAngle"
                    //@ts-ignore
                    labelRadius={({ innerRadius }) => innerRadius + 5 }
                    radius={({ datum }) => 20 + datum.y}
                    innerRadius={20}
                    style={{ labels: { fill: "transparent", fontSize: 10, fontWeight: "bold"}}}
                    data={props.data.map(d => {
                        return {
                            x: d.category,
                            y: d.value
                        }
                    })}
                />
                <VictoryAxis style={{
                    axis: {stroke: "transparent"},
                    ticks: {stroke: "transparent"},
                    tickLabels: { fill:"transparent"}
                }}/>
                <VictoryLegend orientation="horizontal"
                               gutter={20}
                               style={{ border: { stroke: "black" } }}
                               colorScale={[ "navy", "blue", "cyan" ]}
                               data={[
                                   { name: "One" }, { name: "Two" }, { name: "Three" }
                               ]}
                />
            </ResponsiveVictoryChart>
        </div>
    </div>
}