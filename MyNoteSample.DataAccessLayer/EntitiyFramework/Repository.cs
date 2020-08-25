using MyNoteSample.Common;
using MyNoteSample.Core.DataAccess;
using MyNoteSample.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;

namespace MyNoteSample.DataAccessLayer.EntityFramework
{
    public class Repository<T> : RepositoryBase, IDataAccess<T> where T : class
    {

        private DbSet<T> _objectSet;
        public Repository()
        {

            _objectSet = db.Set<T>();
        }

        public List<T> List()
        {
            return _objectSet.ToList();
        }
        public IQueryable<T> ListQueryable()
        {
            return _objectSet.AsQueryable<T>();
        }
        public List<T> List(Expression<Func<T, bool>> expression)
        {
            return _objectSet.Where(expression).ToList();
        }
        public int Save()
        {
            return db.SaveChanges();
        }
        public int Update(T obj)
        {
            if (obj is EntityBase)
            {
                EntityBase eb = obj as EntityBase;
                DateTime now = DateTime.Now;

                eb.ModifiedOn = now;
                eb.ModifiedUsername = App.Common.GetUsername();//TODO:işlem yapan kullanıcı yazmalı
            }
            return Save();
        }
        public int Insert(T objec)
        {
            _objectSet.Add(objec);
            if (objec is EntityBase)
            {
                EntityBase eb = objec as EntityBase;
                DateTime now = DateTime.Now;

                eb.CreatedOn = now;
                eb.ModifiedOn = now;
                eb.ModifiedUsername = App.Common.GetUsername();//TODO:işlem yapan kullanıcı yazmalı
            }
            return Save();
        }
        public int Delete(T obj)
        {
            _objectSet.Remove(obj);

            return Save();
        }
        public T Find(Expression<Func<T, bool>> expression)
        {
            return _objectSet.FirstOrDefault(expression);
        }

    }
}
