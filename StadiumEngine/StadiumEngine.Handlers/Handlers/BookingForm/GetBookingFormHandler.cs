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
            Fields = fields.Where( x => x.IsActive ).Select(
                x => new BookingFormFieldDto
                {
                    Data = Mapper.Map<FieldDto>( x ),
                    MinPrice = 3000,
                    StadiumName = String.IsNullOrEmpty( request.StadiumToken ) ? "Андреевская 2, ФЦ" : null,
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
                                },
                                new BookingFormFieldSlotPriceDto
                                {
                                    TariffName = "Тариф \"Будние дни\"",
                                    Value = 3200
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
                        },
                        new BookingFormFieldSlotDto
                        {
                            Name = "13:00",
                            Prices = new List<BookingFormFieldSlotPriceDto>
                            {
                                new BookingFormFieldSlotPriceDto
                                {
                                    TariffName = "Тариф \"Выходные дни\"",
                                    Value = 3200
                                }
                            }
                        },
                        new BookingFormFieldSlotDto
                        {
                            Name = "14:00",
                            Prices = new List<BookingFormFieldSlotPriceDto>
                            {
                                new BookingFormFieldSlotPriceDto
                                {
                                    TariffName = "Тариф \"Выходные дни\"",
                                    Value = 3200
                                }
                            }
                        },
                        new BookingFormFieldSlotDto
                        {
                            Name = "15:00",
                            Prices = new List<BookingFormFieldSlotPriceDto>
                            {
                                new BookingFormFieldSlotPriceDto
                                {
                                    TariffName = "Тариф \"Выходные дни\"",
                                    Value = 3200
                                }
                            }
                        },
                        new BookingFormFieldSlotDto
                        {
                            Name = "16:00",
                            Prices = new List<BookingFormFieldSlotPriceDto>
                            {
                                new BookingFormFieldSlotPriceDto
                                {
                                    TariffName = "Тариф \"Выходные дни\"",
                                    Value = 3200
                                }
                            }
                        },
                        new BookingFormFieldSlotDto
                        {
                            Name = "17:00",
                            Prices = new List<BookingFormFieldSlotPriceDto>
                            {
                                new BookingFormFieldSlotPriceDto
                                {
                                    TariffName = "Тариф \"Выходные дни\"",
                                    Value = 3200
                                }
                            }
                        },
                        new BookingFormFieldSlotDto
                        {
                            Name = "18:00",
                            Prices = new List<BookingFormFieldSlotPriceDto>
                            {
                                new BookingFormFieldSlotPriceDto
                                {
                                    TariffName = "Тариф \"Выходные дни\"",
                                    Value = 3200
                                }
                            }
                        },
                        new BookingFormFieldSlotDto
                        {
                            Name = "19:00",
                            Prices = new List<BookingFormFieldSlotPriceDto>
                            {
                                new BookingFormFieldSlotPriceDto
                                {
                                    TariffName = "Тариф \"Выходные дни\"",
                                    Value = 3200
                                }
                            }
                        },
                        new BookingFormFieldSlotDto
                        {
                            Name = "20:00",
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