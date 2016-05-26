using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace ShooterTutorial.Utilities
{
    class EntityList<T> where T : IUpdateable2, IDrawable2
    {
        private List<T> list;
        private GraphicScene scene;

        public int Count
        {
            get { return list.Count; }
        }

        public EntityList( GraphicScene scene )
        {
            this.scene = scene;
            list = new List<T>();
        }

        public T this[int index]
        {
            get { return list[index]; }
        }

        public void Add(T item)
        {
            list.Add(item);
            scene.Add(item);
        }

        public void Update(Game game, GameTime gameTime)
        {
            for (var i = 0; i < list.Count; i++)
            {
                list[i].Update(game, gameTime);

                if (!list[i].Active)
                {
                    scene.Remove(list[i]);
                    list.Remove(list[i]);
                }
            }
        }
    }
}
