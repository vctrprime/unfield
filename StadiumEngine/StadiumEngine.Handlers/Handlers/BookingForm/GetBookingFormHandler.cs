using AutoMapper;
using StadiumEngine.Domain.Entities.Offers;
using StadiumEngine.Domain.Services.Facades.Offers;
using StadiumEngine.DTO.BookingForm;
using StadiumEngine.DTO.Offers.Fields;
using StadiumEngine.Queries.BookingForm;

namespace StadiumEngine.Handlers.Handlers.BookingForm;

internal sealed class GetBookingFormHandler : BaseRequestHandler<GetBookingFormQuery, BookingFormDto>
{
    private readonly IFieldQueryFacade _fieldQueryFacade;

    public GetBookingFormHandler( 
        IMapper mapper, 
        IFieldQueryFacade fieldQueryFacade ) : base( mapper )
    {
        _fieldQueryFacade = fieldQueryFacade;
    }

    public override async ValueTask<BookingFormDto> Handle( GetBookingFormQuery request, CancellationToken cancellationToken )
    {
        List<Field> fields = await _fieldQueryFacade.GetByStadiumIdAsync( 1 );
        BookingFormDto result = new BookingFormDto
        {
            Fields = fields.Select(
                x => new BookingFormFieldDto
                {
                    Data = Mapper.Map<FieldDto>( x ),
                    MinPrice = 3000,
                    Slots = new List<BookingFormFieldSlotDto>
                    {
                        new BookingFormFieldSlotDto
                        {
                            Name = "11:00",
                            Prices = new List<BookingFormFieldSlotPriceDto>
                            {
                                new BookingFormFieldSlotPriceDto
                                {
                                    TariffName = "Тариф \"Выходные дни\"",
                                    Value = 3000
                                }
                            }
                        },
                        new BookingFormFieldSlotDto
                        {
                            Name = "12:00",
                            Prices = new List<BookingFormFieldSlotPriceDto>
                            {
                                new BookingFormFieldSlotPriceDto
                                {
                                    TariffName = "Тариф \"Выходные дни\"",
                                    Value = 3200
                                }
                            }
                        }
                    }
                } ).ToList()
        };

        return result;
    }
}