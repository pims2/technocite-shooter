using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShooterTutorial.Configuration
{
    class ConfigurationTable<T> : ConfigurationValueBase
    {
        List<T> ValueTable = new List<T>();

        public ConfigurationTable( string name, string description) : base(name, description )
        {
            
        }

        public static implicit operator List<T>( ConfigurationTable<T> value )
        {
            return value.ValueTable;
        }

        public void Clear()
        {
            ValueTable.Clear();
        }

        public void Add(T value)
        {
            ValueTable.Add(value);
        }

        public List<T> Get()
        {
            return ValueTable;
        }
    }
}
