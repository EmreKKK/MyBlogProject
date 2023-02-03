using MyBlogProject.Common;
using MyBlogProject.DAL;
using MyBlogProject.DAL.Abstract;
using MyBlogProject.Entity;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Core.Objects;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace MyBlogProject.DAL.EF
{
    public class Repository<T> : RepositoryBase, IRepository<T> where T : class
    {
        //private DatabaseContext db = new DatabaseContext();
        //private DatabaseContext db;//Singleton
        private DbSet<T> _objectSet;

        public Repository()
        {
            //db = RepositoryBase.CreateContext();
            _objectSet = _db.Set<T>();
        }

        public virtual IQueryable<T> ListQueryable()
        {
            return _objectSet.AsQueryable();
        }
        public List<T> List()
        {
            return _objectSet.ToList();
        }
        public List<T> List(Expression<Func<T, bool>> where)
        {
            return _objectSet.Where(where).ToList();
        }

        public T Find(Expression<Func<T, bool>> where)
        {
            return _objectSet.FirstOrDefault(where);
        }

        public int Insert(T obj)
        {
            _objectSet.Add(obj);

            if (obj is BaseEntity)
            {
                BaseEntity o = obj as BaseEntity;
                o.CreateOn = DateTime.Now;
                o.ModifiedOn = DateTime.Now;
                o.ModifiedUsername = App.Common.GetUsername();
            }
            return _db.SaveChanges();
        }
        public int Update(T obj)
        {
            if (obj is BaseEntity)
            {
                BaseEntity o = obj as BaseEntity;
                o.ModifiedOn = DateTime.Now;
                o.ModifiedUsername = App.Common.GetUsername();
            }

            return _db.SaveChanges();
        }
        public int Delete(T obj)

        {
            _objectSet.Remove(obj);
            return _db.SaveChanges();
        }


    }
}
