namespace StadiumEngine.CustomerAccount.Infrastructure.Extensions;

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
        if ( !environment.IsDevelopment() )
        {
            return;
        }

        Version version = typeof( Program ).Assembly.GetName().Version;

        string stringVersion = $"v{version}";

        string filePath = $"{environment.ContentRootPath}/ClientApp/.env";

        if ( !File.Exists( filePath ) )
        {
            return;
        }

        string[] fileStringsArray = File.ReadAllLines( filePath );

        string[] filteredArray =
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