using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace ShooterTutorial.GameObjects
{
    class CircularShoot : Weapon
    {
        int nbBullets = 20;

        public CircularShoot(Game1 game, Player player) : base(game, player)
        {
        }

        public override void Fire(GameTime gameTime)
        {
            // govern the rate of fire for our lasers
            if (gameTime.TotalGameTime - _previousLaserSpawnTime > _laserSpawnTime)
            {
                _previousLaserSpawnTime = gameTime.TotalGameTime;

                for (var i = 0; i < nbBullets; i++)
                {
                    _game.AddLaser(LinearMovement.create(_player.Position, -10f, 360.0f / nbBullets * i));
                }
                // Add the laer to our list.

            }

        }
    }
}
