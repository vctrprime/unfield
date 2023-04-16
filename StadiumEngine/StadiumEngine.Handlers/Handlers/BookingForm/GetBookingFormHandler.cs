using AutoMapper;
using StadiumEngine.Domain.Entities.Offers;
using StadiumEngine.Domain.Services.Facades.BookingForm;
using StadiumEngine.Domain.Services.Facades.Offers;
using StadiumEngine.Domain.Services.Identity;
using StadiumEngine.DTO.BookingForm;
using StadiumEngine.DTO.Offers.Fields;
using StadiumEngine.Queries.BookingForm;

namespace StadiumEngine.Handlers.Handlers.BookingForm;

internal sealed class GetBookingFormHandler : BaseRequestHandler<GetBookingFormQuery, BookingFormDto>
{
    private readonly IBookingFormQueryFacade _bookingFormQueryFacade;

    public GetBookingFormHandler( IBookingFormQueryFacade bookingFormQueryFacade, IMapper mapper ) : base( mapper )
    {
        _bookingFormQueryFacade = bookingFormQueryFacade;
    }

    public override async ValueTask<BookingFormDto> Handle( GetBookingFormQuery request,
        CancellationToken cancellationToken )
    {
        List<Field> fields = await _bookingFormQueryFacade.GetFieldsForBookingFormAsync(
            request.StadiumToken,
            request.CityId,
            request.Q );

        BookingFormDto result = new BookingFormDto
        {
            Fields = fields.Select(
                x => new BookingFormFieldDto
                {
                    Data = Mapper.Map<FieldDto>( x ),
                    MinPrice = 3000,
                    StadiumName = String.IsNullOrEmpty( request.StadiumToken ) ? x.Stadium.Name : null,
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