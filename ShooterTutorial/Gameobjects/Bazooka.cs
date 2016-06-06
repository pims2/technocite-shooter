using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;


namespace ShooterTutorial.GameObjects
{
    [WeaponDefinition]
    class Bazooka : Weapon
    {

        public Bazooka(Game1 game, Player player) : base(game, player)
        {
        }

        protected override void Fire(GameTime gameTime)
        {
            Random rnd = new Random();
            // Add the laser to our list.
            _game.AddLaser(LoadingShurikenMovement.create(_entity.Position, 0f, 0.0f), _collisionLayers);
            _game.AddLaser(LoadingShurikenMovement.create(_entity.Position, (float)rnd.Next(-180, 0), rnd.Next(0, 40) * (float)Math.PI / 180.0f), _collisionLayers);
            _game.AddLaser(LoadingShurikenMovement.create(_entity.Position, (float)rnd.Next(0, 180), rnd.Next(0, 40) * (float)Math.PI / 180.0f), _collisionLayers);
        }

        public override Animation GetPowerupAnimation()
        {
            var texture = _game.Content.Load<Texture2D>("Graphics\\explosion");

            Animation animation = new Animation();

            // Init the animation with the correct 
            // animation information
            animation.Initialize(texture,
                Vector2.Zero,
                134,
                134,
                12,
                30,
                Color.White,
                1.0f,
                true);

            return animation;
        }
    }

}