namespace StadiumEngine.Services.Facades.Accounts;

internal interface IUserServiceFacade
{
    string GeneratePassword( int length );
    string CryptPassword( string password );
    bool CheckPassword( string secretPassword, string password );
    string CheckPhoneNumber( string phoneNumber );
    bool ValidatePassword( string password );
}