import React, {useEffect, useRef, useState, useCallback} from 'react';
import {GridLoading} from "../common/GridLoading";
import {useRecoilValue, useSetRecoilState} from "recoil";
import {rolesAtom} from "../../../state/roles";
import {dateFormatter} from "../../../helpers/date-formatter";
import {RoleDto} from "../../../models/dto/accounts/RoleDto";
import {useInject} from "inversify-hooks";
import {IAccountsService} from "../../../services/AccountsService";
import {t} from "i18next";
import {getOverlayNoRowsTemplate, StringFormat} from "../../../helpers/utils";
import {GridCellWithTitleRenderer} from "../../common/GridCellWithTitleRenderer";
import {Button, Form, Modal} from "semantic-ui-react";
import {permissionsAtom} from "../../../state/permissions";
import {PopupCellRenderer} from "../../common/PopupCellRenderer";
import {changeBindingStadiumAtom} from "../../../state/changeBindingStadium";
import {ContainerLoading} from "../../common/ContainerLoading";

const AgGrid = require('ag-grid-react');
const { AgGridReact } = AgGrid;

export const RolesGrid = ({setSelectedRole} : any) => {
    const [data, setData] = useState<RoleDto[]>([]);
    const [isLoading, setIsLoading] = useState<boolean>(true);
    
    const setRecoilRoles = useSetRecoilState(rolesAtom);

    const permissions = useRecoilValue(permissionsAtom);

    const  changeBindingStadium = useRecoilValue(changeBindingStadiumAtom);
    
    const [roleModal, setRoleModal] = useState<boolean>(false)
    const [deleteRoleModal, setDeleteRoleModal] = useState<boolean>(false)
    
    const [editingRole, setEditingRole] = useState<RoleDto | null>(null);
    const [deletingRole, setDeletingRole] = useState<RoleDto | null>(null);
    
    const gridRef = useRef<any>();
    
    const [selectedNodeId, setSelectedNodeId] = useState(null);
    

    const NameRenderer = (obj : any) => {
        return <span className="link-cell">{obj.data.name}</span>;
    }

    const columnDefs = [
        {
            headerName: '',
            cellRenderer: (obj: any) => <PopupCellRenderer deleteAccess={permissions.filter(p => p.name === 'delete-role').length > 0} 
                                                   editAccess={permissions.filter(p => p.name === 'update-role').length > 0} 
                                                   deleteHandler={() => {
                                                       setDeletingRole(obj.data);
                                                       setDeleteRoleModal(true);
                                                   }}
                                                   editHandler={() => {
                                                       setEditingRole(obj.data);
                                                       setRoleModal(true);
                                                   }}
            />,
            pinned: 'left',
            width: 58,
        },
        {field: 'name', headerName: t("accounts:roles_grid:name"), width: 150, cellRenderer: NameRenderer, onCellClicked: (e: any) => {e.node.setSelected(true); setSelectedNodeId(e.node.id); }, },
        {field: 'description', headerName: t("accounts:roles_grid:description"), width: 400, cellRenderer: (obj: any) => <GridCellWithTitleRenderer value={obj.data.description}/> },
        {field: 'usersCount', cellClass: "grid-center-cell", headerName: t("accounts:roles_grid:users_count"), width: 200},
        {field: 'stadiumsCount', cellClass: "grid-center-cell", headerName: t("accounts:roles_grid:stadiums_count"), width: 200},
        {field: 'userCreated', cellClass: "grid-center-cell", headerName: t("accounts:roles_grid:user_created"), width: 200},
        {field: 'dateCreated', cellClass: "grid-center-cell", headerName: t("accounts:roles_grid:date_created"), width: 170, valueFormatter: dateFormatter},
    ];

    const [accountsService] = useInject<IAccountsService>('AccountsService');
    
    useEffect(() => {
        fetchRoles();
    }, [])

    useEffect(() => {
        if (changeBindingStadium !== null) {
            const rowNode = gridRef.current.api.getRowNode(selectedNodeId);
            rowNode.setDataValue('stadiumsCount', rowNode.data.stadiumsCount + (changeBindingStadium ? 1 : -1));
        }
    }, [changeBindingStadium])
    
    const fetchRoles = () => {
        setIsLoading(true);
        accountsService.getRoles().then((result: RoleDto[]) => {
            setTimeout(() => {
                setData(result);
                setRecoilRoles(result.map((r) => {
                    return { key: r.id, value: r.id, text: r.name }
                }));
                setIsLoading(false);
            }, 500);
        })
    }

    const onSelectionChanged = useCallback(() => {
        if (gridRef.current !== undefined) {
            const selectedRows = gridRef.current.api.getSelectedRows();
            if (selectedRows.length > 0) {
                setSelectedRole(selectedRows[0]);
            }
            else {
                setSelectedRole(null);
            }
        }
        
    }, []);

    const nameInput = useRef<any>();
    const descriptionInput = useRef<any>();

    const [roleAction, setRoleAction] = useState<boolean>(false);
    const [error, setError] = useState<string|null>(null);
    
    const validate = (): boolean => {
        if (
        nameInput.current?.value === undefined ||
        nameInput.current?.value === null ||
        nameInput.current?.value === '') {
            nameInput.current.style.border = "1px solid red";
            setTimeout(() => {
                nameInput.current.style.border = "";
            }, 2000);
            return false;
        }
        else {
            return true;
        }
    }
    
    const addRole = () => {
        setError(null);
        if (validate()) {
            setRoleAction(true);
            accountsService.addRole({
                name: nameInput.current?.value,
                description: descriptionInput.current?.value
            }).then(() => {
                fetchRoles();
                setRoleModal(false);
            }).catch((error) => setError(error))
                .finally(() => {
                setRoleAction(false);
                
            });
        }
    }

    const updateRole = () => {
        setError(null);
        if (validate()) {
            setRoleAction(true);
            accountsService.updateRole({
                id: editingRole?.id||0,
                name: nameInput.current?.value,
                description: descriptionInput.current?.value
            }).then(() => {
                fetchRoles();
                setRoleModal(false);
            }).catch((error) => setError(error))
                .finally(() => {
                setRoleAction(false);
            });
        }
    }
    
    const deleteRole = () => {
        setRoleAction(true);
        accountsService.deleteRole(deletingRole?.id||0).then(() => {
            fetchRoles();
            setSelectedRole(null);
        }).finally(() => {
            setRoleAction(false);
            setDeleteRoleModal(false);
        });
    }
    
    return (
        <div className="roles-container">

            <Modal
                dimmer='blurring'
                size='small'
                open={roleModal}>
                <ContainerLoading show={roleAction} />
                <Modal.Header>{editingRole === null ? t('accounts:roles_grid:add') : t('accounts:roles_grid:edit')}</Modal.Header>
                <Modal.Content>
                    <Form style={{width: '500px'}}>
                        <Form.Field>
                            <label>{t("accounts:roles_grid:name")}</label>
                            <input ref={nameInput} placeholder={t("accounts:roles_grid:name")||''} defaultValue={editingRole?.name || ''}/>
                        </Form.Field>
                        <Form.Field>
                            <label>{t("accounts:roles_grid:description")}</label>
                            <textarea ref={descriptionInput} rows={4} placeholder={t("accounts:roles_grid:description")||''} defaultValue={editingRole?.description || ''}/>
                        </Form.Field>
                        {error !== null && <div className="error-message">{error}</div>}
                    </Form>
                </Modal.Content>
                <Modal.Actions>
                    <Button style={{backgroundColor: '#CD5C5C', color: 'white'}} onClick={() => {
                        setError(null);
                        setRoleModal(false);
                    }}>{t('common:close_button')}</Button>
                    <Button style={{backgroundColor: '#3CB371', color: 'white'}} onClick={() => {
                        editingRole === null ? addRole() : updateRole()
                    }}>{t('common:save_button')}</Button>
                </Modal.Actions>
            </Modal>

            <Modal
                dimmer='blurring'
                size='small'
                open={deleteRoleModal}>
                <ContainerLoading show={roleAction} />
                <Modal.Header>{t('accounts:roles_grid:delete:header')}</Modal.Header>
                <Modal.Content>
                    <p>{StringFormat(t('accounts:roles_grid:delete:question'), deletingRole?.name||'')}</p>
                </Modal.Content>
                <Modal.Actions>
                    <Button style={{backgroundColor: '#CD5C5C', color: 'white'}} onClick={() => {
                        setDeletingRole(null);
                        setDeleteRoleModal(false);
                    }}>{t('common:no_button')}</Button>
                    <Button style={{backgroundColor: '#3CB371', color: 'white'}} onClick={deleteRole}>{t('common:yes_button')}</Button>
                </Modal.Actions>
            </Modal>
            
            
            <Button onClick={() => {
                setEditingRole(null);
                setRoleModal(true);
            }} disabled={permissions.filter(p => p.name === 'insert-role').length === 0} className="add-role-button">{t('accounts:roles_grid:add')}</Button>
            {data.length === 0 && !isLoading && <span className="no-rows-message">{t('accounts:roles_grid:no_rows')}</span>}
            <div className="grid-container ag-theme-alpine" style={{height: 'calc(100% - 36px)'}}>
                {isLoading ? <GridLoading columns={columnDefs}/> : <AgGridReact
                    ref={gridRef}
                    rowData={data}
                    columnDefs={columnDefs}
                    onSelectionChanged={onSelectionChanged}
                    overlayNoRowsTemplate={getOverlayNoRowsTemplate()}
                />}
            </div>
        </div>)
} ;