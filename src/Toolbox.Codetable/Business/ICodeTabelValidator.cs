using Toolbox.Codetable.Entities;

namespace Toolbox.Codetable.Business
{
    public interface ICodeTabelValidator<T> where T : CodetabelEntityBase
    {
        void KanGebruikerInserten(T entity);
        void KanGebruikerUpdaten(T entity);
        void KanGebruikerDeleten(T entity);
    }
}
