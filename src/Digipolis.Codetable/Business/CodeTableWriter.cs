﻿using Digipolis.Codetable.Entities;
using Digipolis.DataAccess;
using Digipolis.DataAccess.Exceptions;
using Digipolis.DataAccess.Repositories;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace Digipolis.Codetable.Business
{
    public class CodeTabelWriter<T> : ICodetableWriter<T> where T : CodetableEntityBase
    {
        public CodeTabelWriter(ILogger<T> logger, IUowProvider uowProvider)
        {
            _logger = logger;
            _uowProvider = uowProvider;
        }

        protected readonly ILogger _logger;
        protected readonly IUowProvider _uowProvider;

        public async Task<T> InsertAsync(T entity)
        {
            if (entity == null) throw new ArgumentException("No codetable provided", nameof(entity));

            using (var uow = _uowProvider.CreateUnitOfWork(false))
            {
                IRepository<T> repo = uow.GetRepository<T>();
                repo.Add(entity);
                await uow.SaveChangesAsync();
                return entity;
            }
        }

        public async Task UpdateAsync(T entity)
        {
            if (entity == null) throw new ArgumentException("No codetable provided", nameof(entity));

            using (var uow = _uowProvider.CreateUnitOfWork(false))
            {
                IRepository<T> repo = uow.GetRepository<T>();
                repo.Update(entity);
                await uow.SaveChangesAsync();
            }
        }

        public async Task DeleteAsync(int id)
        {
            using (var uow = _uowProvider.CreateUnitOfWork(false))
            {
                IRepository<T> repo = uow.GetRepository<T>();
                var entity = await repo.GetAsync(id);
                if (entity == null) throw new EntityNotFoundException(typeof(T).Name, id);
                repo.Remove(entity);
                await uow.SaveChangesAsync();
            }
        }
    }
}
