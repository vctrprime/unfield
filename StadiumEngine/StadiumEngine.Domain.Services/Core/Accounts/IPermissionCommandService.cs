namespace StadiumEngine.Domain.Services.Core.Accounts;

public interface IPermissionCommandService
{
    Task SyncPermissionsAsync();
}