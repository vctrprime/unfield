using AutoMapper;
using StadiumEngine.Domain.Entities.Offers;
using StadiumEngine.Domain.Repositories.Offers;
using StadiumEngine.Domain.Services;
using StadiumEngine.Domain.Services.Identity;
using StadiumEngine.Domain.Services.Infrastructure;
using StadiumEngine.DTO.Offers.Fields;
using StadiumEngine.Handlers.Commands.Offers.Fields;

namespace StadiumEngine.Handlers.Handlers.Offers.Fields;

internal sealed class AddFieldHandler : BaseRequestHandler<AddFieldCommand, AddFieldDto>
{
    private readonly IFieldRepository _repository;
    private readonly IImageService _imageService;

    public AddFieldHandler(IMapper mapper, IClaimsIdentityService claimsIdentityService, IUnitOfWork unitOfWork, 
        IFieldRepository repository,
        IImageService imageService) : base(mapper, claimsIdentityService, unitOfWork)
    {
        _repository = repository;
        _imageService = imageService;
    }
    
    public override async ValueTask<AddFieldDto> Handle(AddFieldCommand request, CancellationToken cancellationToken)
    {
        try
        {
            await UnitOfWork.BeginTransaction();
            
            var field = Mapper.Map<Field>(request);
            field.StadiumId = _currentStadiumId;
            field.UserCreatedId = _userId;
            foreach (var fieldFieldSportKind in field.FieldSportKinds)
            {
                fieldFieldSportKind.UserCreatedId = _userId;
            }

            field.Images = new List<OffersImage>();
            
            foreach (var image in request.Images)
            {
                var path = await _imageService.Upload(image.FormFile, _legalId, _currentStadiumId, "offers/fields");
                field.Images.Add(new OffersImage
                {
                    Path = path,
                    Order = request.Images.IndexOf(image),
                    UserCreatedId = _userId
                });
            }
        
            _repository.Add(field);
            await UnitOfWork.CommitTransaction();

            return new AddFieldDto();

        }
        catch
        {
            await UnitOfWork.RollbackTransaction();
            throw;
        }
    }
}