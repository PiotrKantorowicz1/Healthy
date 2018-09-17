using System;
using System.Linq;
using Healthy.Core.Pagination;
using Healthy.Core.Types;

namespace Healthy.Infrastructure.Extensions
{
    public static class PagedResultExtensions
    {
        public static Maybe<PagedResult<TResult>> Select<TSource, TResult>(this Maybe<PagedResult<TSource>> result,
                Func<TSource, TResult> selector)
            => result.HasValue ? result.Value.Select(selector) : new Maybe<PagedResult<TResult>>();

        public static PagedResult<TResult> Select<TSource, TResult>(this PagedResult<TSource> result,
            Func<TSource, TResult> selector)
        {
            var mappedResults = result.Items.Select(selector);

            return PagedResult<TResult>.From(result, mappedResults);
        }
    }
}
