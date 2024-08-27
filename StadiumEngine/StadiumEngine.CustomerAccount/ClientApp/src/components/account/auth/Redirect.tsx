import React from "react";
import {useParams} from "react-router-dom";

export const Redirect = () => {
    let {token} = useParams();

    return <div>{token}</div>;
}