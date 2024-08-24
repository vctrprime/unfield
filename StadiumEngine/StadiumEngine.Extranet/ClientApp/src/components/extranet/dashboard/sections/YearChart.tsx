import React from 'react';
import {VictoryLine} from "victory";
import {ResponsiveVictoryChart} from "../../common/ResponsiveVictoryChart";

export const YearChart = () => {
    return <div className="dashboard-section" style={{backgroundColor: 'pink'}}>
        <ResponsiveVictoryChart>
            <VictoryLine
                categories={{
                    x: ["birds", "cats", "dogs", "fish", "frogs", "birds1", "cats2", "dogs3", "fish4", "frogs5"]
                }}
                data={[
                    {x: "cats", y: 1},
                    {x: "dogs", y: 2},
                    {x: "birds", y: 3},
                    {x: "fish", y: 2},
                    {x: "frogs", y: 1},
                    {x: "cats1", y: 1},
                    {x: "dogs2", y: 2},
                    {x: "birds3", y: 3},
                    {x: "fish4", y: 2},
                    {x: "frogs5", y: 1}
                ]}
            />
        </ResponsiveVictoryChart>
    </div>
}