using Unfield.Common;
using Unfield.Common.Enums.Offers;
using Unfield.Common.Exceptions;
using Unfield.Common.Models;
using Unfield.Domain;
using Unfield.Domain.Entities.Offers;
using Unfield.Domain.Repositories.Offers;
using Unfield.Domain.Services.Core.Offers;
using Unfield.Domain.Services.Infrastructure;
using Unfield.Services.Facades.Offers;

namespace Unfield.Services.Core.Offers;

internal class FieldCommandService : BaseOfferCommandService<Field>, IFieldCommandService
{
    private readonly IFieldServiceFacade _fieldServiceFacade;

    public FieldCommandService(
        IFieldServiceFacade fieldServiceFacade,
        IOffersImageRepository imageRepository,
        IOffersSportKindRepository offersSportKindRepository,
        IImageService imageService ) : base( imageRepository, imageService, offersSportKindRepository )
    {
        _fieldServiceFacade = fieldServiceFacade;
    }

    protected override string ImageFolder => "fields";

    public async Task AddFieldAsync( Field field, List<ImageFile> images, int stadiumGroupId ) =>
        await base.AddOfferAsync( field, images, stadiumGroupId );

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