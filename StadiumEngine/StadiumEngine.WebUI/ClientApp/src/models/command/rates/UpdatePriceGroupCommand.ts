export interface UpdatePriceGroupCommand {
    id: number,
    name: string,
    description?: string,
    isActive: boolean
}