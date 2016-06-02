using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ShooterTutorial.ShooterContentTypes;
using ShooterTutorial.Utilities;

namespace ShooterTutorial.GameObjects
{
    class Weapon
    {
        protected enum State
        {
            Ready,
            Fire,
            Wait
        };
   
        // govern how fast our laser can fire.
        protected TimeSpan _laserSpawnTime;
        protected TimeSpan _previousLaserSpawnTime;
        protected Game1 _game;
        protected IPositionable _entity;
        protected State _state;
        protected Boolean _itMustFire;

        public Weapon(Game1 game, IPositionable entity)
        {
            const float SECONDS_IN_MINUTE = 60f;
            const float RATE_OF_FIRE = 200f;
            _laserSpawnTime = TimeSpan.FromSeconds(SECONDS_IN_MINUTE / RATE_OF_FIRE);
            _previousLaserSpawnTime = TimeSpan.Zero;
            _game = game;
            _entity = entity;
            _state = State.Ready;
            _itMustFire = false;
        }

        public virtual void Update(GameTime gameTime)
        {
            switch (_state)
            {
                case State.Ready:
                    if (_itMustFire)
                    {
                        _state = State.Fire;
                    }
                    break;
                case State.Fire:
                    _previousLaserSpawnTime = gameTime.TotalGameTime;
                    Fire(gameTime);
                    _state = State.Wait;

                    break;
                case State.Wait:
                    // govern the rate of fire for our lasers
                    if (gameTime.TotalGameTime - _previousLaserSpawnTime > _laserSpawnTime)
                    {
                        _itMustFire = false;
                        _state = State.Ready;
                    }
                    break;
                default:
                    break;
            }
        }

        public void Shoot()
        {
            _itMustFire = true;
        }

        protected virtual void Fire(GameTime gameTime)
        {
            _game.AddLaser(LinearMovement.create(_entity.Position, 0f, 0f));
        }

        public virtual Animation GetPowerupAnimation()
        {
            var texture = _game.Content.Load<Texture2D>("Graphics\\mineAnimation");

            Animation animation = new Animation();

            // Init the animation with the correct 
            // animation information
            animation.Initialize(texture,
                Vector2.Zero,
                47,
                61,
                8,
                30,
                Color.White,
                1f,
                true);

            return animation;
        }
    }

    class AdnWeapon : Weapon
    {

        public AdnWeapon(Game1 game, IPositionable entity) : base(game, entity)
        {
        }

        protected override void Fire(GameTime gameTime)
        {
            _game.AddLaser(WaveMovement.create(_entity.Position, 0f, 10, 75));
        }
    }

}
