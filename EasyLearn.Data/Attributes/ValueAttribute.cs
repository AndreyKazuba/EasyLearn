using System;

namespace EasyLearn.Data.Attributes
{
    [AttributeUsage(AttributeTargets.Field, AllowMultiple = false)]
    public class ValueAttribute : Attribute
    {
        public string Value { get; set; }
        public ValueAttribute(string value)
        {
            this.Value = value;
        }
    }
}
