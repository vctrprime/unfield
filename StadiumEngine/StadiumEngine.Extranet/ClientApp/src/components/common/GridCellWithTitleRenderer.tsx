import React from "react";

export const GridCellWithTitleRenderer = ({value}: any) => {
    return <span title={value}>{value}</span>;
}