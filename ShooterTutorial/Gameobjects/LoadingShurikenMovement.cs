using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Xna.Framework;

namespace ShooterTutorial.GameObjects
{
    class LoadingShurikenMovement: IMovement
    {
        // postion of the laser
        public Vector2 Position;
        private double Time;
        //private float Offset;
        // Direction of the laser
        public Vector2 Direction;
        public float getRandom()
        {
            Random rnd1 = new Random();

            return (float)rnd1.Next(0, 60);
        }

        // the speed the laser travels
  
        //private float Offsetsize = 200f;
      
        public LoadingShurikenMovement(Vector2 position, Vector2 direction)
        {
            Position = position;

            Direction = direction;
            Direction.Normalize();
        }

        public void update( GameTime gameTime )
        {
            
                


            Time += gameTime.ElapsedGameTime.TotalSeconds;
            if (Time <= 2)
            {
                Position -= 2f * Direction;
            }
            else
            {
                Position += 40f * Direction;

            }

            //Offset = Offsetsize * (float)Math.Tan(Time);
        }

        public Vector2 getPosition()
        {
            return Position;
        }

        public float getRotation()
        {
            Random rnd = new Random();
            float ang;
           int count = 10;
  
            ang = (float)rnd.Next(0, count);
            count++;
            return ang;
        }

        public static LinearMovement create(Vector2 position, float verticalOffset, float angle)
        {
            // Adjust the position slightly to match the muzzle of the cannon.
            position.Y += 37 + verticalOffset;
            position.X += 70;

            var direction = new Vector2();

            direction.X = (float)Math.Cos((double)angle);
            direction.Y = (float)Math.Sin((double)angle);

            return new LinearMovement(position, direction);
        }
    }
}
