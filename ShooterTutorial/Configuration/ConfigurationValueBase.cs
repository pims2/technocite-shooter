using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Data.Json;

namespace ShooterTutorial.Configuration
{
    class ConfigurationValueBase
    {
        public string Name
        {
            get { return _Name; }
        }

        string _Name;
        public string Description;

        public ConfigurationValueBase(string name, string description)
        {
            _Name = name;
            Description = description;
        }
    }
}