using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Xna.Framework;

namespace ShooterTutorial.GameObjects
{
    class WaveWeapon : Weapon
    {

        public WaveWeapon(Game1 game, Player player) : base(game, player)
        {
        }

        protected override void Fire(GameTime gameTime)
        {
            _game.AddLaser(LinearMovement.create(_player.Position, 0f, 0.0f));
        }

    }

    
}
