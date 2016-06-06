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

        protected override void Fire(GameTime gameTime)
        {
            _game.AddLaser(ghaMovement.create(_entity.Position, -10f, -30.0f * (float)Math.PI / 180.0f, 0), _collisionLayers);
            _game.AddLaser(ghaMovement.create(_entity.Position, 0f, 0f, 0), _collisionLayers);
            _game.AddLaser(ghaMovement.create(_entity.Position, 10f, 30.0f * (float)Math.PI / 180.0f, 0), _collisionLayers);
        }
    }
}
