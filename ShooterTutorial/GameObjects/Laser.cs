using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ShooterTutorial;



namespace ShooterTutorial.GameObjects
{
    public class Laser 
    {

        // animation the represents the laser animation.
        public Animation LaserAnimation;

        // postion of the laser
        public Vector2 Position;
      
        // Movement algorithm
        IMovement Movement;

        // The damage the laser deals.
        int Damage = 10;

        // set the laser to active
        public bool Active;

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

        public void Update(GameTime gameTime)
        {
            Movement.update(gameTime);

            Position = Movement.getPosition();

            LaserAnimation.Position = Position;
            LaserAnimation.Update(gameTime);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            LaserAnimation.Draw(spriteBatch, Movement.getRotation());
        }
    }
}
