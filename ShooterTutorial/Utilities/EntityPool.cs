using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShooterTutorial.Utilities
{
    class EntityPool<T> where T:new()
    {
        List<T> availableObject;
        List<T> usedObject;

        public EntityPool()
        {
            availableObject = new List<T>();
            usedObject = new List<T>();
        }

        public void Initialize( int count )
        {
            for( int i = 0; i < count; ++i)
            {
                availableObject.Add(new T());
            }
        }

        public T Get()
        {
            if (availableObject.Count > 0)
            {
                T obj = availableObject[availableObject.Count - 1];
                usedObject.Add(obj);
                availableObject.Remove(obj);

                return obj;
            }
            else
            {
                T obj = new T();
                usedObject.Add(obj);

                return obj;
            }

        }

        public void Put( T obj )
        {
            if( usedObject.Contains( obj ))
            {
                usedObject.Remove(obj);
                availableObject.Add(obj);
            }
            else
            {
                //throw new string("Object is not part of the pool");
            }
        }

        
    }
}
