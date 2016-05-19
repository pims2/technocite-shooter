using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace ShooterTutorial.GameObjects
{
    class Weapon
    {
        // govern how fast our laser can fire.
        protected TimeSpan _laserSpawnTime;
        protected TimeSpan _previousLaserSpawnTime;
        protected Game1 _game;

        public Weapon(Game1 game)
        {
            const float SECONDS_IN_MINUTE = 60f;
            const float RATE_OF_FIRE = 200f;
            _laserSpawnTime = TimeSpan.FromSeconds(SECONDS_IN_MINUTE / RATE_OF_FIRE);
            _previousLaserSpawnTime = TimeSpan.Zero;
            _game = game;
        }

        public virtual void Fire(GameTime gameTime)
        {
            // govern the rate of fire for our lasers
            if (gameTime.TotalGameTime - _previousLaserSpawnTime > _laserSpawnTime)
            {
                _previousLaserSpawnTime = gameTime.TotalGameTime;

                _game.AddLaser( LinearMovement.create(_game._player.Position, 0f, 0f) );
            }

        }
    }

    class TripleWeapon : Weapon
    {

        public TripleWeapon(Game1 game) : base(game)
        {
        }

        public override void Fire(GameTime gameTime)
        {
            // govern the rate of fire for our lasers
            if (gameTime.TotalGameTime - _previousLaserSpawnTime > _laserSpawnTime)
            {
                _previousLaserSpawnTime = gameTime.TotalGameTime;

                // Add the laer to our list.
                _game.AddLaser(LinearMovement.create(_game._player.Position, -10f, -30.0f * (float)Math.PI / 180.0f));
                _game.AddLaser(LinearMovement.create(_game._player.Position, 0f, 0.0f));
                _game.AddLaser(LinearMovement.create(_game._player.Position, 10f, 30.0f * (float)Math.PI / 180.0f));
            }

        }
    }
}
