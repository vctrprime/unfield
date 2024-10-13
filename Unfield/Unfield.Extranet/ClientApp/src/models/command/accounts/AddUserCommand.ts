export interface AddUserCommand {
    name: string;
    lastName?: string;
    phoneNumber: string;
    roleId: number;
    description?: string;
}