export interface ChangeCustomerPasswordCommand {
    oldPassword: string,
    newPassword: string,
    newPasswordRepeat: string
}