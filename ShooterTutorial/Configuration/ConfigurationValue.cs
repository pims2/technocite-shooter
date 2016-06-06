using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Windows.Data.Json;

namespace ShooterTutorial.Configuration
{
    class ConfigurationValue<T> : ConfigurationValueBase
    {
        private T Value;
        private List<FieldInfo> Fields;

        public static implicit operator T( ConfigurationValue<T> value )
        {
            return value.Value;
        }

        public ConfigurationValue(string name, string description) :
            base(name, description)
        {
            Fields = new List<FieldInfo>();
        }

        public ConfigurationValue(string name) :
            base(name, "")
        {
            Fields = new List<FieldInfo>();
        }

        public void Set(T value)
        {
            Value = value;

            foreach( var field in Fields )
            {
                field.SetValue(null, value);
            }

        }

        public void AddField(FieldInfo field)
        {
            Fields.Add(field);
        }
    }
}
