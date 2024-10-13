import React from 'react';
import {VictoryLabel, VictoryLine} from "victory";
import {StadiumDashboardChartItemDto} from "../../../../models/dto/dashboard/StadiumDashboardDto";
import {t} from "i18next";
import {ResponsiveVictoryChart} from "../../common/ResponsiveVictoryChart";

export interface YearChartProps {
    data: StadiumDashboardChartItemDto[]
}
export const YearChart = (props: YearChartProps) => {
    
    const getCategory = (category: string) => {
        const splitted = category.split(".");
        
        const month = splitted[0];
        const year = splitted[1];
        
        return t('common:months:short:' + month) + " " + year;
    }
    
    const maxValue = Math.max.apply(Math, props.data.map(d => d.value));
    
    return <div className="dashboard-section">
        <div className="section-name">
            { t('dashboard:stadium:sections:titles:year_chart')}
        </div>
        <div className="section-chart-cont">
            <ResponsiveVictoryChart padding={{ left: 40, right: 25, bottom: 35, top: 20 }}>
                <VictoryLine
                    domain={{ y: [0, maxValue ? maxValue + 1 : 1]}}
                    categories={{
                        x: props.data.map(d => getCategory(d.category))
                    }}
                    style={{
                        data: {
                            stroke: "#354650"
                        },
                        labels: {
                            fontFamily: "Unfield Sans Pro",
                            fill: "#354650"
                        }
                    }}
                    labels={({datum}) => datum.y == 0 ? '' : datum.y}
                    labelComponent={<VictoryLabel renderInPortal dy={-10}/>}
                    data={props.data.map(d => {
                        return {
                            x: getCategory(d.category),
                            y: d.value
                        }
                    })
                    }
                />
            </ResponsiveVictoryChart>
        </div>
    </div>
}