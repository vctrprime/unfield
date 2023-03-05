import React from 'react';

export const Child = ({isLastChild}: any) => {
    return isLastChild ? <div className="tree-last-child">
        <div className="empty"/>
        <div className="line">
            <div className="round"/>
        </div>
    </div> : <div className="tree-child">
        <div className="empty"/>
        <div className="line"/>
        <div className="round-line">
            <div className="round"/>
        </div>
    </div>
}