using StadiumEngine.Common.Enums.Offers;
using StadiumEngine.Common.Models;
using StadiumEngine.Domain;
using StadiumEngine.Domain.Entities.Offers;
using StadiumEngine.Domain.Repositories.Offers;
using StadiumEngine.Domain.Services.Infrastructure;

namespace StadiumEngine.Services.Core.Offers;

internal abstract class BaseOfferCommandService<T> where T : BaseOfferEntity
{
    private readonly IOffersImageRepository _imageRepository;
    private readonly IImageService _imageService;
    private readonly IOffersSportKindRepository _offersSportKindRepository;

    protected BaseOfferCommandService(
        IOffersImageRepository imageRepository,
        IImageService imageService,
        IOffersSportKindRepository offersSportKindRepository )
    {
        _imageRepository = imageRepository;
        _imageService = imageService;
        _offersSportKindRepository = offersSportKindRepository;
    }

    protected abstract string ImageFolder { get; }

    protected async Task AddOfferAsync( T offer, List<ImageFile> images, int stadiumGroupId )
    {
        foreach ( OffersSportKind? offersSportKind in offer.SportKinds )
        {
            offersSportKind.UserCreatedId = offer.UserCreatedId;
        }

        offer.Images = new List<OffersImage>();

        foreach ( ImageFile image in images )
        {
            if ( image.FormFile is null )
            {
                continue;
            }

            string path = await _imageService.Upload(
                image.FormFile,
                stadiumGroupId,
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

        await AddOfferAsync( offer );
    }

    protected async Task UpdateOfferAsync( T offer, List<ImageFile> images, List<SportKind> sportKinds )
    {
        int userId = offer.UserModifiedId ?? 0;

        await UpdateOfferAsync( offer );

        List<string> pathsToDelete = await ProcessImagesAsync( images, offer, userId );
        ProcessSportKinds( sportKinds, offer, userId );

        foreach ( string path in pathsToDelete )
        {
            try
            {
                _imageService.Delete( path );
            }
            catch
            {
                //ignore
            }
        }
    }

    protected void DeleteAllImagesAndSportKinds( T offer )
    {
        _offersSportKindRepository.Remove( offer.SportKinds );
        List<string> pathsToDelete = new();

        if ( offer.Images.Any() )
        {
            pathsToDelete = offer.Images.Select( x => x.Path ).ToList();
            _imageRepository.Remove( offer.Images );
        }

        foreach ( string path in pathsToDelete )
        {
            try
            {
                _imageService.Delete( path );
            }
            catch
            {
                //ignore
            }
        }
    }

    private async Task<List<string>> ProcessImagesAsync( List<ImageFile> passedImage, T offer, int userId )
    {
        List<OffersImage> imagesToRemove = offer.Images
            .Where( k => !passedImage.Exists( p => p.Path == k.Path ) )
            .ToList();

        List<string> pathsToDelete = imagesToRemove.Select( x => x.Path ).ToList();

        if ( imagesToRemove.Any() )
        {
            _imageRepository.Remove( imagesToRemove );
        }

        foreach ( ImageFile image in passedImage )
        {
            OffersImage? entityImage = offer.Images.FirstOrDefault( x => x.Path == image.Path );
            if ( entityImage == null && image.FormFile is not null )
            {
                string path = await _imageService.Upload(
                    image.FormFile,
                    offer.Stadium.StadiumGroupId,
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
                int newOrder = passedImage.IndexOf( image );
                if ( newOrder == entityImage!.Order )
                {
                    continue;
                }

                entityImage.Order = newOrder;
                entityImage.UserModifiedId = userId;
                _imageRepository.Update( entityImage );
            }
        }

        return pathsToDelete;
    }

    private void ProcessSportKinds( List<SportKind> passedSportKinds, T offer, int userId )
    {
        List<OffersSportKind> kindsToRemove = offer.SportKinds
            .Where( k => !passedSportKinds.Exists( p => p == k.SportKind ) )
            .ToList();

        if ( kindsToRemove.Any() )
        {
            _offersSportKindRepository.Remove( kindsToRemove );
        }

        List<OffersSportKind> offerSportsKind = offer.SportKinds.ToList();
        List<SportKind> kindsToAdd = passedSportKinds
            .Where( k => !offerSportsKind.Exists( x => x.SportKind == k ) )
            .ToList();

        if ( kindsToAdd.Any() )
        {
            _offersSportKindRepository.Add( kindsToAdd.Select( k => CreateSportKind( offer.Id, userId, k ) ) );
        }
    }

    protected abstract Task AddOfferAsync( T offer );
    protected abstract Task UpdateOfferAsync( T offer );
    protected abstract OffersSportKind CreateSportKind( int offerId, int userId, SportKind sportKind );
    protected abstract OffersImage CreateImage( int offerId, int userId, string path, int order );
}