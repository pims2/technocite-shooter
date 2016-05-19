using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace ShooterTutorial.GameObjects
{
    class TripleWeaponRotateGHA : Weapon
    {
        public TripleWeaponRotateGHA(Game1 game, Player player) : base(game, player)
        {
        }

        public override void Fire(GameTime gameTime)
        {
            // govern the rate of fire for our lasers
            if (gameTime.TotalGameTime - _previousLaserSpawnTime > _laserSpawnTime)
            {
                _previousLaserSpawnTime = gameTime.TotalGameTime;

                // Add the laer to our list.
                _game.AddLaser(ghaMovement.create(_player.Position, -10f, -30.0f * (float)Math.PI / 180.0f, 0));
                _game.AddLaser(ghaMovement.create(_player.Position, 0f, 0f, 0));
                _game.AddLaser(ghaMovement.create(_player.Position, 10f, 30.0f * (float)Math.PI / 180.0f, 0));
            }

        }
    }
}
