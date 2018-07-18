using JooleWebAPI.Interfaces;
using JooleWebAPI.Repositories;
using System;

namespace JooleWebAPI.Unitofwork
{
    public class UnitOfWork : IUnitOfWork
    {
        private JooleContext _context;
        public UnitOfWork(JooleContext Context)
        {
            this._context = Context;
            Users = new UserRepository(_context);
            Projects = new ProjectRepository(_context);
            Categories = new CategoryRepository(_context);
            SubCategories = new SubCategoryRepository(_context);
            //ProductList = new ProductListRepository(_context);
            Products = new ProductRepository(_context);
            ProductProperties = new ProductPropertyRepository(_context);
            ProductPropertyValues = new ProductPropertyValueRepository(_context);
            ProdToProjRelations = new ProductToProjectRepository(_context);
            TechSpecFilter = new TechSpecFilterRepository(_context);
        }

        private bool disposed = false;

        public IUserRepository Users { get; private set; }
        public IProjectRepository Projects { get; private set; }
        public ICategoryRepository Categories { get; private set; }
        public ISubCategoryRepository SubCategories { get; private set; }
        public IProductRepository Products { get; private set; }
        public IProductPropertyRepository ProductProperties { get; private set; }
        public IProductPropertyValueRepository ProductPropertyValues { get; private set; }
        public IProductToProjectRepository ProdToProjRelations { get; set; }
        public ITechSpecFilterRepository TechSpecFilter { get; set; }

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    _context.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public int Complete()
        {
            return _context.SaveChanges();
        }
    }
}
