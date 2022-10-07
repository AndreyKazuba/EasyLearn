using System;
using System.Threading;

namespace EasyLearn.Infrastructure.Helpers
{
    public static class RandomFactory
    {
        private static int ticks = Environment.TickCount;
        public static Random GetRandom(int seed = 0)
        {
            return new Random(Interlocked.Increment(ref ticks) + seed);
        }
    }
}
