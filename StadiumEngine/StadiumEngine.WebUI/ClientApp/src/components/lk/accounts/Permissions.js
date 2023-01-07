import React, {useEffect, useState} from 'react';
import {useRecoilState, useRecoilValue} from "recoil";
import {rolesAtom} from "../../../state/roles";
import {useFetchWrapper} from "../../../helpers/fetch-wrapper";
import {Checkbox, Dropdown} from "semantic-ui-react";
import Skeleton from 'react-loading-skeleton'
import {permissionsAtom} from "../../../state/permissions";

export const Permissions = () => {
    const [roles, setRoles] = useRecoilState(rolesAtom);
    const permissions = useRecoilValue(permissionsAtom);
    
    const fetchWrapper = useFetchWrapper();
    
    const [data, setData] = useState([]);
    const [selectedRoleId, setSelectedRoleId] = useState(roles.length > 0 ? roles[0].value : 0);
    
    useEffect(() => {
        if (roles.length === 0) {
            fetchRoles();
        }
    }, [])
    
    useEffect(() => {
        if (selectedRoleId !== 0) {
            fetchPermissions();
        }
    }, [selectedRoleId])
    
    const togglePermission = (permission) => {
        fetchWrapper.post({url: 'api/accounts/role-permission', 
            body: {
                roleId: selectedRoleId,
                permissionId: permission.id
            }
        }).then(() => {
            permission.isRoleBound = !permission.isRoleBound;
            const newData = [...data];
            setData(newData);
        })
       
    }

    const PermissionGroupTitle = ({groupKey}) => {
        return <div className="permission-group-title">
            {data.length === 0 ?  <Skeleton width={150} height={20} /> : <span>{data.filter(p => p.groupKey === groupKey)[0].groupName}</span>}
        </div>
    }
    
    const PermissionCheckBox = ({permission}) => {
        return permissions.filter(p => p.name === 'toggle-role-permission').length > 0 ?
             <Checkbox
                    checked={permission.isRoleBound}
                    onChange={(e, data) => togglePermission(permission)}
                    label={permission.name} /> :
                <Checkbox label={permission.name} 
                          disabled
                          checked={permission.isRoleBound} />
    }
    
    const Permission = ({permission, className}) => {
        return <div className={className}>
            {data.length === 0 ? 
                <div>
                    <Skeleton width={"90%"} height={17} />
                    <Skeleton width={"80%"} height={8} />
                </div>  :
                permission !== undefined ?
                    <div className="permission-inner">
                        <PermissionCheckBox permission={permission} />
                        <div title={permission.description} className="permission-inner-descr">{permission.description}</div>
                    </div>
                     : 
                    
                    <span/>}
            </div>
    }
    
    const fetchRoles = () => {
        fetchWrapper.get({url: 'api/accounts/roles', withSpinner: false, hideSpinner: false}).then((result) => {
            setRoles(result.map((r) => {
                return { key: r.id, value: r.id, text: r.name }
            }));
            setSelectedRoleId(result[0].id);
        })
    }
    
    const fetchPermissions = () => {
        setData([]);
        fetchWrapper.get({url: `api/accounts/permissions/${selectedRoleId}`, withSpinner: false, hideSpinner: false}).then((result) => {
            setTimeout(() => {
                setData(result);
            }, 500);
        })
    }

    const changeRole = (e, { value }) => {
        setSelectedRoleId(value);
    }
    
    
    return (<div className="permissions-container">
        {roles.length > 0 ?
            <div>
                Показаны разрешения для роли:  &nbsp;
                <Dropdown
                    onChange={changeRole}
                    inline
                    options={roles}
                    defaultValue={selectedRoleId}
                    /> 
            </div> : null}
        <div className="outer-container">
            <div className="inner-container">
                <PermissionGroupTitle groupKey="main-settings" />
                {new Array(1).fill('').map((v, i) => {
                    return <Permission key={i} className="permission" permission={data.find(p => p.groupKey === 'main-settings' && p.sortValue === i+1)}/>
                   
                })}
            </div>
        </div>
        <div className="outer-container">
            <div className="inner-container">
                <PermissionGroupTitle groupKey="schedule" />
                {new Array(1).fill('').map((v, i) => {
                    return <Permission key={i} className="permission" permission={data.find(p => p.groupKey === 'schedule' && p.sortValue === i+1)}/>
                })}
            </div>
        </div>
        <div className="outer-container">
            <div className="inner-container">
                <PermissionGroupTitle groupKey="employees" />
                {new Array(1).fill('').map((v, i) => {
                    return <Permission key={i} className="permission" permission={data.find(p => p.groupKey === 'employees' && p.sortValue === i+1)}/>
                })}
            </div>
        </div>
        <div className="outer-container">
            <div className="inner-container">
                <PermissionGroupTitle groupKey="reports" />
                {new Array(1).fill('').map((v, i) => {
                    return <Permission key={i} className="permission" permission={data.find(p => p.groupKey === 'reports' && p.sortValue === i+1)}/>
                })}
            </div>
        </div>
        <div className="outer-container" style={{width: '100%'}}>
            <div className="actives-permissions-container inner-container">
                <PermissionGroupTitle groupKey="actives" />
                {new Array(4).fill('').map((v, i) => {
                    return <Permission key={i} className="permission actives-permission" permission={data.find(p => p.groupKey === 'actives' && p.sortValue === i+1)}/>
                })}
            </div>
        </div>
        <div className="outer-container" style={{width: '100%'}}>
            <div className="accounts-permissions-container inner-container">
                <PermissionGroupTitle groupKey="accounts" />
                {new Array(12).fill('').map((v, i) => {
                    return <Permission key={i} className="permission accounts-permission" permission={data.find(p => p.groupKey === 'accounts' && p.sortValue === i+1)}/>
                })}
            </div>
            
        </div>
    </div>);
}