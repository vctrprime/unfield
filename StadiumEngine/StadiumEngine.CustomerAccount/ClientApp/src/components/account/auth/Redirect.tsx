import React from "react";
import {useParams} from "react-router-dom";

export const Redirect = () => {
    let {lng, token} = useParams();

    return <div>{token}-{lng}</div>;
}