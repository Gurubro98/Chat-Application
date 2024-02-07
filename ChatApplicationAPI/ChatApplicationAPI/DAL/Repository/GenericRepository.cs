using DAL.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repository
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        protected ApplicationDbContext _context = null;
        protected DbSet<T> table = null;
        public GenericRepository(ApplicationDbContext context)
        {
            this._context = context;
            table = context.Set<T>();
        }

        public IQueryable<T> GetAll()
        {
            return table;
        }


        public T GetById(object id)
        {
            return table.Find(id);
        }
        public void Create(T obj)
        {
            table.Add(obj);
        }
        public void Update(object id, T obj)
        {
            var exist = table.Find(id);
            if (exist != null)
            {
                _context.Entry(exist).State = EntityState.Detached;
                table.Attach(obj);
                _context.Entry(obj).State = EntityState.Modified;
            }

        }
        public void Delete(object id)
        {
            T existing = table.Find(id);
            table.Remove(existing);
        }
        public void DeleteObject(T obj)
        {
            table.Remove(obj);
        }

        public void Save()
        {
            try
            {
                _context.SaveChanges();
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message.ToString());
            }
        }
    }
}
