using Digipolis.Codetable.Entities;
using System;

namespace Digipolis.Codetable.Business
{
    public class CodeTabelValidator<T> : ICodeTabelValidator<T> where T :  CodetableEntityBase
    {
        public void CanUserDelete(T entity)
        {
            throw new NotImplementedException();
        }

        public void CanUserInsert(T entity)
        {
            throw new NotImplementedException();
        }

        public void CanUserUpdate(T entity)
        {
            throw new NotImplementedException();
        }
    }
}
