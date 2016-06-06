﻿using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

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
            T item = pool.Get();
            list.Add(item);
            scene.Add(item);

            if( item is ICollidable )
            {
                _collisionManager.Add((ICollidable)item);
            }

            return item;
        }

        public void Update(Game game, GameTime gameTime)
        {
            for (var i = 0; i < list.Count; i++)
            {
                list[i].Update(game, gameTime);

                if (!list[i].Active)
                {
                    T item = list[i];
                    pool.Put(item);
                    if (item is ICollidable)
                    {
                        _collisionManager.Remove((ICollidable)item);
                    }
                    scene.Remove(item);
                    list.Remove(item);
                };
            }
        }
    }
}
