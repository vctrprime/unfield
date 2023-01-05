using StadiumEngine.Domain.Entities.Accounts;

namespace StadiumEngine.Handlers.Static;

public static class PermissionSet
{
    public static List<PermissionGroup> PermissionGroups = new List<PermissionGroup>
    {
        new PermissionGroup
        {
            Key = "schedule",
            Sort = 2,
            Name = "Шахматка броней",
            Permissions = new List<Permission>
            {
                new() { Name = "get-bookings", DisplayName = "Просмотр броней", Sort = 1}
            }
        },
        new PermissionGroup
        {
            Key = "actives",
            Sort = 3,
            Name = "Активы",
            Permissions = new List<Permission>
            {
                new() { Name = "get-fields", DisplayName = "Просмотр площадок", Sort = 1}
            }
        },
        new PermissionGroup
        {
            Key = "employees",
            Sort = 4,
            Name = "Персонал",
            Permissions = new List<Permission>
            {
                new() { Name = "get-employees", DisplayName = "Просмотр сотрудников", Sort = 1}
            }
        },
        new PermissionGroup
        {
            Key = "reports",
            Sort = 5,
            Name = "Отчетность",
            Permissions = new List<Permission>
            {
                new() { Name = "get-reports", DisplayName = "Просмотр отчетов", Sort = 1}
            }
        },
        new PermissionGroup
        {
            Key = "accounts",
            Sort = 6,
            Name = "Аккаунты и роли",
            Description = "Управление доступом к приложению",
            Permissions = new List<Permission>
            {
                new() { Name = "get-users", DisplayName = "Просмотр пользователей", Sort = 1},
                new() { Name = "get-roles", DisplayName = "Просмотр ролей", Sort = 5},
                new() { Name = "insert-user", DisplayName = "Добавление пользователя", Sort = 2},
                new() { Name = "insert-role", DisplayName = "Добавление роли", Sort = 6},
                new() { Name = "update-user", DisplayName = "Обновление пользователя", Sort = 3},
                new() { Name = "update-role", DisplayName = "Обновление роли", Sort = 7},
                new() { Name = "delete-user", DisplayName = "Удаление пользователя", Sort = 4},
                new() { Name = "delete-role", DisplayName = "Удаление роли", Sort = 8},
                new() { Name = "get-permissions", DisplayName = "Просмотр разрешений", Sort = 9},
                new() { Name = "get-stadiums", DisplayName = "Просмотр объектов", Sort = 10},
            }
        },
        new PermissionGroup
        {
            Key = "main-settings",
            Sort = 1,
            Name = "Основные настройки",
            Description = "Управление основными настройками объекта",
            Permissions = new List<Permission>
            {
                new() { Name = "update-main-settings", DisplayName = "Обновление основных настроек", Sort = 1},
            }
        },
    };
}