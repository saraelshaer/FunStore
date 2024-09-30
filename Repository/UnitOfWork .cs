﻿using FurnitureStore.Data;
using FurnitureStore.IRepository;
using FurnitureStore.Models;

namespace FurnitureStore.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AppDbContext _context;
        //private IRepository<Product> _productRepository;

        public UnitOfWork(AppDbContext context)
        {
            _context = context;
            ProductRepository = new Repository<Product>(_context);
            UserRepository = new Repository<User>(_context);
            UserRoleRepo= new Repository<UserRole>(_context);
            CategoryRepository = new Repository<Category>(_context);
            ReviewRepository = new Repository<Review>(_context);
            RoleRepository= new Repository<Role>(_context);
        }

        public IRepository<Product> ProductRepository { get; }

        public IRepository<User> UserRepository { get; }

        public IRepository<Category> CategoryRepository { get; }

        public IRepository<Review> ReviewRepository { get; }


        public IRepository<UserRole> UserRoleRepo { get; }
        public IRepository<Role> RoleRepository { get; }

       

        public void Save()
        {
            _context.SaveChanges();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
    