using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Test12.DataAccess.Data;
using Test12.DataAccess.Repository.IRepository;


namespace Test12.DataAccess.Repository
{
    public class Repository<T> : IRepository<T> where T : class
    {

        private readonly ApplicationDbContext _context;
        internal DbSet<T> dbSet; //because is generic class I cant do (_context.PreparationList.Add) I need something public.
        public Repository(ApplicationDbContext context)
        {
            _context = context;
            this.dbSet = _context.Set<T>();
            _context.Preparations.Include(u => u.component.Count);
            _context.PreparationIngredients.Include(u => u.Preparation);
            _context.PreparationTools.Include(u => u.Preparation);
            _context.PreparationSteps.Include(u => u.Preparation);
            _context.Production.Include(u => u.component2.Count);
            _context.ProductionIngredients.Include(u => u.Production);
            _context.ProductionTools.Include(u => u.Production);
            _context.ProductionSteps.Include(u => u.Production);
            _context.LoginModels.Include(u => u.Login_ID);
            _context.Cleaning.Include(u => u.Brand);
            _context.الخطوات3.Include(u => u.التنظيف);
            _context.DevicesAndTools.Include(u => u.Brand);
            _context.FoodStuffs.Include(u => u.Brand);
            _context.ReadyProducts.Include(u => u.Brand);

        }


        public void Add(T entity)
        {
            dbSet.Add(entity);
        }

        public T Get(Expression<Func<T, bool>> filter, string? incloudeProperties = null) //Individual Get . 
        {
            IQueryable<T> query = dbSet;
            query = query.Where(filter);
            if (!string.IsNullOrEmpty(incloudeProperties))
            {
                foreach (var includeProp in incloudeProperties
                    .Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(includeProp);
                }
            }
            return query.FirstOrDefault(); // this is for implement  Category? categoryFormDb2 = _db.Categories.Where(u => u.Id == id).FirstOrDefault();.
        }

        public IEnumerable<T> GetAll(string? incloudeProperties = null)
        {

            IQueryable<T> query = dbSet;

            if (!string.IsNullOrEmpty(incloudeProperties))
            {
                foreach (var includeProp in incloudeProperties
                    .Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(includeProp);
                }
            }
            return query.AsEnumerable();
        }

        public void Remove(T entity)
        {
            dbSet.Remove(entity);
        }

        public void RemoveRange(IEnumerable<T> entity)
        {
            dbSet.RemoveRange(entity);
        }
        public void Delete(T entity)
        {
            dbSet.Remove(entity);
        }
    }
}


