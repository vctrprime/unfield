using AutoMapper;
using Mediator;
using StadiumEngine.DTO.Test;
using StadiumEngine.Entities.Domain.Geo;
using StadiumEngine.Handlers.Commands.Test;
using StadiumEngine.Repositories.Abstract;

namespace StadiumEngine.Handlers.Handlers.Test;

internal sealed class CreateTestHandler : IRequestHandler<CreateTestCommand, TestDto>
{
    private readonly ITestRepository _repository;
    private readonly IMapper _mapper;

    public CreateTestHandler(ITestRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }
    
    public async ValueTask<TestDto> Handle(CreateTestCommand request, CancellationToken cancellationToken)
    {
        var data = _mapper.Map<City>(request);
        var city = await _repository.Create(data);

        var dto = _mapper.Map<TestDto>(city);

        return dto;
    }
}