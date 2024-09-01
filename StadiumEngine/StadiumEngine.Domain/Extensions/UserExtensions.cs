using System;
using System.Security.Cryptography;
using System.Text;
using StadiumEngine.Domain.Entities.Accounts;

namespace StadiumEngine.Domain.Extensions;

public static class UserExtensions
{
    public static Guid GetUserToken( this User user ) => CreateGuid( user, 0 );

    public static Guid GetUserToken( this User user, int stadiumId ) => CreateGuid( user, stadiumId );

    private static Guid CreateGuid( User user, int stadiumId )
    {
        using MD5 md5 = MD5.Create();
        byte[] hash = md5.ComputeHash(
            Encoding.Unicode.GetBytes(
                $"{user.StadiumGroupId}{( user.RoleId.HasValue ? $"{user.RoleId}" : "" )}{user.Name}-{user.LastName}{user.Id}-{stadiumId}" ) );
        return new Guid( hash );
    }
}