import React, {useState} from 'react';
import {Button, Icon, Modal} from "semantic-ui-react";
import {t} from "i18next";

export const DeleteButton = ({action, deleteHeader, deleteQuestion}: any) => {
    const [deleteModal, setDeleteModal] = useState<boolean>(false)

    return <div>
        <Modal
            dimmer='blurring'
            size='small'
            open={deleteModal}>
            <Modal.Header>{deleteHeader}</Modal.Header>
            <Modal.Content>
                <p>{deleteQuestion}</p>
            </Modal.Content>
            <Modal.Actions>
                <Button style={{backgroundColor: '#CD5C5C', color: 'white'}} onClick={() => {
                    setDeleteModal(false);
                }}>{t('common:no_button')}</Button>
                <Button style={{backgroundColor: '#3CB371', color: 'white'}} onClick={() => {
                    setDeleteModal(false);
                    action();
                }}>{t('common:yes_button')}</Button>
            </Modal.Actions>
        </Modal>
        <Icon name='trash alternate' title={t('common:delete_button')} className="action-button"
              onClick={() => setDeleteModal(true)}/>
    </div>

}