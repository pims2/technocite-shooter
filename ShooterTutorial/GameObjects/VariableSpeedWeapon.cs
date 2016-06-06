﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Xna.Framework;

namespace ShooterTutorial.GameObjects
{
    class VariableSpeedWeapon : Weapon
    {
        public VariableSpeedWeapon(Game1 game, Player player) : base(game, player)
        {
        }

        protected override void Fire(GameTime gameTime)
        {
            // Add the laer to our list.
            _game.AddLaser(VariableSpeedMovement.create(_entity.Position, -10f, -30.0f * (float)Math.PI / 180.0f), _collisionLayers);
            _game.AddLaser(VariableSpeedMovement.create(_entity.Position, 0f, 0.0f), _collisionLayers);
            _game.AddLaser(VariableSpeedMovement.create(_entity.Position, 10f, 30.0f * (float)Math.PI / 180.0f), _collisionLayers);
        }
    }
}
