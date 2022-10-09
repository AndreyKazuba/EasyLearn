using System;
using System.Collections.Generic;
using System.Linq;

namespace EasyLearn.Infrastructure.Helpers
{
    public static class UniversalHelper
    {
        public static IEnumerable<T> Shuffle<T>(this IEnumerable<T> items)
        {
            Random random = RandomFactory.GetRandom(items.Count());
            return items.OrderBy(relation => random.Next());
        }

        public static int OneIfZero(this int value) => value == 0 ? 1 : value;
    }
}
