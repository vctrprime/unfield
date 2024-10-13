export interface ChangeUserPasswordCommand {
    oldPassword: string,
    newPassword: string,
    newPasswordRepeat: string
}