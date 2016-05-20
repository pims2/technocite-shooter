using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace ShooterTutorial.GameObjects
{
    class GravityBombing : Weapon
    {
        public GravityBombing(Game1 game, Player player) : base(game,player)
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
                Vector2 gravity = new Vector2(0, 1);
                _game.AddLaser(GravityMovement.Create(_player.Position, -10f, 0, gravity, 20));
                gravity = new Vector2(0, -1);
                _game.AddLaser(GravityMovement.Create(_player.Position, -10f, 0, gravity, 20));
                _game.AddLaser(LinearMovement.create(_player.Position, -10f, 0));

                // Add the laer to our list.

            }
        }
    }
}
