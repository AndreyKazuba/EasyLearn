﻿namespace EasyLearn.Data.Helpers
{
    public static class NumberHelper
    {
        public static int GetRangedValue(int value, int min, int max)
        {
            if (value < min) return min;
            if (value > max) return max;
            return value;
        }
    }
}
