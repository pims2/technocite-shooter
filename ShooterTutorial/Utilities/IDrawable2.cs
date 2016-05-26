using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Graphics;

namespace ShooterTutorial.Utilities
{
    interface IDrawable2
    {
        int Layer { get; }
        void Draw(SpriteBatch spriteBatch);
    }
}
