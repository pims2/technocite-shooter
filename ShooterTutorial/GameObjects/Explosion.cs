using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ShooterTutorial;
using ShooterTutorial.Utilities;

namespace ShooterTutorial.GameObjects
{
    class Explosion : IUpdateable2, IDrawable2
    {
        public Animation explosionAnimation;

        public Vector2 Position;

        public bool Active
        {
            get { return _Active; }
            set { _Active = value; }
        }

        private bool _Active;

        int timeToLive;

        public int Layer
        {
            get { return 3; }
        }

        public int Height
        {
            get
            {
                return explosionAnimation.FrameHeight;
            }
        }

        public int Width
        {
            get
            {
                return explosionAnimation.FrameWidth;
            }
        }


        public void Initialize(Animation animation, Vector2 position)
        {
            explosionAnimation = animation;
            Position = position;
            Active = true;

            timeToLive = 30;
        }


        public void Update(Game game, GameTime gameTime)
        {
            explosionAnimation.Update(gameTime);

            timeToLive -= 1;

            if (timeToLive <= 0)
            {
                this.Active = false;
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            explosionAnimation.Draw(spriteBatch);
        }
    }
}
