using Toolbox.Codetable.Entities;
using System;

namespace Toolbox.Codetable.Business
{
    public class CodeTabelValidator<T> : ICodeTabelValidator<T> where T :  CodetabelEntityBase
    {
        public void KanGebruikerDeleten(T entity)
        {
            throw new NotImplementedException();
        }

        public void KanGebruikerInserten(T entity)
        {
            throw new NotImplementedException();
        }

        public void KanGebruikerUpdaten(T entity)
        {
            throw new NotImplementedException();
        }
    }
}
