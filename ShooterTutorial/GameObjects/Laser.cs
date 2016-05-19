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

        // the speed the laser traves
        float laserMoveSpeed = 30f;

        // postion of the laser
        public Vector2 Position;

        // Direction of the laser
        public Vector2 Direction;

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

        public void Initialize(Animation animation, Vector2 position, Vector2 direction)
        {
            LaserAnimation = animation;
            Position = position;

            Direction = direction;
            Direction.Normalize();

            Active = true;
        }

        public void Update(GameTime gameTime)
        {
            //Position.X += laserMoveSpeed;

            //Position.X += laserMoveSpeed * Direction.X;
            //Position.Y += laserMoveSpeed * Direction.Y;

            Position += laserMoveSpeed * Direction;

            LaserAnimation.Position = Position;
            LaserAnimation.Update(gameTime);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            LaserAnimation.Draw(spriteBatch);
        }
    }
}
