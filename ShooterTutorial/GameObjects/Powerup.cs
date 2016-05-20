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

        public Weapon Weapon;

        // Get the width of the enemy ship
        public int Width
        {
            get { return PowerupAnimation.FrameWidth; }
        }

        // Get the height of the enemy ship.
        public int Height
        {
            get { return PowerupAnimation.FrameHeight; }
        }

        public Powerup(Animation animation, Vector2 position, Weapon weapon)
        {
            PowerupAnimation = animation;
            Position = position;
            Weapon = weapon;
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
