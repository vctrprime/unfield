import React, {MouseEventHandler, useRef, useState} from 'react';
import {t} from "i18next";
import Tippy from "@tippyjs/react";

export interface PopupCellRendererProps {
    editHandler: MouseEventHandler<HTMLDivElement>
    deleteHandler: MouseEventHandler<HTMLDivElement>,
    editAccess: boolean,
    deleteAccess: boolean
}

export const PopupCellRenderer = ({
      editHandler,
      deleteHandler,
      editAccess,
      deleteAccess
}: PopupCellRendererProps) => {
    const tippyRef = useRef<any>();
    const [visible, setVisible] = useState(false);
    const show = (e: any) => {
        setVisible(true);
    } 
    const hide = (e: any) => {
        setVisible(false);
    }

    const dropDownContent = (
        <div className="grid-menu-container">
            {editAccess && <div onClick={editHandler} className="grid-menu-item">
                {t('common:edit_button')}
            </div>}
            {deleteAccess && <div onClick={deleteHandler} className="grid-menu-item">
                {t('common:delete_button')}
            </div>}
        </div>
    );

    return (
        <Tippy
            ref={tippyRef}
            content={dropDownContent}
            visible={visible}
            onClickOutside={hide}
            allowHTML={true}
            arrow={false}
            appendTo={document.body}
            interactive={true}
            placement="right"
        >
            {editAccess || deleteAccess ? <i style={{fontSize: '18px', cursor: 'pointer'}} onClick={visible ? hide : show} className="fa fa-edit" aria-hidden="true" /> : <span/>}
        </Tippy>
    );
}