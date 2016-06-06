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

        protected override void Fire(GameTime gameTime)
        {
            for (var i = 0; i < nbBullets; i++)
            {
                _game.AddLaser(LinearMovement.create(_entity.Position, -10f, 360.0f / nbBullets * i), _collisionLayers);
            }
        }
    }
}
