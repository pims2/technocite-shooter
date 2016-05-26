using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ShooterTutorial.Utilities;
using ShooterTutorial;



namespace ShooterTutorial.GameObjects
{
    public class Laser : IUpdateable2, IDrawable2
    {

        // animation the represents the laser animation.
        public Animation LaserAnimation;

        // postion of the laser
        public Vector2 Position;
      
        // Movement algorithm
        IMovement Movement;

        // The damage the laser deals.
        int Damage = 10;

        public int Layer
        {
            get { return 1; }
        }

        // set the laser to active
        public bool Active
        {
            get { return _Active; }
            set { _Active = value; }
        }

        private bool _Active;

        // Range of the laser.
        int Range;

        // the width of the player image.
        public int Width
        {
            get { return LaserAnimation.FrameWidth; }
        }

        // the height of the player image.
        public int Height
        {
            get { return LaserAnimation.FrameHeight; }

        }

        public void Initialize(Animation animation, IMovement movement)
        {
            LaserAnimation = animation;
            Movement = movement;
            Position = movement.getPosition();

            Active = true;
        }

        public void Update(Game game, GameTime gameTime)
        {
            Movement.update(gameTime);

            Position = Movement.getPosition();

            LaserAnimation.Position = Position;
            LaserAnimation.Update(gameTime);

            _Active = Position.X < game.GraphicsDevice.Viewport.Width;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            LaserAnimation.Draw(spriteBatch, Movement.getRotation());
        }
    }
}
