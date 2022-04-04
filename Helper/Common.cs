using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace _123PayApp.Helper
{
    public static class Common
    {
        public static IQueryable<T> PagedRecords<T>(this IQueryable<T> data, int pageNumber, int pageSize)
        {
            IQueryable<T> result;

            var _pageNum = pageNumber < 1 ? 1 : pageNumber;
            var _pageSize = pageSize < 10 ? 10 : pageSize;

            result = data
                .Skip((_pageNum - 1) * _pageSize)
                .Take(_pageSize);

            return result;
        }
    }
}
