using Digipolis.Codetable.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Digipolis.Codetable.Business
{
    public interface ICodetableReader<T>  where T : CodetableEntityBase
    {
        Task<IEnumerable<T>> GetAllAsync();
        IEnumerable<T> GetAll();
        T Get(int id);
        Task<T> GetAsync(int id);
        T Get(string code);
        Task<T> GetAsync(string code);
    }
}