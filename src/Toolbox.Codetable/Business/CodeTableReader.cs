using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using Toolbox.Codetable.Entities;
using Toolbox.DataAccess;
using Toolbox.DataAccess.Entities;
using Toolbox.DataAccess.Uow;
using Toolbox.DataAccess.Repositories;
using Toolbox.DataAccess.Query;

namespace Toolbox.Codetable.Business
{
    public class CodetableReader<T> : ICodetableReader<T> where T : CodetableEntityBase
    {
        public CodetableReader(ILogger<T> logger, IUowProvider uowProvider)
        {
            _logger = logger;
            _uowProvider = uowProvider;
        }

        protected readonly ILogger _logger;
        protected readonly IUowProvider _uowProvider;

        protected OrderBy<T> Ordering => new OrderBy<T>(query => query.OrderBy(a => a.Sortindex));

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            using (var uow = _uowProvider.CreateUnitOfWork(false))
            {
                var repository = uow.GetRepository<T>();
                return await repository.GetAllAsync(orderBy: Ordering.Expression);
            }
        }

        public IEnumerable<T> GetAll()
        {
            using (var uow = _uowProvider.CreateUnitOfWork(false))
            {
                var repository = uow.GetRepository<T>();
                return repository.GetAll(orderBy: Ordering.Expression);
            }
        }

        public T Get(int id)
        {
            using (var uow = _uowProvider.CreateUnitOfWork(false))
            {
                var repository = uow.GetRepository<T>();
                return repository.Get(id);
            }
        }

        public async Task<T> GetAsync(int id)
        {
            using (var uow = _uowProvider.CreateUnitOfWork(false))
            {
                var repository = uow.GetRepository<T>();
                return await repository.GetAsync(id);
            }
        }

        public T Get(string code)
        {
            using (var uow = _uowProvider.CreateUnitOfWork(false))
            {
                var repository = uow.GetRepository<T>();
                return repository.Query(x => x.Code.ToLower() == code.ToLower()).FirstOrDefault();
            }
        }

        public async Task<T> GetAsync(string code)
        {
            using (var uow = _uowProvider.CreateUnitOfWork(false))
            {
                var repository = uow.GetRepository<T>();
                var qryResults = await repository.QueryAsync(x => x.Code.ToLower() == code.ToLower());
                return qryResults.FirstOrDefault();
            }
        }
    }
}