using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Xna.Framework;

namespace ShooterTutorial.GameObjects
{
    class WaveMovement: IMovement
    {
        // postion of the laser
        public Vector2 Position;

        // the speed the laser traves
        float laserMoveSpeed = 30f;

        int Val1, Val2;

        public WaveMovement(Vector2 position, int val1, int val2)
        {
            Position = position;

            Val1 = val1;
            Val2 = val2;
        }

        public void update(GameTime gameTime)
        {
            Position.X += laserMoveSpeed/* * Direction*/;
            Position.Y += (float)Math.Sin(Position.X/Val1)*Val2;
        }

        public Vector2 getPosition()
        {
            return Position;
        }

        public float getRotation()
        {
            return 0f;
        }

        public static WaveMovement create(Vector2 position, float verticalOffset, int val1, int val2)
        {
            // Adjust the position slightly to match the muzzle of the cannon.
            position.Y += 37 + verticalOffset;
            position.X += 70;

            return new WaveMovement(position, val1, val2);
        }
    }
}
