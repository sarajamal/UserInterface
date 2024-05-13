using System.Linq.Expressions;

namespace Test12.DataAccess.Repository.IRepository
{
    public interface IRepository<T> where T : class
    {
        //T - Category 
        IEnumerable<T> GetAll(string? incloudeProperties = null); // call all Entity. 
        T Get(Expression<Func<T, bool>> filter, string? incloudeProperties = null); // this for retrieve one Entity in Edit by any condition not only Id . 
        void Add(T entity);
        void Remove(T entity);
        void RemoveRange(IEnumerable<T> entity);//this for delete many Entity In single Colom May be I need . 
    }
}

