export interface PermissionDto {
    id: number;
    name: string;
    description: string;
    sortValue: number;
    groupName: string;
    groupKey: string;
    groupSortValue: number;
    isRoleBound: boolean;
}