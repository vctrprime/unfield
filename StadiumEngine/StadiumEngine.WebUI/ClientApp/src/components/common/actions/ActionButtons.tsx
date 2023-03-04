import React from 'react';
import {SaveButton} from "./SaveButton";
import {DeleteButton} from "./DeleteButton";
import {useRecoilValue} from "recoil";
import {permissionsAtom} from "../../../state/permissions";

export interface ActionButtonsProps {
    title: string,
    saveAction?: any,
    deleteAction?: any,
    deleteHeader?: any,
    deleteQuestion?: any,
    savePermission: string,
    deletePermission: string
}

export const ActionButtons = (props: ActionButtonsProps) => {
    const permissions = useRecoilValue(permissionsAtom);
    
    
    return <div className="action-buttons-container box-shadow">
        <div className="action-buttons-title">
            {props.title}
        </div>
        <div className="action-buttons">
            {props.saveAction !== null && permissions.filter(p => p.name === props.savePermission).length > 0 && <SaveButton action={props.saveAction}/>}
            {props.deleteAction !== null && permissions.filter(p => p.name === props.deletePermission).length > 0 && <DeleteButton action={props.deleteAction} deleteHeader={props.deleteHeader} deleteQuestion={props.deleteQuestion}/>}
        </div>
        
    </div>
}