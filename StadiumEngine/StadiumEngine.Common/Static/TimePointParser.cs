using System.Globalization;

namespace StadiumEngine.Common.Static;

public static class TimePointParser
{
    public static decimal Parse( string point )
    {
        if ( point == "00:00" )
        {
            point = "24:00";
        }
        
        string[] parts = point.Split( ":" );
        if ( parts[ 1 ] == "30" )
        {
            return ( decimal )( Int32.Parse( parts[ 0 ] ) + 0.5 );
        }

        return Int32.Parse( parts[ 0 ] );
    }
    
    public static string Parse( decimal point )
    {
        string[] parts = point.ToString( CultureInfo.InvariantCulture ).Split( "." );
        string hour = $"{Int32.Parse( parts[ 0 ] ):D2}";
        
        return parts.Length == 1 ? $"{hour}:00" : $"{hour}:30";
    }
}