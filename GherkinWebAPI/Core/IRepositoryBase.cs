using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace GherkinWebAPI.Core
{
    public interface IRepositoryBase<T> : IDisposable where T : class
    {
        IQueryable<T> FindAll();
        IQueryable<T> FindByCondition(Expression<Func<T, bool>> expression);
        T Create(T entity);
        void Update(T entity);
        void Delete(T entity);

        IEnumerable<T> ExecuteQuery(string spQuery, object[] parameters);
        T ExecuteQuerySingle(string spQuery, object[] parameters);
        int ExecuteCommand(string spQuery, object[] parameters);
    }
}
