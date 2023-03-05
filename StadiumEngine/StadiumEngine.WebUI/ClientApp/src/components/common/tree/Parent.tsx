import React from 'react';

export const Parent = ({hasChild}: any) => {
    return <div className="tree-parent">
        <div className="square"/>
        <div className="line" style={hasChild ? {} : {backgroundColor: "transparent"}}/>
    </div>
}