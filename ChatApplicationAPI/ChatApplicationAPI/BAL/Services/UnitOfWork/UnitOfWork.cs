using DAL.Data;
using DAL.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BAL.Services.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        public readonly ApplicationDbContext _context;
        public UnitOfWork(ApplicationDbContext context) 
        {
            _context = context; 
        }
        private bool disposed;
        protected virtual void Dispose(bool disposing)
        {
            if (this.disposed)
            {
                return;
            }

            if (disposing)
            {
                _context.Dispose();
            }

            this.disposed = true;
        }
            
        public void Dispose()
        {
            this.Dispose(true);
        }

        public IGenericRepository<TEntity> Repository<TEntity>() where TEntity : class
        {
            return new GenericRepository<TEntity>(this._context);
        }

        public void Save()
        {
            _context.SaveChanges();
        }
    }
}
