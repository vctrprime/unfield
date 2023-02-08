import React from 'react';
import {SaveButton} from "./SaveButton";
import {DeleteButton} from "./DeleteButton";

export const ActionButtons = ({title, 
                                  saveAction = null, 
                                  deleteAction = null,
                                  deleteHeader = null,
                                  deleteQuestion = null }: any) => {
    return <div className="action-buttons-container">
        <div className="action-buttons-title">
            {title}
        </div>
        <div className="action-buttons">
            {saveAction !== null && <SaveButton action={saveAction}/>}
            {deleteAction !== null && <DeleteButton action={deleteAction} deleteHeader={deleteHeader} deleteQuestion={deleteQuestion}/>}
        </div>
        
    </div>
}