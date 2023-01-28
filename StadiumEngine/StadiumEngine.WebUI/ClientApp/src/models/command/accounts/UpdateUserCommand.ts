export interface UpdateUserCommand {
    id: number;
    name: string;
    lastName?: string;
    roleId: number;
    description?: string;
}