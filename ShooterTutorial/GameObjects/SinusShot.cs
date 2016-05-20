using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace ShooterTutorial.GameObjects
{
    class SinusShot : Weapon
    {

        public SinusShot(Game1 game, Player player) : base(game, player)
        {
            const float SECONDS_IN_MINUTE = 60f;
            const float RATE_OF_FIRE = 500f;
            _laserSpawnTime = TimeSpan.FromSeconds(SECONDS_IN_MINUTE / RATE_OF_FIRE);
        }

        public override void Fire(GameTime gameTime)
        {
            // govern the rate of fire for our lasers
            if (gameTime.TotalGameTime - _previousLaserSpawnTime > _laserSpawnTime)
            {
                _previousLaserSpawnTime = gameTime.TotalGameTime;

                _game.AddLaser(SinusoidaleMovement.Create(_player.Position, -10f, -10, 20));
                _game.AddLaser(SinusoidaleMovement.Create(_player.Position, -10f, 10, 20));
                // Add the laer to our list.

            }

        }
    }
}
