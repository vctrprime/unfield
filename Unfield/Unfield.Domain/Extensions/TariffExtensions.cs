using System;
using Unfield.Domain.Entities.Rates;

namespace Unfield.Domain.Extensions;

public static class TariffExtensions
{
    public static bool HasDayOfWeek( this Tariff tariff, DateTime day )
    {
        switch ( day.DayOfWeek )
        {
            case DayOfWeek.Monday when tariff.Monday:
            case DayOfWeek.Tuesday when tariff.Tuesday:
            case DayOfWeek.Sunday when tariff.Sunday:
            case DayOfWeek.Wednesday when tariff.Wednesday:
            case DayOfWeek.Thursday when tariff.Thursday:
            case DayOfWeek.Friday when tariff.Friday:
            case DayOfWeek.Saturday when tariff.Saturday:
                return true;
            default:
                return false;
        }
    }
}