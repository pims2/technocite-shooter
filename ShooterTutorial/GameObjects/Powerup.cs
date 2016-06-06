﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using ShooterTutorial.Utilities; 

namespace ShooterTutorial.GameObjects
{
    class Powerup : IDrawable2, ICollidable
    {
        public Animation PowerupAnimation;

        public Vector2 Position;

        public bool Active;

        public double Time;

        public Weapon Weapon;

        public int Layer
        {
            get { return 10; }
        }

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

        public CollisionLayer CollisionGroup
        {
            get
            {
                return CollisionLayer.PowerUp;
            }
        }

        public CollisionLayer CollisionLayers
        {
            get
            {
                return CollisionLayer.Laser | CollisionLayer.Player;
            }
        }

        public Rectangle BoundingRectangle
        {
            get
            {
                return new Rectangle(
                        (int)Position.X,
                        (int)Position.Y,
                        Width,
                        Height);
            }
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

        public void OnCollision(ICollidable other)
        {
            Active = false;
        }
    }
}
