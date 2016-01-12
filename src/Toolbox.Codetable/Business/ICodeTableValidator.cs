using Toolbox.Codetable.Entities;

namespace Toolbox.Codetable.Business
{
    public interface ICodeTabelValidator<T> where T : CodetableEntityBase
    {
        void CanUserInsert(T entity);
        void CanUserUpdate(T entity);
        void CanUserDelete(T entity);
    }
}
