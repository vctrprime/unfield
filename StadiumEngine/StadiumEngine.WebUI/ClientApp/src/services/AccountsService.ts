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

export interface IAccountsService {
    authorize(command: AuthorizeUserCommand): Promise<AuthorizeUserDto>;
    logout(): Promise<void>;
    getCurrentUserPermissions(): Promise<UserPermissionDto[]>;
    getCurrentUserStadiums(): Promise<UserStadiumDto[]>;
    changeCurrentStadium(stadiumId: number): Promise<AuthorizeUserDto>;
    getUsers(): Promise<UserDto[]>;
    getRoles(): Promise<RoleDto[]>;
    addRole(command: AddRoleCommand): Promise<void>;
    updateRole(command: UpdateRoleCommand): Promise<void>;
    deleteRole(roleId: number): Promise<void>;
    getStadiums(roleId: number): Promise<StadiumDto[]>;
    getPermissions(roleId: number): Promise<PermissionDto[]>;
    toggleRolePermission(command: ToggleRolePermissionCommand): Promise<void>;
    toggleRoleStadium(command: ToggleRoleStadiumCommand): Promise<void>;
    changeLanguage(language: string): Promise<void>;
    
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
            successMessage: t('common:success_request')
        })
    }

    deleteRole(roleId: number): Promise<void> {
        return this.fetchWrapper.delete({
            url: `${this.baseUrl}/roles/${roleId}`,
            successMessage: t('common:success_request')
        })
    }

    updateRole(command: UpdateRoleCommand): Promise<void> {
        return this.fetchWrapper.put({
            url: `${this.baseUrl}/roles`,
            body: command,
            successMessage: t('common:success_request')
        })
    }
    
}

