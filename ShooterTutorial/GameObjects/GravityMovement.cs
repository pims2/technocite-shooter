using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
namespace ShooterTutorial.GameObjects
{
    class GravityMovement : IMovement
    {
        private Vector2 position, directionOfMove;
        private float speed;
        private Vector2 gravity;

        private float angle;

        public GravityMovement(Vector2 positionOfStart, Vector2 direction, float speedOfMove, Vector2 gravityVector)
        {
            position = positionOfStart;
            directionOfMove = direction;
            speed = speedOfMove;
            gravity = gravityVector;
        }

        public void update(GameTime time)
        {
            directionOfMove += gravity * (float)time.ElapsedGameTime.TotalSeconds;
                       
            position.X += speed * directionOfMove.X;
            position.Y += speed * directionOfMove.Y;
        }
        public Vector2 getPosition()
        {
            return position;
        }
        public float getRotation()
        {
            return angle;
        }
        public static GravityMovement Create(Vector2 position, float offsetY, float angle, Vector2 gravity,float inputSpeed = 30)
        {

            Vector2 tempPosition = new Vector2(position.X + 30, position.Y + 30 + offsetY);
            float angleInRadian = angle * (float)Math.PI / 180.0f;

            Vector2 laserDirection = new Vector2();

            laserDirection.X = (float)Math.Cos(angleInRadian);
            laserDirection.Y = (float)Math.Sin(angleInRadian);

            return new GravityMovement(tempPosition, laserDirection, inputSpeed, gravity);

        }
    }
}
