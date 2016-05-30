using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace ShooterTutorial.GameObjects
{
    class BackAndFront : Weapon
    {

        public BackAndFront(Game1 game, Player player) : base(game, player)
        {
        }

        protected override void Fire(GameTime gameTime)
        {
            _game.AddLaser(LinearMovement.create(_player.Position, -10f, 0.0f));
            _game.AddLaser(LinearMovement.create(_player.Position, -10f, 180.0f));
        }
    }
}
