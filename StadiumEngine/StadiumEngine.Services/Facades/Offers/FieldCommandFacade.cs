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

    public async Task AddFieldAsync( Field field, List<ImageFile> images, int legalId ) =>
        await base.AddOfferAsync( field, images, legalId );

    public async Task UpdateFieldAsync( Field field, List<ImageFile> images, List<SportKind> sportKinds ) =>
        await base.UpdateOfferAsync( field, images, sportKinds );

    public async Task DeleteFieldAsync( int fieldId, int stadiumId )
    {
        Field? field = await _fieldServiceFacade.GetFieldAsync( fieldId, stadiumId );

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

    protected override async Task AddOfferAsync( Field field ) => await _fieldServiceFacade.AddFieldAsync( field );

    protected override async Task UpdateOfferAsync( Field field ) => await _fieldServiceFacade.UpdateFieldAsync( field );

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