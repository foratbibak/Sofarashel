using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace Sofarashel.Domain.ViewModels.Common
{
    public class PagedResult<T>
    {
        public PagedResult()
        {
            PageNumber = 1;
            PageSize = 10;
            Items = new List<T>();
        }

        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public int TotalCount { get; set; }
        public int PageCount { get; set; }
        public List<T> Items { get; set; }

        public async Task PagingAsync(IQueryable<T> query)
        {
            TotalCount = await query.CountAsync();
            PageCount = (int)Math.Ceiling(TotalCount / (double)PageSize);

            if (PageNumber > PageCount)
            {
                PageNumber = PageCount < 1 ? 1 : PageCount;
            }

            var skip = (PageNumber - 1) * PageSize;
            Items = await query.Skip(skip).Take(PageSize).ToListAsync();
        }
    }
}