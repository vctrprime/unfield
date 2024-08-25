using Mediator;
using StadiumEngine.Common.Static;
using StadiumEngine.Domain.Entities.Dashboard;
using StadiumEngine.DTO.Schedule;
using StadiumEngine.DTO.Settings.Main;
using StadiumEngine.Queries.Schedule;
using StadiumEngine.Queries.Settings.Main;

namespace StadiumEngine.Jobs.Services.Builders.Dashboard;

internal class StadiumDashboardDataBuilder : IStadiumDashboardDataBuilder
{
    private readonly IMediator _mediator;

    public StadiumDashboardDataBuilder( IMediator mediator )
    {
        _mediator = mediator;
    }

    public async Task<StadiumDashboard> BuildAsync( int stadiumId, DateTime date )
    {
        DateTime startDate = date.AddMonths( -11 );
        startDate = new DateTime( startDate.Year, startDate.Month, 1 );

        List<BookingListItemDto> bookings = await _mediator.Send(
            new GetBookingListQuery
            {
                Start = startDate,
                End = date,
                StadiumId = stadiumId
            } );

        MainSettingsDto settings =await _mediator.Send( new GetMainSettingsQuery
        {
            StadiumId = stadiumId
        } );

        return new StadiumDashboard
        {
            StadiumId = stadiumId,
            Data = new StadiumDashboardData
            {
                YearChart = BuildYearChart( bookings, startDate ),
                FieldDistribution = BuildFieldDistribution( bookings ),
                AverageBill = BuildAverageBill( bookings ),
                PopularInventory = BuildPopularInventory( bookings ),
                TimeChart = BuildTimeChart( bookings, settings )
            }
        };
    }

    private StadiumDashboardYearChart BuildYearChart( List<BookingListItemDto> bookings, DateTime startDate )
    {
        StadiumDashboardYearChart result = new StadiumDashboardYearChart
        {
            Items = new List<StadiumDashboardYearChartItem>()
        };

        List<IGrouping<string, BookingListItemDto>> groupedBookings = bookings
            .Where( x => x.Day.HasValue )
            .GroupBy( x => $"{x.Day.Value.Month}.{x.Day.Value:yy}" )
            .ToList();

        int i = 0;

        while ( i < 12 )
        {
            DateTime date = startDate.AddMonths( i );
            string key = $"{date.Month}.{date:yy}";

            IGrouping<string, BookingListItemDto>? group = groupedBookings
                .SingleOrDefault( x => x.Key == key );

            result.Items.Add(
                new StadiumDashboardYearChartItem
                {
                    Month = group?.Key ?? key,
                    Value = group?.Count() ?? 0,
                } );

            i++;
        }

        return result;
    }

    private StadiumDashboardFieldDistribution BuildFieldDistribution( List<BookingListItemDto> bookings ) =>
        new()
        {
            Items = bookings.GroupBy( x => x.FieldName ).Select(
                x => new StadiumDashboardFieldDistributionItem
                {
                    Field = x.Key,
                    Value = x.Count()
                } ).ToList()
        };

    private StadiumDashboardAverageBill BuildAverageBill( List<BookingListItemDto> bookings ) =>
        new()
        {
            FieldValue =
                Math.Round( bookings.Select( b => b.OriginalData.FieldAmount ).DefaultIfEmpty( 0 ).Average(), 2 ),
            InventoryValue =
                Math.Round( bookings.Select( b => b.OriginalData.InventoryAmount ).DefaultIfEmpty( 0 ).Average(), 2 ),
            TotalValue = Math.Round(
                bookings.Select( b => b.OriginalData.TotalAmountAfterDiscount ).DefaultIfEmpty( 0 ).Average(),
                2 )
        };

    private StadiumDashboardPopularInventory BuildPopularInventory( List<BookingListItemDto> bookings ) =>
        new()
        {
            Items = bookings
                .SelectMany( x => x.OriginalData.Inventories )
                .GroupBy( x => x.Inventory.Name )
                .Select(
                    x => new StadiumDashboardPopularInventoryItem
                    {
                        Inventory = x.Key,
                        Value = x.Sum( i => i.Quantity )
                    } ).ToList()
        };

    private StadiumDashboardTimeChart BuildTimeChart( List<BookingListItemDto> bookings, MainSettingsDto settings )
    {
        StadiumDashboardTimeChart result = new StadiumDashboardTimeChart
        {
            Items = new List<StadiumDashboardTimeChartItem>()
        };

        decimal i = settings.OpenTime;
        while ( i < settings.CloseTime )
        {
            string slot = TimePointParser.Parse( i );

            result.Items.Add(
                new StadiumDashboardTimeChartItem
                {
                    Time = slot,
                    Value = bookings.SelectMany( x => x.OriginalData.Costs ).Count( x => x.StartHour == i )
                } );

            i += ( decimal )0.5;
        }

        return result;
    }
}