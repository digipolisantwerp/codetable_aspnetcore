﻿using Toolbox.Codetable.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Toolbox.Codetable.Business
{
    public interface ICodetabelReader<T>  where T : CodetabelEntityBase
    {
        Task<IEnumerable<T>> GetAllAsync();
        IEnumerable<T> GetAll();
        T Get(int id);
        Task<T> GetAsync(int id);
        T Get(string code);
        Task<T> GetAsync(string code);
    }
}