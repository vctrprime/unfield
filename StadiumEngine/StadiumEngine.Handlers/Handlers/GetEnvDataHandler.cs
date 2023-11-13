using AutoMapper;
using StadiumEngine.Common.Configuration;
using StadiumEngine.DTO;
using StadiumEngine.Queries;

namespace StadiumEngine.Handlers.Handlers;

internal sealed class GetEnvDataHandler  : BaseRequestHandler<GetEnvDataQuery, EnvDataDto>
{
    private readonly EnvConfig _config;
    
    public GetEnvDataHandler( EnvConfig config, IMapper mapper ) : base( mapper )
    {
        _config = config;
    }

    public override async ValueTask<EnvDataDto> Handle(
        GetEnvDataQuery request,
        CancellationToken cancellationToken )
    {
        EnvDataDto result = Mapper.Map<EnvDataDto>( _config );
        return await Task.Run( () => result, cancellationToken );
    }
}