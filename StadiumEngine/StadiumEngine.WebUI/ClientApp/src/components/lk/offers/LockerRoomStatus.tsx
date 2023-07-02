import React from 'react';
import {PermissionsKeys} from "../../../static/PermissionsKeys";
import {Button, Popup} from "semantic-ui-react";
import {LockerRoomStatus} from "../../../models/dto/offers/enums/LockerRoomStatus";
import {t} from "i18next";
import {useRecoilValue} from "recoil";
import {permissionsAtom} from "../../../state/permissions";

export interface StatusRendererProps {
    status: LockerRoomStatus,
    syncAction: any;
    position?: "top center" | "top left" | "top right" | "bottom right" | "bottom left" | "right center" | "left center" | "bottom center" | undefined;
}

export const StatusRenderer = (props: StatusRendererProps) => {
    const permissions = useRecoilValue(permissionsAtom);
    
    const hasPermission = permissions.find( x => x.name == PermissionsKeys.SyncLockerRoomStatus) !== undefined;

    const StatusPopupTrigger = () : JSX.Element => {
        switch (props.status) {
            case LockerRoomStatus.Unknown:
                return <div className="locker-room-status unknown">{t("offers:locker_rooms_grid:statuses:unknown")}</div>;
            case LockerRoomStatus.Ready:
                return <div className="locker-room-status ready">{t("offers:locker_rooms_grid:statuses:ready")}</div>;
            case LockerRoomStatus.Busy:
                return <div className="locker-room-status busy">{t("offers:locker_rooms_grid:statuses:busy")}</div>;
            case LockerRoomStatus.NeedCleaning:
                return <div className="locker-room-status need-cleaning">{t("offers:locker_rooms_grid:statuses:need_cleaning")}</div>;
        }

        return <span/>;
    }
    

    return <Popup
        trigger={<div style={{width: '100%', position: 'absolute', pointerEvents: hasPermission ? 'initial' : 'none'}}>
            <StatusPopupTrigger />
        </div>}
        content={<Button onClick={props.syncAction}
                         color='vk'
                         content={props.status === LockerRoomStatus.NeedCleaning ? t("offers:locker_rooms_grid:statuses:cleaned") : t("offers:locker_rooms_grid:statuses:sync")} />}
        on='click'
        position={props.position || 'top center'}
    />
}