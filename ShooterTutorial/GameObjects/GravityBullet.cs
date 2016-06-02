using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace ShooterTutorial.GameObjects
{
    class GravityBullet : Weapon
    {

        public GravityBullet(Game1 game, Player player) : base(game, player)
        {
            const float SECONDS_IN_MINUTE = 60f;
            const float RATE_OF_FIRE = 500f;
            _laserSpawnTime = TimeSpan.FromSeconds(SECONDS_IN_MINUTE / RATE_OF_FIRE);
        }

        protected override void Fire(GameTime gameTime)
        {
            Vector2 gravity = new Vector2(-5, 1);
            _game.AddLaser(GravityMovement.Create(_entity.Position, -10f, 0, gravity, 20));
            gravity = new Vector2(-5, -1);
            _game.AddLaser(GravityMovement.Create(_entity.Position, -10f, 0, gravity, 20));
            _game.AddLaser(LinearMovement.create(_entity.Position, -10f, 0));
        }
    }
}
