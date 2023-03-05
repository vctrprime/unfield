using StadiumEngine.Common.Enums.Offers;
using StadiumEngine.Common.Models;
using StadiumEngine.Domain.Entities.Offers;
using StadiumEngine.Domain.Repositories.Offers;
using StadiumEngine.Domain.Services.Infrastructure;

namespace StadiumEngine.Services.Facades.Offers;

internal abstract class BaseOfferCommandFacade<T> where T : BaseOfferEntity
{
    private readonly IOffersImageRepository _imageRepository;
    private readonly IImageService _imageService;
    private readonly IOffersSportKindRepository _offersSportKindRepository;

    protected BaseOfferCommandFacade(
        IOffersImageRepository imageRepository,
        IImageService imageService,
        IOffersSportKindRepository offersSportKindRepository )
    {
        _imageRepository = imageRepository;
        _imageService = imageService;
        _offersSportKindRepository = offersSportKindRepository;
    }

    protected abstract string ImageFolder { get; }

    protected async Task AddOffer( T offer, List<ImageFile> images, int legalId )
    {
        foreach (var offersSportKind in offer.SportKinds) offersSportKind.UserCreatedId = offer.UserCreatedId;

        offer.Images = new List<OffersImage>();

        foreach (var image in images)
        {
            var path = await _imageService.Upload(
                image.FormFile,
                legalId,
                offer.StadiumId,
                $"offers/{ImageFolder}" );
            offer.Images.Add(
                new OffersImage
                {
                    Path = path,
                    Order = images.IndexOf( image ),
                    UserCreatedId = offer.UserCreatedId
                } );
        }

        AddOffer( offer );
    }

    protected async Task UpdateOffer( T offer, List<ImageFile> images, List<SportKind> sportKinds )
    {
        var userId = offer.UserModifiedId ?? 0;

        UpdateOffer( offer );

        var pathsToDelete = await ProcessImages( images, offer, userId );
        ProcessSportKinds( sportKinds, offer, userId );

        foreach (var path in pathsToDelete)
            try
            {
                _imageService.Delete( path );
            }
            catch
            {
                //ignore
            }
    }

    protected void DeleteAllImagesAndSportKinds( T offer )
    {
        _offersSportKindRepository.Remove( offer.SportKinds );
        var pathsToDelete = new List<string>();

        if (offer.Images.Any())
        {
            pathsToDelete = offer.Images.Select( x => x.Path ).ToList();
            _imageRepository.Remove( offer.Images );
        }

        foreach (var path in pathsToDelete)
            try
            {
                _imageService.Delete( path );
            }
            catch
            {
                //ignore
            }
    }

    private async Task<List<string>> ProcessImages( List<ImageFile> passedImage, T offer, int userId )
    {
        var imagesToRemove = offer.Images
            .Where( k => !passedImage.Exists( p => p.Path == k.Path ) )
            .ToList();

        var pathsToDelete = imagesToRemove.Select( x => x.Path ).ToList();

        if (imagesToRemove.Any()) _imageRepository.Remove( imagesToRemove );

        foreach (var image in passedImage)
        {
            var entityImage = offer.Images.FirstOrDefault( x => x.Path == image.Path );
            if (entityImage == null)
            {
                var path = await _imageService.Upload(
                    image.FormFile,
                    offer.Stadium.LegalId,
                    offer.StadiumId,
                    $"offers/{ImageFolder}" );
                _imageRepository.Add(
                    CreateImage(
                        offer.Id,
                        userId,
                        path,
                        passedImage.IndexOf( image ) ) );
            }
            else
            {
                var newOrder = passedImage.IndexOf( image );
                if (newOrder == entityImage.Order) continue;

                entityImage.Order = newOrder;
                entityImage.UserModifiedId = userId;
                _imageRepository.Update( entityImage );
            }
        }

        return pathsToDelete;
    }

    private void ProcessSportKinds( List<SportKind> passedSportKinds, T offer, int userId )
    {
        var kindsToRemove = offer.SportKinds
            .Where( k => !passedSportKinds.Exists( p => p == k.SportKind ) )
            .ToList();

        if (kindsToRemove.Any()) _offersSportKindRepository.Remove( kindsToRemove );

        var offerSportsKind = offer.SportKinds.ToList();
        var kindsToAdd = passedSportKinds
            .Where( k => !offerSportsKind.Exists( x => x.SportKind == k ) )
            .ToList();

        if (kindsToAdd.Any())
            _offersSportKindRepository.Add( kindsToAdd.Select( k => CreateSportKind( offer.Id, userId, k ) ) );
    }

    protected abstract void AddOffer( T offer );
    protected abstract void UpdateOffer( T offer );
    protected abstract OffersSportKind CreateSportKind( int offerId, int userId, SportKind sportKind );
    protected abstract OffersImage CreateImage( int offerId, int userId, string path, int order );
}