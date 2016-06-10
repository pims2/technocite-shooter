using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShooterTutorial.Utilities
{
    public class CollisionManager
    {
        public class CollisionEventArgs
        {
            public ICollidable first;
            public ICollidable second;
        }
 
        public event EventHandler<CollisionEventArgs> OnCollision;

        Dictionary<CollisionLayer, List<ICollidable>> _collidableLists = new Dictionary<CollisionLayer, List<ICollidable>>();
 
        public void Add(ICollidable collidable )
        {
            CollisionLayer group = collidable.CollisionGroup;

            lock (_collidableLists)
            {
                if (!_collidableLists.ContainsKey(group))
                {
                    _collidableLists.Add(group, new List<ICollidable>());
                }

                _collidableLists[group].Add(collidable);
            }
        }

        public void Remove(ICollidable collidable)
        {
            lock (_collidableLists)
            {
                _collidableLists[collidable.CollisionGroup].Remove(collidable);
            }
        }

        public void Update()
        {
            List<Task> tasks = new List<Task>();

            lock (_collidableLists)
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

                        var first_list_copy = first_list.Clone();
                        var second_list_copy = second_list.Clone();

                        tasks.Add(new Task(() => ProcessCollisions(this, first_list_copy, second_key, second_list_copy)));
                    }
                }
            }

            tasks.StartAndWaitForAll();
        }

        private static void ProcessCollisions(CollisionManager _this, List<ICollidable> first_list, CollisionLayer second_key, List<ICollidable> second_list)
        {
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
                            CollisionEventArgs args = new CollisionEventArgs();
                            args.first = first_collidable;
                            args.second = second_collidable;
                            _this.OnCollision(_this, args);
                        }
                    }
                }
            }
        }
    }

    static class Extensions
    {
        public static List<T> Clone<T>(this List<T> list)
        {
            return list.Select(item => item).ToList();
        }

        public static void StartAndWaitForAll(this List<Task> tasks)
        {
            tasks.AsParallel().ForAll(task => task.Start());

            Task.WaitAll(tasks.ToArray());
        }
    }
}
