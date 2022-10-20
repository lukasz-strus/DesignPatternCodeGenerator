using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesignPatternCodeGenerator.Base.Helpers
{
    public static class Groupings
    {
        public static IGrouping<TKey, TElement>
            FilterElements<TKey, TElement>(
            this IGrouping<TKey, TElement> grouping,
            Func<TElement, bool> predicate) =>
            new LateGrouping<TKey, TElement>(grouping.Key, grouping.Where(predicate));
    }

    internal class LateGrouping<TKey, TElement> : IGrouping<TKey, TElement>
    {
        public TKey Key { get; }
        private readonly IEnumerable<TElement> elements;

        public LateGrouping(TKey key, IEnumerable<TElement> elements)
        {
            Key = key;
            this.elements = elements;
        }

        public IEnumerator<TElement> GetEnumerator() => elements.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}
