import React from 'react';
import {Icon} from "semantic-ui-react";
import {t} from "i18next";

export const SaveButton = ({action}: any) => {
    return <Icon name='save' title={t('common:save_button')} className="action-button" onClick={action}/>
}