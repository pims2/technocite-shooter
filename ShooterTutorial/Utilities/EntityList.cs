using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace ShooterTutorial.Utilities
{
    class EntityList<T> where T : IUpdateable2, IDrawable2
    {
        private List<T> list;

        public int Count
        {
            get { return list.Count; }
        }

        public EntityList()
        {
            list = new List<T>();
        }

        public T this[int index]
        {
            get { return list[index]; }
        }

        public void Add(T item)
        {
            list.Add(item);
        }

        public void Update(GameTime gameTime)
        {
            for (var i = 0; i < list.Count; i++)
            {
                list[i].Update(gameTime);

                if (!list[i].Active)
                {
                    list.Remove(list[i]);
                }
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            for (var i = 0; i < list.Count; i++)
            {
                list[i].Draw(spriteBatch);
            }
        }
    }
}
