using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShooterTutorial.Configuration
{
    class ConfigurationValueBase
    {
        public string Name
        {
            get { return _Name; }
        }

        public string Description
        {
            get { return _Description; }
        }

        string _Name;
        string _Description;

        public ConfigurationValueBase(string name, string description)
        {
            _Name = name;
            _Description = description;
        }
    }
}
