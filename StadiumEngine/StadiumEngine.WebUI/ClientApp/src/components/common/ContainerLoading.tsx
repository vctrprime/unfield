import {HashLoader} from "react-spinners";
import React from "react";

interface ContainerLoadingProps {
    show: boolean
}

export const ContainerLoading = ({show}: ContainerLoadingProps) => {
    return (
        <div>
            {show && <div className="d-flex justify-content-center align-items-center"
                                          style={{ backgroundColor: 'rgba(53,70,80, 0.2)',height: "100%", width: "100%", zIndex: 10000, position: "absolute", top: 0, left: 0}}>
                <HashLoader color="#00d2ff"/>
            </div>}
        </div>
    );
}