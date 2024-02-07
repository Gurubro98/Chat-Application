using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repository
{
    public interface IGenericRepository<T> where T : class
    {
        IQueryable<T> GetAll();
        T GetById(object id);
        void Create(T obj);
        void Update(object id, T obj);
        void Delete(object id);
        void DeleteObject(T obj);
        void Save();
    }
}
