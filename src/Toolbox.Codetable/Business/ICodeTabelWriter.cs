using Toolbox.Codetable.Entities;
using System.Threading.Tasks;

namespace Toolbox.Codetable.Business
{
    public interface ICodetabelWriter<T> where T : CodetabelEntityBase
    {
        Task<T> InsertAsync(T entity);
        Task UpdateAsync(T entity);
        Task DeleteAsync(int id);
    }
}
