import React, {useEffect, useRef, useState} from 'react';
import {GridLoading} from "../common/GridLoading";
import {dateFormatter} from "../../../helpers/date-formatter";
import {UserDto} from "../../../models/dto/accounts/UserDto";
import {useInject} from "inversify-hooks";
import {IAccountsService} from "../../../services/AccountsService";
import {t} from "i18next";
import {getOverlayNoRowsTemplate, StringFormat} from "../../../helpers/utils";
import {Button, Form, Modal} from "semantic-ui-react";
import {useRecoilState, useRecoilValue} from "recoil";
import {permissionsAtom} from "../../../state/permissions";
import {PopupCellRenderer} from "../../common/PopupCellRenderer";
import i18n from "../../../i18n/i18n";
import PhoneInput from 'react-phone-input-2'
import ru from 'react-phone-input-2/lang/ru.json'
import 'react-phone-input-2/lib/style.css'
import {rolesAtom} from "../../../state/roles";
import {PermissionsRoleDropDownData} from "./Permissions";
import {RoleDto} from "../../../models/dto/accounts/RoleDto";
import {ContainerLoading} from "../../common/ContainerLoading";
import {PermissionsKeys} from "../../../static/PermissionsKeys";

const AgGrid = require('ag-grid-react');
const {AgGridReact} = AgGrid;


export const UsersGrid = () => {
    const [data, setData] = useState<UserDto[]>([]);
    const [isLoading, setIsLoading] = useState<boolean>(true);

    const [roles, setRoles] = useRecoilState<PermissionsRoleDropDownData[]>(rolesAtom);

    const permissions = useRecoilValue(permissionsAtom);

    const [userModal, setUserModal] = useState<boolean>(false)
    const [deleteUserModal, setDeleteUserModal] = useState<boolean>(false)

    const [editingUser, setEditingUser] = useState<UserDto | null>(null);
    const [deletingUser, setDeletingUser] = useState<UserDto | null>(null);
    const [newUserLogin, setNewUserLogin] = useState<string | undefined>();

    const gridRef = useRef<any>();

    const [accountsService] = useInject<IAccountsService>('AccountsService');

    useEffect(() => {
        const fetchRoles = () => {
            accountsService.getRoles().then((result: RoleDto[]) => {
                setRoles(result.map((r) => {
                    return {key: r.id, value: r.id, text: r.name}
                }));
            })
        }

        if (roles.length === 0) {
            fetchRoles();
        }
    }, [])


    const columnDefs = [
        {
            headerName: '',
            cellRenderer: (obj: any) => <PopupCellRenderer
                deleteAccess={permissions.filter(p => p.name === PermissionsKeys.DeleteUser).length > 0}
                editAccess={permissions.filter(p => p.name === PermissionsKeys.UpdateUser).length > 0}
                deleteHandler={() => {
                    setDeletingUser(obj.data);
                    setDeleteUserModal(true);
                }}
                editHandler={() => {
                    setEditingUser(obj.data);
                    setUserModal(true);
                }}
            />,
            pinned: 'left',
            width: 58,
        },
        {field: 'name', headerName: t("accounts:users_grid:name"), width: 150},
        {field: 'lastName', headerName: t("accounts:users_grid:last_name"), width: 170},
        {
            field: 'phoneNumber',
            cellClass: "grid-center-cell",
            headerName: t("accounts:users_grid:phone_number"),
            width: 150
        },
        {field: 'roleName', cellClass: "grid-center-cell", headerName: t("accounts:users_grid:role_name"), width: 170},
        {
            field: 'userCreated',
            cellClass: "grid-center-cell",
            headerName: t("accounts:users_grid:user_created"),
            width: 200
        },
        {
            field: 'dateCreated',
            cellClass: "grid-center-cell",
            headerName: t("accounts:users_grid:date_created"),
            width: 170,
            valueFormatter: dateFormatter
        },
        {
            field: 'lastLoginDate',
            cellClass: "grid-center-cell",
            headerName: t("accounts:users_grid:last_login_date"),
            width: 200,
            valueFormatter: dateFormatter
        }
    ]


    useEffect(() => {
        fetchUsers();
    }, [])

    const fetchUsers = () => {
        setIsLoading(true);
        accountsService.getUsers().then((result: UserDto[]) => {
            setTimeout(() => {
                setData(result);
                setIsLoading(false);
            }, 500);
        })
    }

    const nameInput = useRef<any>();
    const lastNameInput = useRef<any>();
    const roleIdInput = useRef<any>();

    const validate = (): boolean => {
        if (!nameInput.current?.value) {
            nameInput.current.style.border = "1px solid red";
            setTimeout(() => {
                nameInput.current.style.border = "";
            }, 2000);
            return false;
        } else {
            return true;
        }
    }

    const [userAction, setUserAction] = useState<boolean>(false);
    const [error, setError] = useState<string | null>(null);

    const addUser = () => {
        setError(null);
        if (validate()) {
            setUserAction(true);
            accountsService.addUser({
                name: nameInput.current?.value,
                roleId: parseInt(roleIdInput.current?.value),
                phoneNumber: newUserLogin || '',
                lastName: lastNameInput.current?.value
            }).then(() => {
                fetchUsers();
                setUserModal(false);
            }).catch((error) => {
                setError(error);
            }).finally(() => {
                setUserAction(false);
            });
        }
    }

    const updateUser = () => {
        setError(null);
        if (validate()) {
            setUserAction(true);
            accountsService.updateUser({
                id: editingUser?.id || 0,
                name: nameInput.current?.value,
                roleId: parseInt(roleIdInput.current?.value),
                lastName: lastNameInput.current?.value
            }).then(() => {
                fetchUsers();
                setUserModal(false);
            })
                .catch((error) => {
                    setError(error);
                })
                .finally(() => {
                    setUserAction(false);
                });
        }
    }

    const deleteUser = () => {
        setUserAction(true);
        accountsService.deleteUser(deletingUser?.id || 0).then(() => {
            fetchUsers();
        }).finally(() => {
            setUserAction(false);
            setDeleteUserModal(false);
        });
    }

    return (
        <div className="users-container">

            <Modal
                dimmer='blurring'
                size='small'
                open={userModal}>
                <ContainerLoading show={userAction}/>
                <Modal.Header>{editingUser === null ? t('accounts:users_grid:add') : t('accounts:users_grid:edit')}</Modal.Header>
                <Modal.Content>
                    <Form style={{width: '500px'}}>
                        {editingUser === null && <Form.Field>
                            <label>{t("accounts:users_grid:phone_number")}</label>
                            <PhoneInput
                                onlyCountries={['ru']}
                                country='ru'
                                inputStyle={{width: '100%', height: 38, paddingLeft: '42px', fontFamily: 'inherit'}}
                                placeholder={'+7 (123) 456-78-90'}
                                value={newUserLogin}
                                onChange={(phone) => setNewUserLogin(phone)}
                                localization={i18n.language === 'ru' ? ru : undefined}
                                countryCodeEditable={false}
                            />
                        </Form.Field>}
                        <Form.Field>
                            <label>{t("accounts:users_grid:name")}</label>
                            <input ref={nameInput} placeholder={t("accounts:users_grid:name") || ''}
                                   defaultValue={editingUser?.name || ''}/>
                        </Form.Field>
                        <Form.Field>
                            <label>{t("accounts:users_grid:last_name")}</label>
                            <input ref={lastNameInput} placeholder={t("accounts:users_grid:last_name") || ''}
                                   defaultValue={editingUser?.lastName || ''}/>
                        </Form.Field>
                        <Form.Field>
                            <label>{t("accounts:users_grid:role_name")}</label>
                            <select ref={roleIdInput} defaultValue={editingUser?.roleId || roles[0]?.value}>
                                {roles.map((r, i) => {
                                    return <option key={i} value={r.value}>{r.text}</option>
                                })}
                            </select>
                        </Form.Field>
                        {error !== null && <div className="error-message">{error}</div>}
                    </Form>
                </Modal.Content>
                <Modal.Actions>
                    <Button style={{backgroundColor: '#CD5C5C', color: 'white'}} onClick={() => {
                        setError(null);
                        setUserModal(false);
                    }}>{t('common:close_button')}</Button>
                    <Button disabled={editingUser === null && (newUserLogin === undefined ||
                        newUserLogin === null ||
                        newUserLogin === '' ||
                        newUserLogin.length < 11)} style={{backgroundColor: '#3CB371', color: 'white'}} onClick={() => {
                        editingUser === null ? addUser() : updateUser()
                    }}>{t('common:save_button')}</Button>
                </Modal.Actions>
            </Modal>

            <Modal
                dimmer='blurring'
                size='small'
                open={deleteUserModal}>
                <ContainerLoading show={userAction}/>
                <Modal.Header>{t('accounts:users_grid:delete:header')}</Modal.Header>
                <Modal.Content>
                    <p>{StringFormat(t('accounts:users_grid:delete:question'), `${deletingUser?.name} ${deletingUser?.lastName}` || '')}</p>
                </Modal.Content>
                <Modal.Actions>
                    <Button style={{backgroundColor: '#CD5C5C', color: 'white'}} onClick={() => {
                        setDeletingUser(null);
                        setDeleteUserModal(false);
                    }}>{t('common:no_button')}</Button>
                    <Button style={{backgroundColor: '#3CB371', color: 'white'}}
                            onClick={deleteUser}>{t('common:yes_button')}</Button>
                </Modal.Actions>
            </Modal>


            <Button onClick={() => {
                setEditingUser(null);
                setNewUserLogin(undefined);
                setUserModal(true);
            }} disabled={permissions.filter(p => p.name === PermissionsKeys.InsertUser).length === 0}
                    className="add-user-button">{t('accounts:users_grid:add')}</Button>
            {data.length === 0 && !isLoading &&
                <span className="no-rows-message">{t('accounts:users_grid:no_rows')}</span>}
            <div className="grid-container ag-theme-alpine" style={{height: 'calc(100% - 36px)'}}>
                {isLoading ? <GridLoading columns={columnDefs}/> : <AgGridReact
                    ref={gridRef}
                    rowData={data}
                    columnDefs={columnDefs}
                    overlayNoRowsTemplate={getOverlayNoRowsTemplate()}
                />}
            </div>
        </div>)
}