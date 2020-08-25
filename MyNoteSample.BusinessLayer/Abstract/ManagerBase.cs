using MyNoteSample.Core.DataAccess;
using MyNoteSample.DataAccessLayer.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;


namespace MyNoteSample.BusinessLayer.Abstract
{
    public abstract class ManagerBase<T> : IDataAccess<T> where T : class
    {
        private Repository<T> repo = new Repository<T>();
        public virtual int Delete(T obj)
        {
            return repo.Delete(obj);
        }

        public T Find(Expression<Func<T, bool>> expression)
        {
            return repo.Find(expression);
        }

        public virtual int Insert(T objec)
        {
            return repo.Insert(objec);
        }

        public List<T> List()
        {
            return repo.List();
        }

        public List<T> List(Expression<Func<T, bool>> expression)
        {
            return repo.List(expression);
        }

        public IQueryable<T> ListQueryable()
        {
            return repo.ListQueryable();
        }

        public int Save()
        {
            return repo.Save();
        }

        public virtual int Update(T obj)
        {
            return repo.Update(obj);
        }
    }
}
