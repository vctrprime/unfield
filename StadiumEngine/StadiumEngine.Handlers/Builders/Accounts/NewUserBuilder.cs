using AutoMapper;
using StadiumEngine.Domain.Entities.Accounts;
using StadiumEngine.Domain.Services.Identity;
using StadiumEngine.Domain.Services.Infrastructure;
using StadiumEngine.Handlers.Commands.Accounts;
using StadiumEngine.Handlers.Commands.Utils;

namespace StadiumEngine.Handlers.Builders.Accounts;

internal class NewUserBuilder
{
    private readonly IMapper _mapper;
    private readonly IPasswordGenerator _passwordGenerator;
    private readonly IHasher _hasher;

    public NewUserBuilder(IMapper mapper, IPasswordGenerator passwordGenerator, IHasher hasher)
    {
        _mapper = mapper;
        _passwordGenerator = passwordGenerator;
        _hasher = hasher;
    }

    public (User, string) Build(AddAdminUserCommand command, int legalId)
    {
        return Build(_mapper.Map<AddUserCommand>(command), legalId);
    }

    public (User, string) Build(AddUserCommand command, int legalId, int? userCreatedId = null)
    {
        var user = _mapper.Map<User>(command);
        
        var userPassword = _passwordGenerator.Generate(8);
        user.Password = _hasher.Crypt(userPassword);
        
        user.LegalId = legalId;
        user.UserCreatedId = userCreatedId;

        return (user, userPassword);
    }
}