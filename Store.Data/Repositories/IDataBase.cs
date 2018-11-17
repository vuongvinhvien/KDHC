using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Store.Data.Repositories;

namespace Store.Data.Repositories
{

    public interface IRepositoryBase<TEntity>  where TEntity : class
    {
        void Add(TEntity obj);
        TEntity GetById(string id);
        IEnumerable<TEntity> GetAll();
        void Update(TEntity obj);
        void Remove(TEntity obj);
        TEntity GetByTypeInt(int id);
    }
    public class RepositoryBase<TEntity> : IRepositoryBase<TEntity> where TEntity : class
    {
        private readonly IUnitOfWork Db;
        public RepositoryBase(IUnitOfWork _Db)
        {
            Db = _Db;
        } 

        public void Add(TEntity obj)
        {
            
            Db.Set<TEntity>().Add(obj);
            Db.Commit();
        }
        public TEntity GetByTypeInt(int id)
        {
            return Db.Set<TEntity>().Find(id);
        }

        public TEntity GetById(string id)
        {
            return Db.Set<TEntity>().Find(id);
        }

        public IEnumerable<TEntity> GetAll()
        {
            return Db.Set<TEntity>().ToList();
        }

        public void Update(TEntity obj)
        {
            Db.Entry<TEntity>(obj);
            Db.Commit();
        }

        public void Remove(TEntity obj)
        {
            Db.Set<TEntity>().Remove(obj);
 
            Db.Commit();
        }


    }


}
