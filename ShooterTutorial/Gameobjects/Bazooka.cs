using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;


namespace ShooterTutorial.GameObjects
{

    class Bazooka : Weapon
    {

        public Bazooka(Game1 game, Player player) : base(game, player)
        {
        }

        public override void Fire(GameTime gameTime)
        {
            // govern the rate of fire for our lasers
            if (gameTime.TotalGameTime - _previousLaserSpawnTime > _laserSpawnTime)
            {
                _previousLaserSpawnTime = gameTime.TotalGameTime;
                Random rnd = new Random();
                // Add the laser to our list.
                _game.AddLaser(LoadingShurikenMovement.create(_player.Position, 0f, 0.0f));
                _game.AddLaser(LoadingShurikenMovement.create(_player.Position, (float)rnd.Next(-180, 0), rnd.Next(0, 40) * (float)Math.PI / 180.0f));
                _game.AddLaser(LoadingShurikenMovement.create(_player.Position, (float)rnd.Next(0, 180), rnd.Next(0, 40) * (float)Math.PI / 180.0f));


            }
        }
    }

}