using System.Collections.Generic;

namespace Fretefy.Test.Domain.DTOs
{
    public class PagedResult<TEntity>
    {
        public int TotalItems { get; set; }
        public List<TEntity> Items { get; set; }
    }
}
