using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XavSpace.Facade.Managers.Base
{
    public interface IAsyncManager<TEntity, TKey>
    {
        Task<int> AddAsync(TEntity item);

        Task<int> UpdateAsync(TEntity item);

        Task<TEntity> GetAsync(TKey id);
        Task<IEnumerable<TEntity>> GetAsync();

        Task<int> DeleteAsync(TKey id);
    }
}
