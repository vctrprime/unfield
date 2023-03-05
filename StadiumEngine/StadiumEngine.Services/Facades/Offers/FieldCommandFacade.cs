using StadiumEngine.Common;
using StadiumEngine.Common.Enums.Offers;
using StadiumEngine.Common.Exceptions;
using StadiumEngine.Common.Models;
using StadiumEngine.Domain.Entities.Offers;
using StadiumEngine.Domain.Repositories.Offers;
using StadiumEngine.Domain.Services.Facades.Offers;
using StadiumEngine.Domain.Services.Infrastructure;

namespace StadiumEngine.Services.Facades.Offers;

internal class FieldCommandFacade : BaseOfferCommandFacade<Field>, IFieldCommandFacade
{
    private readonly IFieldRepository _fieldRepository;
    protected override string ImageFolder => "fields";

    public FieldCommandFacade(
        IFieldRepository fieldRepository,
        IOffersImageRepository imageRepository,
        IOffersSportKindRepository offersSportKindRepository,
        IImageService imageService ) : base( imageRepository, imageService, offersSportKindRepository )
    {
        _fieldRepository = fieldRepository;
    }

    public async Task AddField( Field field, List<ImageFile> images, int legalId )
    {
        await base.AddOffer( field, images, legalId );
    }

    public async Task UpdateField( Field field, List<ImageFile> images, List<SportKind> sportKinds )
    {
        await base.UpdateOffer( field, images, sportKinds );
    }

    public async Task DeleteField( int fieldId, int stadiumId )
    {
        var field = await _fieldRepository.Get( fieldId, stadiumId );

        if (field == null) throw new DomainException( ErrorsKeys.FieldNotFound );

        if (field.ChildFields.Any()) throw new DomainException( ErrorsKeys.FieldHasChildrenFields );

        _fieldRepository.Remove( field );

        DeleteAllImagesAndSportKinds( field );
    }

    protected override void AddOffer( Field field )
    {
        _fieldRepository.Add( field );
    }

    protected override void UpdateOffer( Field field )
    {
        _fieldRepository.Update( field );
    }

    protected override OffersSportKind CreateSportKind( int fieldId, int userId, SportKind sportKind )
    {
        return new OffersSportKind
        {
            FieldId = fieldId,
            UserCreatedId = userId,
            SportKind = sportKind
        };
    }

    protected override OffersImage CreateImage( int fieldId, int userId, string path, int order )
    {
        return new OffersImage
        {
            FieldId = fieldId,
            Path = path,
            Order = order,
            UserCreatedId = userId
        };
    }
}