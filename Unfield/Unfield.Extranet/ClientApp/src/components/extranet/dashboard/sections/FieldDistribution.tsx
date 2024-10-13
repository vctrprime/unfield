import React, {useEffect, useRef, useState} from 'react';
import {t} from "i18next";
import {VictoryAxis, VictoryLegend, VictoryPie, VictoryTheme, VictoryTooltip} from "victory";
import {ResponsiveVictoryChart} from "../../common/ResponsiveVictoryChart";
import {StadiumDashboardChartItemDto} from "../../../../models/dto/dashboard/StadiumDashboardDto";

export interface FieldDistributionProps {
    data: StadiumDashboardChartItemDto[]
}

export const FieldDistribution = (props: FieldDistributionProps) => {
    const colorScale = [
        "#00d2ff",
        "#36454f",
        "#6e7f80",
        "#c5e3e7",
        "#75acb4",
        "#777887",
        "#052c4f"
    ]
    
    const firstValue = props.data[0]?.value ?? 1;
    const percents = props.data.map(d =>  {
        return {
            category: d.category,
            percent: d.value/firstValue
        } 
    });
    
    const [pieContWidth, setPieContWidth] = useState(0);
    const piContRef = useRef(null);
    
    useEffect(() => {
        //@ts-ignore
        setPieContWidth(piContRef.current.getBoundingClientRect().width);
    }, []);
    
    const getRadius = (category: string) => {
        const percent = percents.find( x => x.category === category)?.percent;

        return 20 + (pieContWidth / 2 - 50) * (percent ?? 1);
    }

    return <div className="dashboard-section">
        <div className="section-name">
            {t('dashboard:stadium:sections:titles:field_distribution')}
        </div>
        <div className="section-chart-cont">
            <div className="field-distrib-block" style={{width: '40%'}}>
                <ResponsiveVictoryChart theme={VictoryTheme.grayscale}>
                    <VictoryLegend orientation="vertical"
                                   y={40}
                                   gutter={20}
                                   style={{border: {stroke: "transparent"}, labels: { fontSize: 11}}}
                                   colorScale={colorScale}
                                   data={props.data.map(d => {
                                       return {
                                           name: d.category
                                       }
                                   })}
                    />
                    <VictoryAxis style={{
                        axis: {stroke: "transparent"},
                        ticks: {stroke: "transparent"},
                        tickLabels: {fill: "transparent"}
                    }}/>
                </ResponsiveVictoryChart>
            </div>
            <div className="field-distrib-block"  ref={piContRef} style={{width: '60%'}}>
                <ResponsiveVictoryChart padding={{ left: 0, right: 0, bottom: 40, top: 20 }}>
                    <VictoryPie
                        colorScale={colorScale}
                        labels={({ datum }) => `${datum.x}: ${datum.y}`}
                        labelComponent={<VictoryTooltip dy={0} centerOffset={{ x: -50 }} />}
                        //@ts-ignore
                        labelRadius={({innerRadius}) => innerRadius + 5}
                        radius={({datum}) => getRadius(datum.x)}
                        innerRadius={20}
                        style={{labels: {fill: "#666", fontSize: 10, fontWeight: "bold"}}}
                        data={props.data.map(d => {
                            return {
                                x: d.category,
                                y: d.value,
                            }
                        })}
                    />
                    <VictoryAxis style={{
                        axis: {stroke: "transparent"},
                        ticks: {stroke: "transparent"},
                        tickLabels: {fill: "transparent"}
                    }}/>
                </ResponsiveVictoryChart>
            </div>
        </div>

    </div>
}