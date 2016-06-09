using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace ShooterTutorial.GameObjects
{
    class WeaponManager
    {
        List<TypeInfo> _weaponTypeList = new List<TypeInfo>();
        Random random = new Random();

        public WeaponManager()
        {
            Assembly currentAssembly = typeof(WeaponManager).GetTypeInfo().Assembly;

            foreach( var type in currentAssembly.DefinedTypes)
            {
                if( type.GetCustomAttribute(typeof(WeaponDefinitionAttribute)) != null)
                {
                    if( type.Name == "Weapon")
                    {
                        continue;
                    }
                    _weaponTypeList.Add(type);
                }
            }
        }

        public Weapon GetRandomWeapon(Game1 game, Player player)
        {

            int i = random.Next(_weaponTypeList.Count);

             return (Weapon)Activator.CreateInstance((_weaponTypeList[i]).AsType(), new object[] { game, player });
        }
    }
}
