using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
namespace ShooterTutorial.GameObjects
{
    class SinusoidaleMovement : IMovement
    {
        private Vector2 position, directionOfMove;
        private float angle;
        private float speed;

        private double sinusAngle;

        public SinusoidaleMovement(Vector2 positionOfStart, Vector2 direction, float speedOfMove)
        {
            position = positionOfStart;
            directionOfMove = direction;
            speed = speedOfMove;
        }

        public void update(GameTime time)
        {
            sinusAngle++;
            sinusAngle *= Math.PI / 180;  
            //position += speed * directionOfMove*;
            position.X += speed * directionOfMove.X;
            position.Y += speed * directionOfMove.Y * (float)Math.Sin(sinusAngle);
        }
        public Vector2 GetPosition()
        {
            return position;
        }
        public float GetRotation()
        {
            return angle;
        }
        public static linearMovement Create(Vector2 position, float offsetY, float angle, float inputSpeed = 30)
        {

            Vector2 tempPosition = new Vector2(position.X + 30, position.Y + 30 + offsetY);
            float angleInRadian = angle * (float)Math.PI / 180.0f;

            Vector2 laserDirection = new Vector2();

            laserDirection.X = (float)Math.Cos(angleInRadian);
            laserDirection.Y = (float)Math.Sin(angleInRadian);

            return new linearMovement(tempPosition, laserDirection, inputSpeed);
        }
    }
}
