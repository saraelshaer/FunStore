﻿using FurnitureStore.Data;
using FurnitureStore.IRepository;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace FurnitureStore.Repository
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly AppDbContext _context;
        private readonly DbSet<T> _dbSet;

        public Repository(AppDbContext context)
        {
            _context = context;
            _dbSet = _context.Set<T>();
        }
        public void Add(T entity) => _dbSet.Add(entity);

        public IQueryable<T> GetAll() { return _dbSet.AsQueryable(); }

        public T GetById(int id) { return _dbSet.Find(id); }

        public void Update(T entity) => _dbSet.Update(entity);
        public void HardDelete(T entity) => _dbSet.Remove(entity);

        public void SoftDelete(T entity)
        {
            var isActiveProp = entity.GetType().GetProperty("IsActive");
            isActiveProp.SetValue(entity, false);
            _dbSet.Update(entity);
        }

        public T Find(Expression<Func<T, bool>> criteria, string[] includes = null)
        {
            IQueryable<T> query = _dbSet;
            if(includes != null)
            {
                foreach(var include in includes)
                {
                    query=query.Include(include);
                }
            }
            return query.SingleOrDefault(criteria);
        }
    }
}
