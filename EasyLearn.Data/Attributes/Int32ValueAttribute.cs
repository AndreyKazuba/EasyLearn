using System;

namespace EasyLearn.Data.Attributes
{
    [AttributeUsage(AttributeTargets.Field, AllowMultiple = false)]
    public class Int32ValueAttribute : Attribute
    {
        public int Value { get; set; }
        public Int32ValueAttribute(int value) => Value = value;
    }
}
