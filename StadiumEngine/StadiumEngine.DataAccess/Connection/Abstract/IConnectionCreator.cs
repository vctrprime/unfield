using System;
using System.Data;
using System.Threading.Tasks;

namespace StadiumEngine.DataAccess.Connection.Abstract
{
    public interface IConnectionCreator : IDisposable
    {
        IDbConnection Connection { get; }
    }
}