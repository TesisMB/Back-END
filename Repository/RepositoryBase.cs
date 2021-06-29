
using Back_End.Entities;
using Contracts.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Linq.Expressions;

namespace Repository
{
    public abstract class RepositoryBase<T> : IRepositoryBase<T> where T : class
    {

        protected CruzRojaContext CruzRojaContext2 { get; set; }

        public RepositoryBase(CruzRojaContext cruzRojaContext2)
        {
            CruzRojaContext2 = cruzRojaContext2;        
        }

        public IQueryable<T> FindAll()
        {
           return CruzRojaContext2.Set<T>().AsNoTracking();
        }

        public IQueryable<T> FindByCondition(Expression<Func<T, bool>> expression)
        {
            return CruzRojaContext2.Set<T>().Where(expression).AsNoTracking();
        }
        public void Create(T entity)
        {
            CruzRojaContext2.Set<T>().Add(entity);
        }
        public void Update(T entity)
        {
            CruzRojaContext2.Set<T>().Update(entity);
        }

        public void Delete(T entity)
        {
            CruzRojaContext2.Set<T>().Remove(entity);
        }
    }
}
