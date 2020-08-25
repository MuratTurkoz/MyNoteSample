using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;


namespace MyNoteSample.Core.DataAccess
{
    public interface IDataAccess<T>
    {
        List<T> List();
        IQueryable<T> ListQueryable();
        List<T> List(Expression<Func<T, bool>> expression);
        int Save();
        int Update(T obj);
        int Insert(T objec);
        int Delete(T obj);
        T Find(Expression<Func<T, bool>> expression);
    }
}
