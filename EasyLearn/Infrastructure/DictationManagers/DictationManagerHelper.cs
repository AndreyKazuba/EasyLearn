using System;
using System.Collections.Generic;
using System.Linq;

namespace EasyLearn.Infrastructure.DictationManagers
{
    public static class DictationManagerHelper
    {
        public static IEnumerable<T> Shuffle<T>(IEnumerable<T> items)
        {
            Random random = new Random();
            return items.OrderBy(relation => random.Next());
        }
    }
}
