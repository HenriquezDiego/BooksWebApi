using System.Collections.Generic;

namespace BooksWebApi.DataAccess.Core
{
    public interface IRepository<TEntity> where TEntity : class
    {
        TEntity Get(int id);
        IEnumerable<TEntity> GetAll();
        bool Insert(TEntity entity);
        bool Delete(int id);
        bool Update(int id, TEntity entity);
    }
}
