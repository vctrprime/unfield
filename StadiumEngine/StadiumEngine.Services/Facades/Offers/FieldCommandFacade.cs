using StadiumEngine.Common;
using StadiumEngine.Common.Enums.Offers;
using StadiumEngine.Common.Exceptions;
using StadiumEngine.Common.Models;
using StadiumEngine.Domain;
using StadiumEngine.Domain.Entities.Offers;
using StadiumEngine.Domain.Repositories.Offers;
using StadiumEngine.Domain.Services.Facades.Offers;
using StadiumEngine.Domain.Services.Infrastructure;
using StadiumEngine.Services.Facades.Services.Offers;

namespace StadiumEngine.Services.Facades.Offers;

internal class FieldCommandFacade : BaseOfferCommandFacade<Field>, IFieldCommandFacade
{
    private readonly IFieldServiceFacade _fieldServiceFacade;

    public FieldCommandFacade(
        IFieldServiceFacade fieldServiceFacade,
        IOffersImageRepository imageRepository,
        IOffersSportKindRepository offersSportKindRepository,
        IImageService imageService ) : base( imageRepository, imageService, offersSportKindRepository )
    {
        _fieldServiceFacade = fieldServiceFacade;
    }

    protected override string ImageFolder => "fields";

    public async Task AddField( Field field, List<ImageFile> images, int legalId ) =>
        await base.AddOffer( field, images, legalId );

    public async Task UpdateField( Field field, List<ImageFile> images, List<SportKind> sportKinds ) =>
        await base.UpdateOffer( field, images, sportKinds );

    public async Task DeleteField( int fieldId, int stadiumId )
    {
        Field? field = await _fieldServiceFacade.GetField( fieldId, stadiumId );

        if ( field == null )
        {
            throw new DomainException( ErrorsKeys.FieldNotFound );
        }

        if ( field.ChildFields.Any() )
        {
            throw new DomainException( ErrorsKeys.FieldHasChildrenFields );
        }

        _fieldServiceFacade.RemoveField( field );

        DeleteAllImagesAndSportKinds( field );
    }

    protected override async Task AddOffer( Field field ) => await _fieldServiceFacade.AddField( field );

    protected override async Task UpdateOffer( Field field ) => await _fieldServiceFacade.UpdateField( field );

    protected override OffersSportKind CreateSportKind( int fieldId, int userId, SportKind sportKind ) =>
        new()
        {
            FieldId = fieldId,
            UserCreatedId = userId,
            SportKind = sportKind
        };

    protected override OffersImage CreateImage( int fieldId, int userId, string path, int order ) =>
        new()
        {
            FieldId = fieldId,
            Path = path,
            Order = order,
            UserCreatedId = userId
        };
}