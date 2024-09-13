using StadiumEngine.Domain.Services.Identity;
using StadiumEngine.Domain.Services.Infrastructure;

namespace StadiumEngine.Services.Facades.Accounts;

internal class UserServiceFacade : IUserServiceFacade
{
    private readonly IHasher _hasher;
    private readonly IPasswordGenerator _passwordGenerator;
    private readonly IPasswordValidator _passwordValidator;
    private readonly IPhoneNumberChecker _phoneNumberChecker;

    public UserServiceFacade(
        IPasswordGenerator passwordGenerator,
        IHasher hasher,
        IPhoneNumberChecker phoneNumberChecker,
        IPasswordValidator passwordValidator )
    {
        _passwordGenerator = passwordGenerator;
        _hasher = hasher;
        _phoneNumberChecker = phoneNumberChecker;
        _passwordValidator = passwordValidator;
    }

    public string GeneratePassword( int length ) => _passwordGenerator.Generate( length );

    public string CryptPassword( string password ) => _hasher.Crypt( password );

    public bool CheckPassword( string secretPassword, string password ) => _hasher.Check( secretPassword, password );

    public string CheckPhoneNumber( string phoneNumber ) => _phoneNumberChecker.Check( phoneNumber );

    public bool ValidatePassword( string password ) => _passwordValidator.Validate( password );
}