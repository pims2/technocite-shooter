using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using Windows.Data.Json;

namespace ShooterTutorial.Configuration
{
    class ConfigurationManager
    {
        private static List<ConfigurationValueBase> ConfigurationTable = new List<ConfigurationValueBase>();

        public static ConfigurationValue<T> create<T>( string name, T value, string description )
        {
            ConfigurationValue<T> configuration_value = new ConfigurationValue<T>(name, value, description);

            ConfigurationTable.Add(configuration_value);

            return configuration_value;
        }

        public static void LoadConfiguration( JsonObject content )
        {
            
        }

        public static void logConfiguration()
        {
            foreach( var value in ConfigurationTable )
            {
                Debug.WriteLine(value.Name + " : " + value.Description);
            }
        }
    }
}
