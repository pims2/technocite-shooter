using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Xna.Framework;

namespace ShooterTutorial.GameObjects
{
    class VariableSpeedMovement :IMovement
    {
        private Vector2 Position;
        private Vector2 Direction;
        private float Offset;
        private double Time;

        private float Speed = 5f;
        private float OffsetSize = 200f;

        public VariableSpeedMovement(Vector2 position, Vector2 direction)
        {
            Position = position;
            Direction = direction;
            Time = 0;
            Offset = 0;
        }

        public void update(GameTime gameTime)
        {
            Time += gameTime.ElapsedGameTime.TotalSeconds;
            Position += Direction * Speed;
            Offset = OffsetSize * (float)Math.Sin(Time);
        }

        public Vector2 getPosition()
        {
            return Position + Offset * Direction;
        }

        public float getRotation()
        {
            return (float)Math.Atan2(Direction.Y, Direction.X);
        }

        public static VariableSpeedMovement create(Vector2 position, float verticalOffset, float angle)
        {
            // Adjust the position slightly to match the muzzle of the cannon.
            position.Y += 37 + verticalOffset;
            position.X += 70;

            var direction = new Vector2();

            direction.X = (float)Math.Cos((double)angle);
            direction.Y = (float)Math.Sin((double)angle);

            return new VariableSpeedMovement(position, direction);
        }
    }
}
