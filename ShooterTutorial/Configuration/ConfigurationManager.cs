using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using Windows.Data.Json;
using System.Reflection;

namespace ShooterTutorial.Configuration
{
    class ConfigurationManager
    {
        private static List<ConfigurationValueBase> ConfigurationTable = new List<ConfigurationValueBase>();
        private static bool ItIsInitialized = false;

        public static ConfigurationValue<T> create<T>( string name, string description )
        {
            ConfigurationValue<T> configuration_value;

            configuration_value = get<T>(name);

            if (configuration_value == null)
            {
                configuration_value = new ConfigurationValue<T>(name, description);

                ConfigurationTable.Add(configuration_value);
            }
            else
            {
                configuration_value.Description = description;
            }

            return configuration_value;
        }

        public static ConfigurationTable<T> createTable<T>(string name, string description)
        {
            ConfigurationTable<T> configuration_value;

            configuration_value = getTable<T>(name);

            if (configuration_value == null)
            {
                configuration_value = new ConfigurationTable<T>(name, description);

                ConfigurationTable.Add(configuration_value);
            }
            else
            {
                configuration_value.Description = description;
            }

            return configuration_value;
        }

        public static ConfigurationValue<T> create<T>(string name)
        {
            ConfigurationValue<T> configuration_value = new ConfigurationValue<T>(name);

            ConfigurationTable.Add(configuration_value);

            return configuration_value;
        }

        public static ConfigurationTable<T> createTable<T>(string name)
        {
            ConfigurationTable<T> configuration_value = new ConfigurationTable<T>(name, "");

            ConfigurationTable.Add(configuration_value);

            return configuration_value;
        }

        public static ConfigurationValue<T> get<T>(string name)
        {
            var result = ConfigurationTable.Find(value => value.Name == name);

            if( result == null )
            {
                result = create<T>(name);
            }

            return (ConfigurationValue < T >)result;
        }

        public static ConfigurationTable<T> getTable<T>(string name)
        {
            var result = ConfigurationTable.Find(value => value.Name == name);

            if (result == null)
            {
                result = createTable<T>(name);
            }

            return (ConfigurationTable<T>)result;
        }

        public static void LoadConfiguration( JsonObject content )
        {
            if( !ItIsInitialized )
            {
                ItIsInitialized = true;
                Initialize();
            }

            LoadConfigurationPrefixed(content, "");
        }

        private static void LoadConfigurationPrefixed( JsonObject content, string prefix)
        {
            foreach (var value in content)
            {
                switch (value.Value.ValueType)
                {
                    case JsonValueType.Boolean:
                        {
                            ConfigurationValue<bool> configuration_value = get<bool>(prefix + value.Key);

                            configuration_value.Set(value.Value.GetBoolean());
                        }
                        break;
                    case JsonValueType.Number:
                        {
                            ConfigurationValue<int> configuration_value = get<int>(prefix + value.Key);

                            configuration_value.Set((int)value.Value.GetNumber());
                        }
                        break;

                    case JsonValueType.Object:
                        {
                            LoadConfigurationPrefixed(value.Value.GetObject(), prefix + value.Key + '.');
                        }
                        break;

                    case JsonValueType.Array:
                        {
                            ConfigurationTable<int> configuration_table = getTable<int>(prefix + value.Key);

                            configuration_table.Clear();

                            foreach (var item in value.Value.GetArray())
                            {
                                configuration_table.Add((int)item.GetNumber());
                            }
                        }
                        break;

                    default:
                        break;
                }
            }
        }

        public static void logConfiguration()
        {
            foreach( var value in ConfigurationTable )
            {
                Debug.WriteLine(value.Name + " : " + value.Description);
            }
        }

        private static void Initialize()
        {
            Assembly assembly = typeof(ConfigurationManager).GetTypeInfo().Assembly;

            foreach( TypeInfo type in assembly.DefinedTypes )
            {
                foreach( var field in type.DeclaredFields)
                {
                    if( field.IsStatic )
                    {
                        var attribute = field.GetCustomAttribute<Configuration>();

                        if (attribute != null)
                        {
                            switch (field.FieldType.Name)
                            {
                                case "Int32":
                                    {
                                        ConfigurationValue<int> value = create<int>(attribute.Name, attribute.Description);

                                        value.Set( (int)field.GetValue(null) );

                                        value.AddField(field);
                                    }
                                    break;

                                case "Single":
                                    {
                                        ConfigurationValue<float> value = create<float>(attribute.Name, attribute.Description);

                                        value.Set((float)field.GetValue(null));

                                        value.AddField(field);
                                    }
                                    break;

                                case "String":
                                    {
                                        ConfigurationValue<string> value = create<string>(attribute.Name, attribute.Description);

                                        value.Set((string)field.GetValue(null));

                                        value.AddField(field);
                                    }
                                    break;

                                case "Boolean":
                                    {
                                        ConfigurationValue<bool> value = create<bool>(attribute.Name, attribute.Description);

                                        value.Set((bool)field.GetValue(null));

                                        value.AddField(field);
                                    }
                                    break;

                                case "List`1":
                                    {
                                        var createTable = GetCreateTable(field.FieldType.GenericTypeArguments[0]);
                                        ConfigurationValueBase table = (ConfigurationValueBase)createTable.Invoke(null, new object[] { attribute.Name, attribute.Description } );

                                        var getter = table.GetType().GetRuntimeMethod("Get", new Type[] { });

                                        field.SetValue(null, getter.Invoke(table, null));
                                    }
                                    break;

                                default:
                                    break;
                            }
                            
                        }
                    }
                }
            }
        }

        private static MethodInfo GetCreateTable(Type type)
        {
            var createMethods = typeof(ConfigurationManager).GetRuntimeMethods()
                                .Where(m => m.Name == "createTable" && m.IsGenericMethodDefinition)
                                .First();
            return createMethods.MakeGenericMethod(new Type[] { type });
        }
    }
}

