namespace StadiumEngine.Domain.Services.Facades.Accounts;

public interface IPermissionCommandFacade
{
    Task SyncPermissionsAsync();
}