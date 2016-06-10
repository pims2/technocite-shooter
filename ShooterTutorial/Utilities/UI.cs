using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ShooterTutorial.GameObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShooterTutorial.Utilities
{
    public class UI
    {
        SpriteFont _font;
        string _healthMessage;

        public void Initialize(Game game, Player player)
        {
            _font = game.Content.Load<SpriteFont>("Graphics/gameFont");
            player.OnHealthModified += UpdateHealthMessage;
            UpdateHealthMessage(player.Health);
        }

        public void UpdateHealthMessage(int health)
        {
            _healthMessage = "Health: " + health;
        }

        public void Draw(SpriteBatch sprite_batch)
        {
            sprite_batch.DrawString(_font, _healthMessage, new Vector2(100, 100), Color.White);
        }
    }
}
