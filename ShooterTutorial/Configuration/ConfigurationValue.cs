using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShooterTutorial.Configuration
{
    class ConfigurationValue<T> : ConfigurationValueBase
    {
        private T Value;

        public static implicit operator T( ConfigurationValue<T> value )
        {
            return value.Value;
        }

        public ConfigurationValue(string name, T value, string description) :
            base(name, description)
        {
            Value = value;
        }
    }
}
