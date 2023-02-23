using StadiumEngine.Common;
using StadiumEngine.Common.Enums.Offers;
using StadiumEngine.Common.Exceptions;
using StadiumEngine.Common.Models;
using StadiumEngine.Domain.Entities.Offers;
using StadiumEngine.Domain.Repositories.Offers;
using StadiumEngine.Domain.Services.Facades.Offers;
using StadiumEngine.Domain.Services.Infrastructure;

namespace StadiumEngine.Services.Facades.Offers;

internal class FieldFacade : IFieldFacade
{
    private readonly IFieldRepository _fieldRepository;
    private readonly IOffersImageRepository _imageRepository;
    private readonly IFieldSportKindRepository _fieldSportKindRepository;
    private readonly IImageService _imageService;

    public FieldFacade(
        IFieldRepository fieldRepository, 
        IOffersImageRepository imageRepository, 
        IFieldSportKindRepository fieldSportKindRepository, 
        IImageService imageService)
    {
        _fieldRepository = fieldRepository;
        _imageRepository = imageRepository;
        _fieldSportKindRepository = fieldSportKindRepository;
        _imageService = imageService;
    }

    public async Task<List<Field>> GetByStadiumId(int stadiumId)
    {
        return await _fieldRepository.GetAll(stadiumId);
    }

    public async Task<Field?> GetByFieldId(int fieldId, int stadiumId)
    {
        return await _fieldRepository.Get(fieldId, stadiumId);
    }

    public async Task AddField(
        Field field, 
        List<ImageFile> images,
        int legalId)
    {
        foreach (var fieldFieldSportKind in field.FieldSportKinds)
        {
            fieldFieldSportKind.UserCreatedId = field.UserCreatedId;
        }

        field.Images = new List<OffersImage>();
            
        foreach (var image in images)
        {
            var path = await _imageService.Upload(image.FormFile, legalId, field.StadiumId, "offers/fields");
            field.Images.Add(new OffersImage
            {
                Path = path,
                Order = images.IndexOf(image),
                UserCreatedId = field.UserCreatedId
            });
        }
        
        _fieldRepository.Add(field);
    }

    public async Task UpdateField(Field field, List<ImageFile> images, List<SportKind> sportKinds)
    {
        int userId = field.UserModifiedId ?? 0;
        
        _fieldRepository.Update(field);
        
        var pathsToDelete = await ProcessImages(images, field, userId);
        ProcessSportKinds(sportKinds, field, userId);
        
        foreach (var path in pathsToDelete)
        {
            try
            {
                _imageService.Delete(path);
            }
            catch
            {
                //ignore
            }
            
        }
    }

    public async Task DeleteField(int fieldId, int stadiumId)
    {
        var field = await _fieldRepository.Get(fieldId, stadiumId);

        if (field == null) throw new DomainException(ErrorsKeys.FieldNotFound);

        if (field.ChildFields.Any())
        {
            throw new DomainException(ErrorsKeys.FieldHasChildrenFields);
        }
        
        List<string> pathsToDelete = new List<string>();
        
        _fieldRepository.Remove(field);
            
        _fieldSportKindRepository.Remove(field.FieldSportKinds);

        if (field.Images.Any())
        {
            pathsToDelete = field.Images.Select(x => x.Path).ToList();
            _imageRepository.Remove(field.Images);
        }
        
        foreach (var path in pathsToDelete)
        {
            try
            {
                _imageService.Delete(path);
            }
            catch
            {
                //ignore
            }
        }
    }

    private async Task<List<string>> ProcessImages(List<ImageFile> passedImage, Field field, int userId)
    {
        var imagesToRemove = field.Images
            .Where(k => !passedImage.Exists(p => p.Path == k.Path))
            .ToList();

        var pathsToDelete = imagesToRemove.Select(x => x.Path).ToList();

        if (imagesToRemove.Any())
        {
            _imageRepository.Remove(imagesToRemove);
        }

        foreach (var image in passedImage)
        {
            var entityImage = field.Images.FirstOrDefault(x => x.Path == image.Path);
            if (entityImage == null)
            {
                var path = await _imageService.Upload(image.FormFile, field.Stadium.LegalId, field.StadiumId, "offers/fields");
                _imageRepository.Add(new OffersImage
                {
                    FieldId = field.Id,
                    Path = path,
                    Order =  passedImage.IndexOf(image),
                    UserCreatedId = userId
                });
            }
            else
            {
                var newOrder = passedImage.IndexOf(image);
                if (newOrder == entityImage.Order) continue;
                
                entityImage.Order = newOrder;
                entityImage.UserModifiedId = userId;
                _imageRepository.Update(entityImage);
            }
        }

        return pathsToDelete;
    }
    
    private void ProcessSportKinds(List<SportKind> passedSportKinds, Field field, int userId)
    {
        var kindsToRemove = field.FieldSportKinds
            .Where(k => !passedSportKinds.Exists(p => p == k.SportKind))
            .ToList();

        if (kindsToRemove.Any())
        {
            _fieldSportKindRepository.Remove(kindsToRemove);
        }

        var fieldSportsKind = field.FieldSportKinds.ToList();
        var kindsToAdd = passedSportKinds
            .Where(k => !fieldSportsKind.Exists(x => x.SportKind == k))
            .ToList();

        if (kindsToAdd.Any())
        {
            _fieldSportKindRepository.Add(kindsToAdd.Select(k => new FieldSportKind
            {
                UserCreatedId = userId,
                FieldId = field.Id,
                SportKind = k
            }));
        }
    }
}