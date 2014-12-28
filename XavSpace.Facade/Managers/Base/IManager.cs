using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XavSpace.Facade.Managers.Base
{
    public interface IManager<TEntity, TKey>
    {
        int Add(TEntity item);

        int Update(TEntity item);

        TEntity Get(TKey id);
        IEnumerable<TEntity> Get();

        int Delete(TKey id);
    }
}
