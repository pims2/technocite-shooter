using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Graphics;

namespace ShooterTutorial.Utilities
{
    class GraphicScene
    {
        List<IDrawable2> ObjectList;

        public GraphicScene()
        {
            ObjectList = new List<IDrawable2>();
        }

        public void Add( IDrawable2 _object )
        {
            ObjectList.Add(_object);
        }

        public void Remove(IDrawable2 _object)
        {
            ObjectList.Remove(_object);
        }

        private static int CompareLayer(IDrawable2 a, IDrawable2 b)
        {
            return a.Layer - b.Layer;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            ObjectList.Sort( CompareLayer );

            foreach( var o in ObjectList )
            {
                o.Draw(spriteBatch);
            }
        }
    }
}
