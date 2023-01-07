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
                new() { Name = "get-bookings", DisplayName = "Просмотр броней", Description = "Просмотр расписания и забронированных слотов", Sort = 1}
            }
        },
        new PermissionGroup
        {
            Key = "actives",
            Sort = 3,
            Name = "Активы",
            Permissions = new List<Permission>
            {
                new() { Name = "get-fields", DisplayName = "Просмотр площадок", Description = "Просмотр списка площадок", Sort = 1}
            }
        },
        new PermissionGroup
        {
            Key = "employees",
            Sort = 4,
            Name = "Персонал",
            Permissions = new List<Permission>
            {
                new() { Name = "get-employees", DisplayName = "Просмотр сотрудников", Description = "Просмотр списка сотрудников", Sort = 1}
            }
        },
        new PermissionGroup
        {
            Key = "reports",
            Sort = 5,
            Name = "Отчетность",
            Permissions = new List<Permission>
            {
                new() { Name = "get-reports", DisplayName = "Просмотр отчетов", Description = "Доступ к ознакомлению с отчетами", Sort = 1}
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
                new() { Name = "get-users", DisplayName = "Просмотр пользователей", Description = "Просмотр списка пользователей", Sort = 1},
                new() { Name = "get-roles", DisplayName = "Просмотр ролей", Description = "Просмотр списка ролей", Sort = 2},
                new() { Name = "insert-user", DisplayName = "Добавление пользователя", Description = "Возможность добавить пользователя", Sort = 5},
                new() { Name = "insert-role", DisplayName = "Добавление роли", Description = "Возможность добавить роль", Sort = 6},
                new() { Name = "update-user", DisplayName = "Обновление пользователя", Description = "Возможность изменить данные пользователя", Sort = 7},
                new() { Name = "update-role", DisplayName = "Обновление роли", Description = "Возможность изменить данные роли", Sort = 8},
                new() { Name = "delete-user", DisplayName = "Удаление пользователя", Description = "Возможность удалить пользователя", Sort = 9},
                new() { Name = "delete-role", DisplayName = "Удаление роли", Description = "Возможность удалить роль", Sort = 10},
                new() { Name = "get-permissions", DisplayName = "Просмотр разрешений", Description = "Просмотр списка разрешений", Sort = 3},
                new() { Name = "get-stadiums", DisplayName = "Просмотр объектов", Description = "Просмотр списка объектов для роли", Sort = 4},
                new() { Name = "toggle-role-permission", DisplayName = "Связь \"роль-разрешение\"", Description = "Возможность добавить или удалить связь роли с разрешением", Sort = 11},
                new() { Name = "toggle-role-stadium", DisplayName = "Связь \"роль-объект\"", Description = "Возможность добавить или удалить связь роли с объектом", Sort = 12},
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
                new() { Name = "update-main-settings", DisplayName = "Обновление основных настроек", Description = "Возможность изменить основные данные по объекту", Sort = 1},
            }
        },
    };
}