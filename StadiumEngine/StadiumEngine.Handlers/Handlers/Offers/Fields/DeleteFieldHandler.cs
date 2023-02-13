using AutoMapper;
using StadiumEngine.Common;
using StadiumEngine.Common.Exceptions;
using StadiumEngine.Domain.Repositories.Offers;
using StadiumEngine.Domain.Services;
using StadiumEngine.Domain.Services.Identity;
using StadiumEngine.Domain.Services.Infrastructure;
using StadiumEngine.DTO.Offers.Fields;
using StadiumEngine.Handlers.Commands.Offers.Fields;

namespace StadiumEngine.Handlers.Handlers.Offers.Fields;

internal sealed class DeleteFieldHandler : BaseRequestHandler<DeleteFieldCommand, DeleteFieldDto>
{
    private readonly IFieldRepository _fieldRepository;
    private readonly IOffersImageRepository _imageRepository;
    private readonly IFieldSportKindRepository _fieldSportKindRepository;
    private readonly IImageService _imageService;

    public DeleteFieldHandler(IMapper mapper, IClaimsIdentityService claimsIdentityService, IUnitOfWork unitOfWork, 
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

    public override async ValueTask<DeleteFieldDto> Handle(DeleteFieldCommand request, CancellationToken cancellationToken)
    {
        var field = await _fieldRepository.Get(request.FieldId, _currentStadiumId);

        if (field == null) throw new DomainException(ErrorsKeys.FieldNotFound);

        List<string> pathsToDelete = new List<string>();
        
        try
        {
            await UnitOfWork.BeginTransaction();
            _fieldRepository.Remove(field);
            
            _fieldSportKindRepository.Remove(field.FieldSportKinds);

            if (field.Images.Any())
            {
                pathsToDelete = field.Images.Select(x => x.Path).ToList();
                _imageRepository.Remove(field.Images);
            }
            
            await UnitOfWork.CommitTransaction();
        }
        catch
        {
            await UnitOfWork.RollbackTransaction();
            throw;
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
        
        return new DeleteFieldDto();
    }
}