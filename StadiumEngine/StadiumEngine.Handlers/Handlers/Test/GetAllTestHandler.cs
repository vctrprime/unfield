using AutoMapper;
using Mediator;
using StadiumEngine.DTO.Test;
using StadiumEngine.Handlers.Queries.Test;
using StadiumEngine.Repositories.Abstract;

namespace StadiumEngine.Handlers.Handlers.Test;

internal sealed class GetAllTestHandler : IRequestHandler<GetAllTestQuery, List<TestDto>>
{
    private readonly ITestRepository _repository;
    private readonly IMapper _mapper;

    public GetAllTestHandler(ITestRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async ValueTask<List<TestDto>> Handle(GetAllTestQuery request, CancellationToken cancellationToken)
    {
        var cities = await _repository.GetAll();

        var citiesDto = _mapper.Map<List<TestDto>>(cities);

        return citiesDto;
    }
}