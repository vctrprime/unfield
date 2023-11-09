import React, {useEffect, useState} from 'react';
import {useRecoilValue} from "recoil";
import {stadiumAtom} from "../../state/stadium";
import {HubConnection} from "@microsoft/signalr";

const signalR = require("@microsoft/signalr");

export const Notifications = () => {
    const stadium = useRecoilValue(stadiumAtom);

    const [connection, setConnection] = useState<HubConnection|null>(null);
    
    useEffect(() => {
        connection?.stop();
        setConnection(null);
        if (stadium) {
            setConnection(new signalR.HubConnectionBuilder()
                .withUrl("/stadiumHub")
                .build());
        }
    }, [stadium])
    
    useEffect(() => {
        if (connection) {
            connection.start().then(res => {
                connection.invoke("JoinStadium", "stadium-" + stadium?.id)
                    .catch(err => {
                        console.log(err);
                    });
                connection.on("HasNewUIMessages", () => {
                    console.log('new message');
                });
            }).catch(err => {
                console.log(err);
            });
        }
    }, [connection])
    
    return <span/>
}