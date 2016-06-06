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
        enum State
        {
            Alive,
            Dead,
            Invincible
        }

        public Animation PlayerAnimation;

        public Vector2 Position {
            get { return _position; }
            set { _position = value; }
        }
        private Vector2 _position;

        // Amount of hit points the player has
        public int Health;

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
                // :TODO: collision with enemy lasers

                return CollisionLayer.Enemy | CollisionLayer.PowerUp;
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

        private State _state;
        private Game1 _game;

        TimeSpan _timer;

        public void Initialize(Game1 game,Animation animation, Vector2 position)
        {
            PlayerAnimation = animation;


            _position = position;
            Health = 100;
            _state = State.Alive;
            _game = game;
        }

        public void Update(GameTime gameTime)
        {
            switch (_state)
            {
                case State.Alive:
                    if (Health <= 0)
                    {
                        _timer = gameTime.TotalGameTime;
                        _game.AddExplosion(_position);
                        PlayerAnimation.Active = false;
                        _state = State.Dead;
                    }
                    PlayerAnimation.Position = _position;
                    break;
                case State.Dead:
                    if (gameTime.TotalGameTime.Seconds - _timer.Seconds >= 3 )
                    {
                        _timer = gameTime.TotalGameTime;
                        _state = State.Invincible;
                        PlayerAnimation.Active = true;
                        PlayerAnimation.Color = Color.Red;
                    }
                    break;
                case State.Invincible:
                    if (gameTime.TotalGameTime.Seconds - _timer.Seconds >= 3)
                    {
                        PlayerAnimation.Color = Color.White;
                        Health = 100;
                        _state = State.Alive;
                    }

                    PlayerAnimation.Position = _position;
                    break;
                default:
                    break;
            }
            PlayerAnimation.Update(gameTime);
        }

        public void Damage(int damage)
        {
            Health -= damage;
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
        }
    }
}
