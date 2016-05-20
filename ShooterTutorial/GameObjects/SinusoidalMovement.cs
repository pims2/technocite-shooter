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

        private double amplitude;
        private double timer;

        public SinusoidaleMovement(Vector2 positionOfStart, Vector2 direction, float speedOfMove)
        {
            position = positionOfStart;
            directionOfMove = direction;
            speed = speedOfMove;
            timer = 0;
            
        }

        public void update(GameTime time)
        {
            timer += 10*time.ElapsedGameTime.TotalSeconds;
            System.Diagnostics.Debug.WriteLine(amplitude);
            position.X += speed * directionOfMove.X;
            position.Y += speed *directionOfMove.Y + 15*((float)Math.Sin(timer));
        }
        public Vector2 getPosition()
        {
            return position;
        }
        public float getRotation()
        {
            return angle;
        }
        public static SinusoidaleMovement Create(Vector2 position, float offsetY, float angle, float inputSpeed = 30)
        {

            Vector2 tempPosition = new Vector2(position.X + 30, position.Y + 30 + offsetY);
            float angleInRadian = angle * (float)Math.PI / 180.0f;
            
            Vector2 laserDirection = new Vector2();

            laserDirection.X = (float)Math.Cos(angleInRadian);
            laserDirection.Y = (float)Math.Sin(angleInRadian);

            return new SinusoidaleMovement(tempPosition, laserDirection, inputSpeed);
        }
    }
}
