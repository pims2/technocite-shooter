using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Xna.Framework;

namespace ShooterTutorial.GameObjects
{
    class LinearMovementRotation: IMovement
    {
        // postion of the laser
        public Vector2 Position;

        // Direction of the laser
        public Vector2 Direction;

        // the speed the laser traves
        float laserMoveSpeed = 30f;
        // rotation
        float Rotation = 0f;

        public LinearMovementRotation(Vector2 position, Vector2 direction, float rotation)
        {
            Position = position;
            Rotation = rotation;
            Direction = direction;
            Direction.Normalize();
        }

        public void update( GameTime gameTime )
        {
            Position += laserMoveSpeed * Direction;
        }

        public Vector2 getPosition()
        {
            return Position;
        }

        public float getRotation()
        {
            return Rotation;
        }

        public static LinearMovementRotation create(Vector2 position, float verticalOffset, float angle, float rotation)
        {
            // Adjust the position slightly to match the muzzle of the cannon.
            position.Y += 37 + verticalOffset;
            position.X += 70;

            var direction = new Vector2();

            direction.X = (float)Math.Cos((double)angle);
            direction.Y = (float)Math.Sin((double)angle);

            return new LinearMovementRotation(position, direction, rotation);
        }
    }
}
