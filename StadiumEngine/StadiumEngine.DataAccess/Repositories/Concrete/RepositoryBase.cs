using StadiumEngine.DataAccess.Connection.Abstract;

namespace StadiumEngine.DataAccess.Repositories.Concrete
{
    public class RepositoryBase
    {
        protected readonly IConnectionCreator ConnectionCreator;

        public RepositoryBase(IConnectionCreator connectionCreator)
        {
            ConnectionCreator = connectionCreator;
        }
    }
}