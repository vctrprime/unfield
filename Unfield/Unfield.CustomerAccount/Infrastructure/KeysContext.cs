using Microsoft.AspNetCore.DataProtection.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Unfield.CustomerAccount.Infrastructure;

/// <summary>
/// 
/// </summary>
public class KeysContext : DbContext, IDataProtectionKeyContext
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="options"></param>
    public KeysContext( DbContextOptions<KeysContext> options ) : base( options )
    {
    }
    
    /// <summary>
    /// 
    /// </summary>
    public DbSet<DataProtectionKey> DataProtectionKeys { get; set; }
}