using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Windows.Data.Json;

namespace ShooterTutorial.GameObjects
{
    class EnemyFactory
    {
        struct EnemyDefinition
        {
            public Type MovementType;
            public float Speed;
        };

        private List<EnemyDefinition> _TemplateList = new List<EnemyDefinition>();
        private Random _Random = new Random();

        public EnemyFactory()
        {

        }

        public void Initialize( JsonArray enemy_table )
        {
            var assembly = typeof(EnemyFactory).GetTypeInfo().Assembly;

            foreach( var enemy_template in enemy_table )
            {
                var enemy_template_object = (JsonObject)enemy_template.GetObject();
                EnemyDefinition definition = new EnemyDefinition();

                definition.MovementType = assembly.DefinedTypes.Where( t =>
                    t.Name == enemy_template_object.GetNamedString("Movement")
                    ).Single().AsType();
                definition.Speed = (float)enemy_template_object.GetNamedNumber("Speed");

                if (definition.MovementType == null)
                {
                    MessageBox.Show("Unable to find type " + enemy_template_object.GetNamedString("Movement"), "Invalid type in enemies.json", new String[] { "Ok" });
                }

                _TemplateList.Add(definition);
            }
        }

        Enemy GetRandomEnemy()
        {
            return null;
        }
    }
}
