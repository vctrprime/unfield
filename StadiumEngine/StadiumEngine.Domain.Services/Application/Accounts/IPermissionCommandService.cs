namespace StadiumEngine.Domain.Services.Application.Accounts;

public interface IPermissionCommandService
{
    Task SyncPermissionsAsync();
}