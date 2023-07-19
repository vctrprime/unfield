using System.Collections.Generic;
using StadiumEngine.Common.Constant;
using StadiumEngine.Domain.Entities.Accounts;

namespace StadiumEngine.Domain.Static;

public static class PermissionSet
{
    public static List<PermissionGroup> PermissionGroups = new()
    {
        new PermissionGroup
        {
            Key = PermissionsKeys.ScheduleGroup,
            Sort = 2,
            Name = "Шахматка броней",
            Permissions = new List<Permission>
            {
                new()
                {
                    Name = PermissionsKeys.GetBookings,
                    DisplayName = "Просмотр броней",
                    Description = "Просмотр расписания и забронированных слотов",
                    Sort = 1
                },
                new()
                {
                    Name = PermissionsKeys.InsertBooking,
                    DisplayName = "Создание бронирования",
                    Description = "Возможность добавить бронирование",
                    Sort = 2
                },
                new()
                {
                    Name = PermissionsKeys.UpdateBooking,
                    DisplayName = "Изменение бронирования",
                    Description = "Возможность изменить данные бронирования",
                    Sort = 3
                },
                new()
                {
                    Name = PermissionsKeys.DeleteBooking,
                    DisplayName = "Отмена бронирования",
                    Description = "Возможность отменить бронирование",
                    Sort = 4
                }
            }
        },
        new PermissionGroup
        {
            Key = PermissionsKeys.OffersGroup,
            Sort = 3,
            Name = "Активы",
            Permissions = new List<Permission>
            {
                new()
                {
                    Name = PermissionsKeys.GetFields,
                    DisplayName = "Просмотр площадок",
                    Description = "Просмотр списка площадок",
                    Sort = 1
                },
                new()
                {
                    Name = PermissionsKeys.InsertField,
                    DisplayName = "Добавление площадки",
                    Description = "Возможность добавить площадку",
                    Sort = 2
                },
                new()
                {
                    Name = PermissionsKeys.UpdateField,
                    DisplayName = "Обновление площадки",
                    Description = "Возможность изменить данные плошадки",
                    Sort = 3
                },
                new()
                {
                    Name = PermissionsKeys.DeleteField,
                    DisplayName = "Удаление площадки",
                    Description = "Возможность удалить площадку",
                    Sort = 4
                },
                new()
                {
                    Name = PermissionsKeys.GetLockerRooms,
                    DisplayName = "Просмотр раздевалок",
                    Description = "Просмотр списка раздевалок",
                    Sort = 5
                },
                new()
                {
                    Name = PermissionsKeys.InsertLockerRoom,
                    DisplayName = "Добавление раздевалки",
                    Description = "Возможность добавить раздевалку",
                    Sort = 6
                },
                new()
                {
                    Name = PermissionsKeys.UpdateLockerRoom,
                    DisplayName = "Обновление раздевалки",
                    Description = "Возможность изменить данные раздевалки",
                    Sort = 7
                },
                new()
                {
                    Name = PermissionsKeys.DeleteLockerRoom,
                    DisplayName = "Удаление раздевалки",
                    Description = "Возможность удалить раздевалку",
                    Sort = 8
                },
                new()
                {
                    Name = PermissionsKeys.GetInventories,
                    DisplayName = "Просмотр инвентаря",
                    Description = "Просмотр списка инвентаря",
                    Sort = 9
                },
                new()
                {
                    Name = PermissionsKeys.InsertInventory,
                    DisplayName = "Добавление инвентаря",
                    Description = "Возможность добавить инвентарь",
                    Sort = 10
                },
                new()
                {
                    Name = PermissionsKeys.UpdateInventory,
                    DisplayName = "Обновление инвентаря",
                    Description = "Возможность изменить данные об инвентаре",
                    Sort = 11
                },
                new()
                {
                    Name = PermissionsKeys.DeleteInventory,
                    DisplayName = "Удаление инвентаря",
                    Description = "Возможность удалить инвентарь",
                    Sort = 12
                }
            }
        },
        new PermissionGroup
        {
            Key = PermissionsKeys.RatesGroup,
            Sort = 4,
            Name = "Тарифы и цены",
            Permissions = new List<Permission>
            {
                new()
                {
                    Name = PermissionsKeys.GetPriceGroups,
                    DisplayName = "Просмотр ценовых групп",
                    Description = "Просмотр списка ценовых групп",
                    Sort = 1
                },
                new()
                {
                    Name = PermissionsKeys.InsertPriceGroup,
                    DisplayName = "Добавление ценовой группы",
                    Description = "Возможность добавить ценовую группу",
                    Sort = 2
                },
                new()
                {
                    Name = PermissionsKeys.UpdatePriceGroup,
                    DisplayName = "Обновление ценовой группы",
                    Description = "Возможность изменить данные ценовой группы",
                    Sort = 3
                },
                new()
                {
                    Name = PermissionsKeys.DeletePriceGroup,
                    DisplayName = "Удаление ценовой группы",
                    Description = "Возможность удалить ценовую группу",
                    Sort = 4
                },
                new()
                {
                    Name = PermissionsKeys.GetTariffs,
                    DisplayName = "Просмотр тарифов",
                    Description = "Просмотр списка тарифов",
                    Sort = 5
                },
                new()
                {
                    Name = PermissionsKeys.InsertTariff,
                    DisplayName = "Добавление тарифа",
                    Description = "Возможность добавить тариф",
                    Sort = 6
                },
                new()
                {
                    Name = PermissionsKeys.UpdateTariff,
                    DisplayName = "Обновление тарифа",
                    Description = "Возможность изменить данные тарифа",
                    Sort = 7
                },
                new()
                {
                    Name = PermissionsKeys.DeleteTariff,
                    DisplayName = "Удаление тарифа",
                    Description = "Возможность удалить тариф",
                    Sort = 8
                },
                new()
                {
                    Name = PermissionsKeys.GetPrices,
                    DisplayName = "Просмотр цен",
                    Description = "Просмотр ценовой матрицы",
                    Sort = 9
                },
                new()
                {
                    Name = PermissionsKeys.SetPrices,
                    DisplayName = "Установка цен",
                    Description = "Возможность изменить цены",
                    Sort = 10
                }
            }
        },
        new PermissionGroup
        {
            Key = PermissionsKeys.EmployeesGroup,
            Sort = 5,
            Name = "Персонал",
            Permissions = new List<Permission>
            {
                new()
                {
                    Name = PermissionsKeys.GetEmployees,
                    DisplayName = "Просмотр сотрудников",
                    Description = "Просмотр списка сотрудников",
                    Sort = 1
                }
            }
        },
        new PermissionGroup
        {
            Key = PermissionsKeys.ReportsGroup,
            Sort = 6,
            Name = "Отчетность",
            Permissions = new List<Permission>
            {
                new()
                {
                    Name = PermissionsKeys.GetReports,
                    DisplayName = "Просмотр отчетов",
                    Description = "Доступ к ознакомлению с отчетами",
                    Sort = 1
                }
            }
        },
        new PermissionGroup
        {
            Key = PermissionsKeys.AccountsGroup,
            Sort = 7,
            Name = "Аккаунты и роли",
            Description = "Управление доступом к приложению",
            Permissions = new List<Permission>
            {
                new()
                {
                    Name = PermissionsKeys.GetUsers,
                    DisplayName = "Просмотр пользователей",
                    Description = "Просмотр списка пользователей",
                    Sort = 1
                },
                new()
                {
                    Name = PermissionsKeys.GetRoles,
                    DisplayName = "Просмотр ролей",
                    Description = "Просмотр списка ролей",
                    Sort = 2
                },
                new()
                {
                    Name = PermissionsKeys.InsertUser,
                    DisplayName = "Добавление пользователя",
                    Description = "Возможность добавить пользователя",
                    Sort = 5
                },
                new()
                {
                    Name = PermissionsKeys.InsertRole,
                    DisplayName = "Добавление роли",
                    Description = "Возможность добавить роль",
                    Sort = 6
                },
                new()
                {
                    Name = PermissionsKeys.UpdateUser,
                    DisplayName = "Обновление пользователя",
                    Description = "Возможность изменить данные пользователя",
                    Sort = 7
                },
                new()
                {
                    Name = PermissionsKeys.UpdateRole,
                    DisplayName = "Обновление роли",
                    Description = "Возможность изменить данные роли",
                    Sort = 8
                },
                new()
                {
                    Name = PermissionsKeys.DeleteUser,
                    DisplayName = "Удаление пользователя",
                    Description = "Возможность удалить пользователя",
                    Sort = 9
                },
                new()
                {
                    Name = PermissionsKeys.DeleteRole,
                    DisplayName = "Удаление роли",
                    Description = "Возможность удалить роль",
                    Sort = 10
                },
                new()
                {
                    Name = PermissionsKeys.GetPermissions,
                    DisplayName = "Просмотр разрешений",
                    Description = "Просмотр списка разрешений",
                    Sort = 3
                },
                new()
                {
                    Name = PermissionsKeys.GetStadiums,
                    DisplayName = "Просмотр объектов",
                    Description = "Просмотр списка объектов для роли",
                    Sort = 4
                },
                new()
                {
                    Name = PermissionsKeys.ToggleRolePermission,
                    DisplayName = "Связь \"роль-разрешение\"",
                    Description = "Возможность добавить или удалить связь роли с разрешением",
                    Sort = 11
                },
                new()
                {
                    Name = PermissionsKeys.ToggleUserStadium,
                    DisplayName = "Связь \"пользователь-объект\"",
                    Description = "Возможность добавить или удалить связь пользователя с объектом",
                    Sort = 12
                }
            }
        },
        new PermissionGroup
        {
            Key = PermissionsKeys.SettingsGroup,
            Sort = 1,
            Name = "Управление объектом",
            Description = "Управление настройками объекта",
            Permissions = new List<Permission>
            {
                new()
                {
                    Name = PermissionsKeys.UpdateMainSettings,
                    DisplayName = "Обновление основных настроек",
                    Description = "Возможность изменить основные данные по объекту",
                    Sort = 5
                },
                new()
                {
                    Name = PermissionsKeys.SyncLockerRoomStatus,
                    DisplayName = "Cинхронизация статуса раздевалки",
                    Description = "Возможность запустить синхронизацию статуса (после уборки, либо принудительная синхронизация)",
                    Sort = 6
                },
                new()
                {
                    Name = PermissionsKeys.GetBreaks,
                    DisplayName = "Просмотр перерывов",
                    Description = "Просмотр списка перерывов",
                    Sort = 1
                },
                new()
                {
                    Name = PermissionsKeys.InsertBreak,
                    DisplayName = "Добавление перерыва",
                    Description = "Возможность добавить перерыв",
                    Sort = 2
                },
                new()
                {
                    Name = PermissionsKeys.UpdateBreak,
                    DisplayName = "Обновление перерыва",
                    Description = "Возможность изменить данные перерыва",
                    Sort = 3
                },
                new()
                {
                    Name = PermissionsKeys.DeleteBreak,
                    DisplayName = "Удаление перерыва",
                    Description = "Возможность удалить перерыв",
                    Sort = 4
                },
            }
        }
    };
}