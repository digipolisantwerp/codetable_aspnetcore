using Toolbox.Codetable.Entities;
using Toolbox.DataAccess;
using Toolbox.DataAccess.Exceptions;
using Toolbox.DataAccess.Repositories;
using Toolbox.DataAccess.Uow;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace Toolbox.Codetable.Business
{
    public class CodeTabelWriter<T> : ICodetabelWriter<T> where T : CodetabelEntityBase
    {
        public CodeTabelWriter(ILogger logger, IUowProvider uowProvider)
        {
            _logger = logger;
            _uowProvider = uowProvider;
        }

        protected readonly ILogger _logger;
        protected readonly IUowProvider _uowProvider;

        public async Task<T> InsertAsync(T entity)
        {
            if (entity == null) throw new ArgumentException("No codetabel provided", nameof(entity));



            using (var uow = _uowProvider.CreateUnitOfWork(false))
            {
                IRepository<T> repo = uow.GetRepository<IRepository<T>>();
                repo.Add(entity);
                await uow.SaveChangesAsync();
                return entity;
            }
        }

        public async Task UpdateAsync(T entity)
        {
            if (entity == null) throw new ArgumentException("No codetabel provided", nameof(entity));

            using (var uow = _uowProvider.CreateUnitOfWork(false))
            {
                IRepository<T> repo = uow.GetRepository<IRepository<T>>();
                repo.Update(entity);
                await uow.SaveChangesAsync();
            }
        }

        public async Task DeleteAsync(int id)
        {
            using (var uow = _uowProvider.CreateUnitOfWork(false))
            {
                IRepository<T> repo = uow.GetRepository<IRepository<T>>();
                var entity = await repo.GetAsync(id);
                if (entity == null) throw new EntityNotFoundException(typeof(T).Name, id);
                repo.Remove(entity);
                await uow.SaveChangesAsync();
            }
        }
    }
}
