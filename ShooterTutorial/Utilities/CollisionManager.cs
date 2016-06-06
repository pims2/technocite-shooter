using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShooterTutorial.Utilities
{
    class CollisionManager
    {
        Dictionary<CollisionLayer, List<ICollidable>> _collidableLists = new Dictionary<CollisionLayer, List<ICollidable>>();
 
        public void Add(ICollidable collidable )
        {
            CollisionLayer group = collidable.CollisionGroup;
 
            if (!_collidableLists.ContainsKey(group))
            {
                _collidableLists.Add(group, new List<ICollidable>());
            }

            _collidableLists[group].Add(collidable);
        }

        public void Remove(ICollidable collidable)
        {
            _collidableLists[collidable.CollisionGroup].Remove(collidable);
        }

        public void Update()
        {
            for (int i = 0; i < _collidableLists.Count; i++)
            {
                var first = _collidableLists.ElementAt(i);
                var first_key = first.Key;
                var first_list = first.Value;

                for (int j = 0; j < _collidableLists.Count; j++)
                {
                    var second = _collidableLists.ElementAt(j);
                    var second_key = second.Key;
                    var second_list = second.Value;

                    foreach (var first_collidable in first_list)
                    {
                        if ((first_collidable.CollisionLayers & second_key) != 0)
                        {
                            foreach (var second_collidable in second_list)
                            {
                                if (
                                    first_collidable != second_collidable
                                    && first_collidable.BoundingRectangle.Intersects(second_collidable.BoundingRectangle)
                                    )
                                {
                                    first_collidable.OnCollision(second_collidable);
                                }
                            }
                        }
                    }
                }
            }
        }
    }
}
