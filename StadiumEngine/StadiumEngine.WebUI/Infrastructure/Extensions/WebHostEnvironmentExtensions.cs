using System;
using System.IO;
using System.Linq;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;

namespace StadiumEngine.WebUI.Infrastructure.Extensions;

/// <summary>
///     Extensions for IWebHostEnvironment
/// </summary>
public static class WebHostEnvironmentExtensions
{
    /// <summary>
    ///     Get application version string.
    /// </summary>
    public static void WriteReactEnvAppVersion( this IWebHostEnvironment environment )
    {
        if (!environment.IsDevelopment()) return;
        var version = typeof( Program ).Assembly.GetName().Version;

        var stringVersion = $"v{version}";

        var filePath = $"{environment.ContentRootPath}/ClientApp/.env";

        if (!File.Exists( filePath )) return;

        var fileStringsArray = File.ReadAllLines( filePath );

        var filteredArray =
            fileStringsArray.Where(
                    x =>
                        !x.StartsWith(
                            "REACT_APP_VERSION",
                            StringComparison.OrdinalIgnoreCase ) )
                .Append( $"REACT_APP_VERSION={stringVersion}" )
                .ToArray();

        File.WriteAllLines( filePath, filteredArray );
    }
}