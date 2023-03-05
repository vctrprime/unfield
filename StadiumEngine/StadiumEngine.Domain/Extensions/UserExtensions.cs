using System;
using System.Security.Cryptography;
using System.Text;
using StadiumEngine.Domain.Entities.Accounts;

namespace StadiumEngine.Domain.Extensions;

public static class UserExtensions
{
    public static Guid GetUserToken( this User user )
    {
        return CreateGuid( user, 0 );
    }

    public static Guid GetUserToken( this User user, int stadiumId )
    {
        return CreateGuid( user, stadiumId );
    }

    private static Guid CreateGuid( User user, int stadiumId )
    {
        using var md5 = MD5.Create();
        var hash = md5.ComputeHash(
            Encoding.Unicode.GetBytes(
                $"{user.LegalId}{( user.RoleId.HasValue ? $"{user.RoleId}" : "" )}{user.Name}-{user.LastName}{user.Id}-{stadiumId}" ) );
        return new Guid( hash );
    }
}