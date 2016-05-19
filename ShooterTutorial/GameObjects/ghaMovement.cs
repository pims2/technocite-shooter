using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Xna.Framework;

namespace ShooterTutorial.GameObjects
{
    class ghaMovement: IMovement
    {
        // postion of the laser
        public Vector2 Position;

        // Direction of the laser
        public Vector2 Direction;

        // the speed the laser traves
        float laserMoveSpeed = 20f;
        // rotation
        float Rotation = 0f;
        double cpt;
        bool etat;

        public ghaMovement(Vector2 position, Vector2 direction, float rotation)
        {
            Position = position;
            Rotation = rotation;
            Direction = direction;
            cpt = 0;
            etat = true;
            Direction.Normalize();
        }

        public void update( GameTime gameTime )
        {
            cpt++;
            Rotation+=(float)gameTime.ElapsedGameTime.TotalSeconds-4.5f;
            
            if (etat)
            {
                Position += laserMoveSpeed-- * Direction;
                etat = laserMoveSpeed < 0 ? !etat : etat;
            }
            else
            {
                if (cpt > 50)
                {
                    Position += (laserMoveSpeed + 4 * 4.50f) * Direction;
                    Direction.X+=0.01f;
                }
            }
        }

        public Vector2 getPosition()
        {
            return Position;
        }

        public float getRotation()
        {
            return Rotation ;
        }

        public static ghaMovement create(Vector2 position, float verticalOffset, float angle, float rotation)
        {
            // Adjust the position slightly to match the muzzle of the cannon.
            position.Y += 37 + verticalOffset;
            position.X += 70;
            
            var direction = new Vector2();

            direction.X = (float)Math.Cos((double)angle) ;
            direction.Y = (float)Math.Sin((double)angle) ;

            return new ghaMovement(position, direction, rotation);
        }
    }
}
