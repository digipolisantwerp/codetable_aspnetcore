using Digipolis.Codetable.Entities;
using System.Threading.Tasks;

namespace Digipolis.Codetable.Business
{
    public interface ICodetableWriter<T> where T : CodetableEntityBase
    {
        Task<T> InsertAsync(T entity);
        Task UpdateAsync(T entity);
        Task DeleteAsync(int id);
    }
}
