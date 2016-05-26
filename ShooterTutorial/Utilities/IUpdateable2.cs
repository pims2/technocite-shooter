using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Xna.Framework;

namespace ShooterTutorial.Utilities
{
    interface IUpdateable2
    {
        void Update(Game game, GameTime gameTime);

        bool Active { get; }
    }
}
