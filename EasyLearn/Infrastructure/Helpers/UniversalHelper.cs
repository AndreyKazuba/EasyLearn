using System;
using System.Collections.Generic;
using System.Linq;

namespace EasyLearn.Infrastructure.Helpers
{
    public static class UniversalHelper
    {
        public static IEnumerable<T> Shuffle<T>(this IEnumerable<T> items)
        {
            Random random = new Random();
            return items.OrderBy(relation => random.Next());
        }
    }
}
