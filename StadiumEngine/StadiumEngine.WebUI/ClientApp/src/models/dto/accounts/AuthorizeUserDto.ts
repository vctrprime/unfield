export interface AuthorizeUserDto {
    fullName: string,
    roleName: string | null,
    isSuperuser: boolean
}