import {AuthorizeUserDto} from "../models/dto/accounts/AuthorizeUserDto";
import {AuthorizeUserCommand} from "../models/command/accounts/AuthorizeUserCommand";
import {UserPermissionDto} from "../models/dto/accounts/UserPermissionDto";
import {BaseService} from "./BaseService";
import {ToggleRolePermissionCommand} from "../models/command/accounts/ToggleRolePermissionCommand";
import {RoleDto} from "../models/dto/accounts/RoleDto";
import {PermissionDto} from "../models/dto/accounts/PermissionDto";
import {UserStadiumDto} from "../models/dto/accounts/UserStadiumDto";
import {StadiumDto} from "../models/dto/accounts/StadiumDto";
import {UserDto} from "../models/dto/accounts/UserDto";
import {t} from "i18next";
import {ToggleRoleStadiumCommand} from "../models/command/accounts/ToggleRoleStadiumCommand";
import {AddRoleCommand} from "../models/command/accounts/AddRoleCommand";
import {UpdateRoleCommand} from "../models/command/accounts/UpdateRoleCommand";
import {AddUserCommand} from "../models/command/accounts/AddUserCommand";
import {UpdateUserCommand} from "../models/command/accounts/UpdateUserCommand";
import {ChangeUserPasswordCommand} from "../models/command/accounts/ChangeUserPasswordCommand";
import {ResetUserPasswordCommand} from "../models/command/accounts/ResetUserPasswordCommand";

export interface IAccountsService {
    authorize(command: AuthorizeUserCommand): Promise<AuthorizeUserDto>;
    logout(): Promise<void>;
    getCurrentUserPermissions(): Promise<UserPermissionDto[]>;
    getCurrentUserStadiums(): Promise<UserStadiumDto[]>;
    changeCurrentStadium(stadiumId: number): Promise<AuthorizeUserDto>;
    getUsers(): Promise<UserDto[]>;
    addUser(command: AddUserCommand): Promise<void>;
    updateUser(command: UpdateUserCommand): Promise<void>;
    deleteUser(userId: number): Promise<void>;
    getRoles(): Promise<RoleDto[]>;
    addRole(command: AddRoleCommand): Promise<void>;
    updateRole(command: UpdateRoleCommand): Promise<void>;
    deleteRole(roleId: number): Promise<void>;
    getStadiums(roleId: number): Promise<StadiumDto[]>;
    getPermissions(roleId: number): Promise<PermissionDto[]>;
    toggleRolePermission(command: ToggleRolePermissionCommand): Promise<void>;
    toggleRoleStadium(command: ToggleRoleStadiumCommand): Promise<void>;
    changeLanguage(language: string): Promise<void>;
    changePassword(command: ChangeUserPasswordCommand): Promise<void>;
    resetPassword(command: ResetUserPasswordCommand): Promise<void>;
}

export class AccountsService extends BaseService implements IAccountsService  {
    constructor() {
        super("api/accounts");
    }
    
    authorize(command: AuthorizeUserCommand): Promise<AuthorizeUserDto> {
        return this.fetchWrapper.post({
            url: `${this.baseUrl}/login`,
            body: command,
            hideSpinner: false
        })
    }

    logout(): Promise<void> {
        return this.fetchWrapper.delete({
            url: `${this.baseUrl}/logout`
        });
    }

    getCurrentUserPermissions(): Promise<UserPermissionDto[]> {
        return this.fetchWrapper.get({
            url: `${this.baseUrl}/user-permissions`, 
            withSpinner: true, 
            hideSpinner: true
        });
    }


    getCurrentUserStadiums(): Promise<UserStadiumDto[]> {
        return this.fetchWrapper.get({
            url: `${this.baseUrl}/user-stadiums`,
            withSpinner: true,
            hideSpinner: false
        })
    }

    changeCurrentStadium(stadiumId: number): Promise<AuthorizeUserDto> {
        return this.fetchWrapper.put({
            url: `${this.baseUrl}/change-stadium/` + stadiumId
        })
    }

    getRoles(): Promise<RoleDto[]> {
        return this.fetchWrapper.get({
            url: `${this.baseUrl}/roles`,
            withSpinner: false,
            hideSpinner: false
        })
    }

    getStadiums(roleId: number): Promise<StadiumDto[]> {
        return this.fetchWrapper.get({
            url: `${this.baseUrl}/stadiums/${roleId}`
        })
    }
    
    getPermissions(roleId: number): Promise<PermissionDto[]> {
        return this.fetchWrapper.get({
            url: `${this.baseUrl}/permissions/${roleId}`, 
            withSpinner: false, 
            hideSpinner: false
        })
    }
    
    toggleRolePermission(command: ToggleRolePermissionCommand): Promise<void> {
        return this.fetchWrapper.post({
            url: `${this.baseUrl}/role-permission`,
            body: command,
            successMessage: t('common:success_request')
        })
    }

    getUsers(): Promise<UserDto[]> {
        return this.fetchWrapper.get({
            url: `${this.baseUrl}/users`, 
            withSpinner: false, 
            hideSpinner: false
        })
    }

    changeLanguage(language: string): Promise<void> {
        return this.fetchWrapper.put({
            url: `${this.baseUrl}/user-language/${language}`,
            hideSpinner: false
        })
    }

    toggleRoleStadium(command: ToggleRoleStadiumCommand): Promise<void> {
        return this.fetchWrapper.post({
            url: `${this.baseUrl}/role-stadium`,
            body: command,
            successMessage: t('common:success_request')
        })
    }

    addRole(command: AddRoleCommand): Promise<void> {
        return this.fetchWrapper.post({
            url: `${this.baseUrl}/roles`,
            body: command,
            successMessage: t('common:success_request'),
            withSpinner: false,
            showErrorAlert: false
        })
    }

    deleteRole(roleId: number): Promise<void> {
        return this.fetchWrapper.delete({
            url: `${this.baseUrl}/roles/${roleId}`,
            successMessage: t('common:success_request'),
            withSpinner: false
        })
    }

    updateRole(command: UpdateRoleCommand): Promise<void> {
        return this.fetchWrapper.put({
            url: `${this.baseUrl}/roles`,
            body: command,
            successMessage: t('common:success_request'),
            withSpinner: false,
            showErrorAlert: false
        })
    }

    addUser(command: AddUserCommand): Promise<void> {
        return this.fetchWrapper.post({
            url: `${this.baseUrl}/users`,
            body: command,
            successMessage: t('common:success_request'),
            withSpinner: false,
            showErrorAlert: false
        })
    }
    updateUser(command: UpdateUserCommand): Promise<void> {
        return this.fetchWrapper.put({
            url: `${this.baseUrl}/users`,
            body: command,
            successMessage: t('common:success_request'),
            withSpinner: false,
            showErrorAlert: false
        })
    }
    deleteUser(userId: number): Promise<void> {
        return this.fetchWrapper.delete({
            url: `${this.baseUrl}/users/${userId}`,
            successMessage: t('common:success_request'),
            withSpinner: false
        })
    }

    changePassword(command: ChangeUserPasswordCommand): Promise<void> {
        return this.fetchWrapper.put({
            url: `${this.baseUrl}/user-password/change`,
            body: command,
            showErrorAlert: false,
            withSpinner: false,
            successMessage: t('accounts:change_password:success')
        })
    }

    resetPassword(command: ResetUserPasswordCommand): Promise<void> {
        return this.fetchWrapper.put({
            url: `${this.baseUrl}/user-password/reset`,
            body: command,
            showErrorAlert: false,
            withSpinner: false,
            successMessage: t('accounts:reset_password:success')
        })
    }


}

