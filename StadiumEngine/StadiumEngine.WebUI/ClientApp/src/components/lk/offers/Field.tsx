import React, {useEffect, useRef, useState} from 'react';
import {useParams} from "react-router-dom";

export const Field = () => {
    let { id } = useParams();
    
    return (<div>{id}</div>)
}