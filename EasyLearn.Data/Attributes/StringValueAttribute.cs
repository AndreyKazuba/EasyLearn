using System;

namespace EasyLearn.Data.Attributes
{
    [AttributeUsage(AttributeTargets.Field, AllowMultiple = false)]
    public class StringValueAttribute : Attribute
    {
        public string Value { get; set; }
        public StringValueAttribute(string value) => Value = value;
    }
}
