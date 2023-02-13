using AutoMapper;
using StadiumEngine.Common;
using StadiumEngine.Common.Enums.Offers;
using StadiumEngine.Common.Exceptions;
using StadiumEngine.Domain.Entities.Offers;
using StadiumEngine.Domain.Repositories.Offers;
using StadiumEngine.Domain.Services;
using StadiumEngine.Domain.Services.Identity;
using StadiumEngine.Domain.Services.Infrastructure;
using StadiumEngine.DTO.Offers.Fields;
using StadiumEngine.Handlers.Commands.Offers;
using StadiumEngine.Handlers.Commands.Offers.Fields;

namespace StadiumEngine.Handlers.Handlers.Offers.Fields;

internal sealed class UpdateFieldHandler : BaseRequestHandler<UpdateFieldCommand, UpdateFieldDto>
{
    private readonly IFieldRepository _fieldRepository;
    private readonly IOffersImageRepository _imageRepository;
    private readonly IFieldSportKindRepository _fieldSportKindRepository;
    private readonly IImageService _imageService;

    public UpdateFieldHandler(IMapper mapper, IClaimsIdentityService claimsIdentityService, IUnitOfWork unitOfWork, 
        IFieldRepository fieldRepository,
        IOffersImageRepository imageRepository,
        IFieldSportKindRepository fieldSportKindRepository,
        IImageService imageService) : base(mapper, claimsIdentityService, unitOfWork)
    {
        _fieldRepository = fieldRepository;
        _imageRepository = imageRepository;
        _fieldSportKindRepository = fieldSportKindRepository;
        _imageService = imageService;
    }

    public override async ValueTask<UpdateFieldDto> Handle(UpdateFieldCommand request, CancellationToken cancellationToken)
    {
        var field = await _fieldRepository.Get(request.Id, _currentStadiumId);

        if (field == null) throw new DomainException(ErrorsKeys.FieldNotFound);

        try
        {
            await UnitOfWork.BeginTransaction();
            
            field.Name = request.Name;
            field.Description = request.Description;
            field.Width = request.Width;
            field.Length = request.Length;
            field.ParentFieldId = request.ParentFieldId;
            field.CoveringType = request.CoveringType;
            field.IsActive = request.IsActive;
            field.UserModifiedId = _userId;
        
            _fieldRepository.Update(field);
            
            var pathsToDelete = await ProcessImages(request.Images, field);
            ProcessSportKinds(request.SportKinds, field);

            await UnitOfWork.CommitTransaction();
            
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
            
            return new UpdateFieldDto();

        }
        catch
        {
            await UnitOfWork.RollbackTransaction();
            throw;
        }
    }

    private async Task<List<string>> ProcessImages(List<OffersImagePassed> passedImage, Field field)
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
                var path = await _imageService.Upload(image.FormFile, _legalId, _currentStadiumId, "offers/fields");
                _imageRepository.Add(new OffersImage
                {
                    FieldId = field.Id,
                    Path = path,
                    Order =  passedImage.IndexOf(image),
                    UserCreatedId = _userId
                });
            }
            else
            {
                var newOrder = passedImage.IndexOf(image);
                if (newOrder == entityImage.Order) continue;
                
                entityImage.Order = newOrder;
                entityImage.UserModifiedId = _userId;
                _imageRepository.Update(entityImage);
            }
        }

        return pathsToDelete;
    }
    
    private void ProcessSportKinds(List<SportKind> passedSportKinds, Field field)
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
                UserCreatedId = _userId,
                FieldId = field.Id,
                SportKind = k
            }));
        }
    }
}