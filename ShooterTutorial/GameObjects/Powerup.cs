using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace ShooterTutorial.GameObjects
{
    class Powerup
    {
        public Animation PowerupAnimation;

        public Vector2 Position;

        public bool Active;

        public double Time;

        public Powerup(Animation animation, Vector2 position)
        {
            PowerupAnimation = animation;
            Position = position;
            Active = true;
            Time = 0;
        }

        public void Update(GameTime gameTime)
        {
            PowerupAnimation.Update(gameTime);
            PowerupAnimation.Position = Position;

            Time += gameTime.ElapsedGameTime.TotalSeconds;

            if( Time > 5.0f )
            {
                Active = false;
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            // draw the animation
            PowerupAnimation.Draw(spriteBatch);
        }

    }
}
