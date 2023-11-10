import React, {useEffect, useRef, useState} from 'react';
import {useRecoilValue} from "recoil";
import {stadiumAtom} from "../../../state/stadium";
import {HubConnection} from "@microsoft/signalr";
import {useInject} from "inversify-hooks";
import {INotificationsService} from "../../../services/NotificationsService";
import {UIMessageDto} from "../../../models/dto/notifications/UIMessageDto";
import {Icon, Popup} from "semantic-ui-react";
import {UIMessage} from "./UIMessage";
import {t} from "i18next";

const signalR = require("@microsoft/signalr");


export const Notifications = () => {
    const stadium = useRecoilValue(stadiumAtom);

    const [connection, setConnection] = useState<HubConnection|null>(null);
    const [uiMessages, setUIMessages] = useState<UIMessageDto[]>([])

    const [notificationsService] = useInject<INotificationsService>('NotificationsService');
    
    const fetchUIMessages = () => {
        notificationsService.getUIMessages().then((response) => {
            setUIMessages(response);
        })
    }
    
    const setRead = () => {
        const unreadMessages = uiMessages.filter( s => !s.isRead);
        if (unreadMessages.length) {
            const max = Math.max(...unreadMessages.map(o => o.id));
            notificationsService.setLastReadMessage(max).then(() => {
                const newMessages = uiMessages.map((c, i) => {
                    if (!c.isRead) {
                        c.isRead = true;
                    } 
                    return c;
                });
                setUIMessages(newMessages);
            });
        }
    }
    
    useEffect(() => {
        connection?.stop();
        setConnection(null);
        if (stadium) {
            fetchUIMessages();
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
                    setTimeout(() => {
                        fetchUIMessages();
                    }, 1000)
                });
            }).catch(err => {
                console.log(err);
            });
        }
    }, [connection])

    const timer = useRef(null);
    
    return <Popup
        content={
            <div className="ui-messages">
                {uiMessages.length == 0 && <div style={{textAlign: 'center'}}>
                    {t("notifications:ui_messages:no_data")}
                </div>}
                {uiMessages.map((m, i) => {
                    return <UIMessage key={i} message={m} />
                    })}
            </div>
        }
        onOpen={() => {
            //@ts-ignore
            timer.current = setTimeout(() => {
                setRead();
            }, 3000);
        }}
        onClose={() => {
            if (timer.current) {
                clearTimeout(timer.current);
            }
        }}
        on='click'
        position="bottom right"
        //popper={{ id: 'popper-container', style: { zIndex: 2000 } }}
        trigger={<div className={"notifications-icon"}>
            <Icon name="bell"/>
            {uiMessages.filter( m => !m.isRead).length > 0 && <div className="has-unread" />}
        </div>}
        />
}