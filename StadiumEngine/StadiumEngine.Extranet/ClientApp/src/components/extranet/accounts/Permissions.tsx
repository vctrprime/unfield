import React, {useEffect, useState} from 'react';
import {useRecoilState, useRecoilValue} from "recoil";
import {rolesAtom} from "../../../state/roles";
import {Checkbox, Dropdown} from "semantic-ui-react";
import Skeleton from 'react-loading-skeleton'
import {permissionsAtom} from "../../../state/permissions";
import {UserPermissionDto} from "../../../models/dto/accounts/UserPermissionDto";
import {RoleDto} from "../../../models/dto/accounts/RoleDto";
import {PermissionDto} from "../../../models/dto/accounts/PermissionDto";
import {useInject} from "inversify-hooks";
import {IAccountsService} from "../../../services/AccountsService";
import {ToggleRolePermissionCommand} from "../../../models/command/accounts/ToggleRolePermissionCommand";
import {t} from "i18next";
import {getTitle} from "../../../helpers/utils";
import {PermissionsKeys} from "../../../static/PermissionsKeys";

export interface PermissionsRoleDropDownData {
    key: number,
    value: number,
    text: string
}

interface PermissionProps {
    permission: PermissionDto | undefined,
    className: string
}

interface PermissionGroupTitleProps {
    groupKey: string
}

interface PermissionCheckBoxProps {
    permission: PermissionDto
}

export const Permissions = () => {
    document.title = getTitle("accounts:permissions_tab")

    const [roles, setRoles] = useRecoilState<PermissionsRoleDropDownData[]>(rolesAtom);
    const permissions = useRecoilValue<UserPermissionDto[]>(permissionsAtom);

    const [accountsService] = useInject<IAccountsService>('AccountsService');

    const [data, setData] = useState<PermissionDto[]>([]);
    const [selectedRoleId, setSelectedRoleId] = useState<number>(roles.length > 0 ? roles[0].value : 0);

    useEffect(() => {
        const fetchRoles = () => {
            accountsService.getRoles().then((result: RoleDto[]) => {
                setRoles(result.map((r) => {
                    return {key: r.id, value: r.id, text: r.name}
                }));
                setSelectedRoleId(result[0].id);
            })
        }

        if (roles.length === 0) {
            fetchRoles();
        }
    }, [])

    useEffect(() => {
        const fetchPermissions = () => {
            setData([]);
            accountsService.getPermissions(selectedRoleId).then((result: PermissionDto[]) => {
                setTimeout(() => {
                    setData(result);
                }, 500);
            })
        }

        if (selectedRoleId !== 0) {
            fetchPermissions();
        }
    }, [selectedRoleId])

    const togglePermission = (permission: PermissionDto) => {
        const command: ToggleRolePermissionCommand = {
            roleId: selectedRoleId,
            permissionId: permission.id
        };
        accountsService.toggleRolePermission(command).then(() => {
            permission.isRoleBound = !permission.isRoleBound;
            const newData = [...data];
            setData(newData);
        })

    }


    const changeRole = (e: any, {value}: any) => {
        setSelectedRoleId(value);
    }


    const PermissionGroupTitle = ({groupKey}: PermissionGroupTitleProps) => {
        return <div className="permission-group-title">
            {data.length === 0 ? <Skeleton width={150} height={20}/> :
                <span>{t(`accounts:permissions_groups:${groupKey.replaceAll('-', '_')}`)}</span>}
        </div>
    }

    const PermissionCheckBox = ({permission}: PermissionCheckBoxProps) => {
        return permissions.filter(p => p.name === PermissionsKeys.ToggleRolePermission).length > 0 ?
            <Checkbox
                checked={permission.isRoleBound}
                onChange={(e, data) => togglePermission(permission)}
                label={t(`accounts:permissions:${permission.name.replaceAll('-', '_')}`)}/> :
            <Checkbox label={t(`accounts:permissions:${permission.name.replaceAll('-', '_')}`)}
                      disabled
                      checked={permission.isRoleBound}/>
    }

    const Permission = ({permission, className}: PermissionProps) => {

        const getDescription = (): string => {
            if (permission === undefined) return '';

            return t(`accounts:permissions:${permission.name.replaceAll('-', '_')}_description`);
        }

        return <div className={className}>
            {data.length === 0 ?
                <div>
                    <Skeleton width={"90%"} height={17}/>
                    <Skeleton width={"80%"} height={8}/>
                </div> :
                permission !== undefined ?
                    <div className="permission-inner">
                        <PermissionCheckBox permission={permission}/>

                        <div title={getDescription()} className="permission-inner-descr">{getDescription()}</div>
                    </div>
                    :

                    <span/>}
        </div>
    }

    return (<div className="permissions-container">
        {selectedRoleId !== 0 ?
            <div>
                {t("accounts:role_permissions_dropdown_title")}:  &nbsp;
                <Dropdown
                    onChange={changeRole}
                    inline
                    options={roles}
                    defaultValue={selectedRoleId}
                />
            </div> : null}
        <div>
            <div className="outer-container"  style={{width: '100%'}}>
                <div className="settings-permission-container inner-container">
                    <PermissionGroupTitle groupKey="settings"/>
                    {new Array(8).fill('').map((v, i) => {
                        return <Permission key={i} className="permission settings-permission"
                                           permission={data.find(p => p.groupKey === 'settings' && p.sortValue === i + 1)}/>

                    })}
                </div>
            </div>
            <div className="outer-container" style={{width: '100%'}}>
                <div className="offers-permissions-container inner-container">
                    <PermissionGroupTitle groupKey="offers"/>
                    {new Array(12).fill('').map((v, i) => {
                        return <Permission key={i} className="permission offers-permission"
                                           permission={data.find(p => p.groupKey === 'offers' && p.sortValue === i + 1)}/>
                    })}
                </div>
            </div>
            <div className="outer-container" style={{width: '100%'}}>
                <div className="rates-permissions-container inner-container">
                    <PermissionGroupTitle groupKey="rates"/>
                    {new Array(12).fill('').map((v, i) => {
                        return <Permission key={i} className="permission rates-permission"
                                           permission={data.find(p => p.groupKey === 'rates' && p.sortValue === i + 1)}/>
                    })}
                </div>
            </div>
            <div className="outer-container" style={{width: '100%'}}>
                <div className="schedule-permissions-container inner-container">
                    <PermissionGroupTitle groupKey="schedule"/>
                    {new Array(4).fill('').map((v, i) => {
                        return <Permission key={i} className="permission schedule-permission" 
                                           permission={data.find(p => p.groupKey === 'schedule' && p.sortValue === i + 1)}/>
                    })}
                </div>
            </div>
            {/*<div className="outer-container" style={{width: '33.33%'}}>
                <div className="inner-container">
                    <PermissionGroupTitle groupKey="employees"/>
                    {new Array(1).fill('').map((v, i) => {
                        return <Permission key={i} className="permission"
                                           permission={data.find(p => p.groupKey === 'employees' && p.sortValue === i + 1)}/>
                    })}
                </div>
            </div>
            <div className="outer-container" style={{width: '33.33%'}}>
                <div className="inner-container">
                    <PermissionGroupTitle groupKey="reports"/>
                    {new Array(1).fill('').map((v, i) => {
                        return <Permission key={i} className="permission"
                                           permission={data.find(p => p.groupKey === 'reports' && p.sortValue === i + 1)}/>
                    })}
                </div>
            </div>*/}
            <div className="outer-container" style={{width: '100%'}}>
                <div className="accounts-permissions-container inner-container">
                    <PermissionGroupTitle groupKey="accounts"/>
                    {new Array(12).fill('').map((v, i) => {
                        return <Permission key={i} className="permission accounts-permission"
                                           permission={data.find(p => p.groupKey === 'accounts' && p.sortValue === i + 1)}/>
                    })}
                </div>

            </div>
        </div>
    </div>);
}