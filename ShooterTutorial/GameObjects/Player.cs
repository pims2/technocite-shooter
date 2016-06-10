using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ShooterTutorial.Utilities;

namespace ShooterTutorial.GameObjects
{
    public class Player :IDrawable2, IPositionable, ICollidable
    {
        private delegate void StateDelegate(GameTime gameTime);
        public delegate void OnHealthModifiedDelegate(int health);
        public OnHealthModifiedDelegate OnHealthModified { get; set; }

        public Animation PlayerAnimation;

        public Vector2 Position {
            get { return _position; }
            set { _position = value; }
        }
        private Vector2 _position;

        // Amount of hit points the player has
        private int _health;
        public int Health { get { return _health; } set { _health = value; OnHealthModified(_health); } }

        public int Layer
        {
            get { return 2; }
        }

        public int Width
        { get { return PlayerAnimation.FrameWidth; } }
        
        public int Height
        { get { return PlayerAnimation.FrameHeight; } }

        public CollisionLayer CollisionGroup
        {
            get
            {
                return CollisionLayer.Player;
            }
        }

        public CollisionLayer CollisionLayers
        {
            get
            {
                return CollisionLayer.Enemy | CollisionLayer.PowerUp | CollisionLayer.Laser;
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

        private StateDelegate _stateDelegate;

        private Game1 _game;

        TimeSpan _timer;

        public void Initialize(Game1 game,Animation animation, Vector2 position)
        {
            PlayerAnimation = animation;


            _position = position;
            Health = 100;
            _stateDelegate = StateAliveUpdate;
            _game = game;
        }

        private void StateAliveUpdate(GameTime gameTime)
        {
            if (Health <= 0)
            {
                _timer = gameTime.TotalGameTime;
                _game.AddExplosion(_position);
                PlayerAnimation.Active = false;
                _stateDelegate = StateDeadUpdate;
            }
            PlayerAnimation.Position = _position;
        }

        public void StateDeadUpdate(GameTime gameTime)
        {
            if (gameTime.TotalGameTime.Seconds - _timer.Seconds >= 3)
            {
                _timer = gameTime.TotalGameTime;
                _stateDelegate = StateInvincibleUpdate;
                PlayerAnimation.Active = true;
                PlayerAnimation.Color = Color.Red;
            }
        }


        public void StateInvincibleUpdate(GameTime gameTime)
        {
            if (gameTime.TotalGameTime.Seconds - _timer.Seconds >= 3)
            {
                PlayerAnimation.Color = Color.White;
                Health = 100;
                _stateDelegate = StateAliveUpdate;
            }

            PlayerAnimation.Position = _position;
        }

        public void Update(GameTime gameTime)
        {
            _stateDelegate(gameTime);
            PlayerAnimation.Update(gameTime);
        }

        public void Damage(int damage)
        {
            Health -= damage;
            Health = Math.Max(0, Health);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            PlayerAnimation.Draw(spriteBatch);
        }

        public void OnCollision(ICollidable other)
        {
            if (other.CollisionGroup == CollisionLayer.Enemy)
            {
                Damage(((Enemy) other).Damage);
            }
            else if (other.CollisionGroup == CollisionLayer.PowerUp)
            {
                _game._weapon = ((Powerup)other).Weapon;
            }
            else if (other.CollisionGroup == CollisionLayer.Laser)
            {
                if ((other.CollisionLayers & CollisionLayer.Player) != 0)
                {
                    Damage(10);
                }
            }
        }
    }
}
