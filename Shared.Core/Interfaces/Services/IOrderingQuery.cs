using Shared.Core.Commons.Bases;

namespace Shared.Core.Interfaces.Services
{
    public interface IOrderingQuery
    {
        IQueryable<T> Ordering<T>(BasePagination request, IQueryable<T> queryable) where T : class;
    }
}
