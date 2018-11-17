using Store.Data.DataDbContext;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.Data.Repositories
{
    public interface IUnitOfWork : IDisposable
    {

        DbSet<T> Set<T>() where T : class;
        void Commit();
        void Entry<T>(T obj) where T : class;
        IEnumerable<T> Runstore<T>(StoreProduceModel Store);
    }

    public class UnitOfWork : IUnitOfWork
    {
        private readonly DataChatBox _context;
        private bool _isDisposed;

        public UnitOfWork(DataChatBox context)
        {
            _context = context;
        }

        public DbSet<T> Set<T>() where T : class
        {
            return _context.Set<T>();
        }

        public void Commit()
        {
            _context.SaveChanges();

        }

        public void Dispose()
        {
            if (_isDisposed)
                return;

            _isDisposed = true;
            _context.Dispose();
        }


        public void Entry<T>(T obj) where T : class
        {
            if (_context.Entry(obj).State == System.Data.Entity.EntityState.Detached)
                _context.Set<T>().Attach(obj);
            _context.Entry(obj).State = System.Data.Entity.EntityState.Modified;
        }
        public IEnumerable<T> Runstore<T>(StoreProduceModel Store)
        {
          
            var text = Store.NameProduce + " "; 

            foreach (var item in Store.Params)
            {
                text = text + ", " + item.ParameterName;
            }
            text =text.Remove(text.IndexOf(','), 1) ;
            IEnumerable<T> result = _context.Database.SqlQuery<T>(text, Store.Params.ToArray())
       .ToList();
            return result;
        }
    }
}
