using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Windows.System.Threading;
using Windows.Foundation;
using System.Threading.Tasks;

namespace ShooterTutorial.Utilities
{
    class EntityList<T> where T : IUpdateable2, IDrawable2, new()
    {
        private List<T> list;
        private EntityPool<T> pool;
        private GraphicScene scene;
        private CollisionManager _collisionManager;

        public int Count
        {
            get { return list.Count; }
        }

        public EntityList( GraphicScene scene, CollisionManager collisionManager)
        {
            this.scene = scene;
            _collisionManager = collisionManager;
            list = new List<T>();
            pool = new EntityPool<T>();
            pool.Initialize(0);
        }

        public T this[int index]
        {
            get { return list[index]; }
        }

        public T Add()
        {
            T item = default(T);
            lock (list)
            {
                item = pool.Get();
                list.Add(item);
            }
            lock (scene)
            {
                scene.Add(item);
            }

            if( item is ICollidable )
            {
                _collisionManager.Add((ICollidable)item);
            }

            return item;
        }

        protected void UpdateEntity(T entity, Game game, GameTime gameTime )
        {
            entity.Update(game, gameTime);

            if (!entity.Active)
            {
                T item = entity;
                if (item is ICollidable)
                {
                    _collisionManager.Remove((ICollidable)item);
                }
                lock(scene)
                {
                    scene.Remove(item);
                }
                lock(list)
                {
                    pool.Put(item);
                    list.Remove(item);
                }
            };
        }

        public void Update(Game game, GameTime gameTime)
        {
            Task[] tasks = new Task[list.Count];
            int count = list.Count;
            T[] copy = new T[list.Count];

            list.CopyTo(copy);
            for (var i = 0; i < count; i++)
            {
                var entity = copy[i];

                tasks[i] =Task.Run(
                    () => UpdateEntity(entity, game, gameTime)
                    );
            }

            Task.WaitAll(tasks);
        }
    }
}
