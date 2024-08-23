import {UserPermissionDto} from "./UserPermissionDto";

export interface AuthorizeUserDto {
    fullName: string,
    roleName: string | null,
    isAdmin: boolean,
    isSuperuser: boolean,
    language: string
    legalName: string
    permissions: UserPermissionDto[]
}