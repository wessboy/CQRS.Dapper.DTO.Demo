using DapperDemo.Models;

namespace DapperDemo.Repository
{
    public interface IGenericRepository<T> where T : class
    {
        T Find(int id);
        List<T> GetAll();
        Company Add(T entity);
        Company Update(T entity);
        void Remove(int id);
    }
}
