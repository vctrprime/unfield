import React from 'react';
import {VictoryChart, VictoryTheme} from "victory";

export const ResponsiveVictoryChart = ({...initialProps}) => {
    //@ts-ignore
    function getSize(element) {
        const width = element?.offsetWidth;
        const height = element?.offsetHeight;
        return { height, width };
    }

    //@ts-ignore
    function useSize(element) {
        const [size, setSize] = React.useState(getSize(element));
        
        React.useEffect(() => {
            const onResize = () => setSize(getSize(element));
            
            if (size.width === undefined) {
                onResize();
            }

            window.addEventListener("resize", onResize);
            return () => window.removeEventListener("resize", onResize);
        });
        return size;
    }

    const ref = React.useRef(null);
    const { width, height } = useSize(ref.current);

    const props = {
        ...initialProps,
        width,
        height
    };

    return (
        <div className="chart" ref={ref}>
            <VictoryChart
                theme={VictoryTheme.material}
                {...props} />
        </div>
    );
}