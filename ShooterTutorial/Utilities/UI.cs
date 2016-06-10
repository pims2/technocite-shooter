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
        Color _textColor;

        async void resetColor()
        {
            await Task.Delay(1000);
            _textColor = Color.White;
        }

        public void Initialize(Game game, Player player, CollisionManager collision_manager)
        {
            _textColor = Color.White;
            _font = game.Content.Load<SpriteFont>("Graphics/gameFont");
            player.OnHealthModified += UpdateHealthMessage;
            UpdateHealthMessage(player.Health);
            collision_manager.OnCollision += (s, e) => {
                if (
                    e.first.CollisionGroup == CollisionLayer.Enemy
                    || e.second.CollisionGroup == CollisionLayer.Enemy
                    )
                {
                    _textColor = Color.Green;
                    resetColor();
                }
            };
        }

        public void UpdateHealthMessage(int health)
        {
            _healthMessage = "Health: " + health;
        }

        public void Draw(SpriteBatch sprite_batch)
        {
            sprite_batch.DrawString(_font, _healthMessage, new Vector2(100, 100), _textColor);
        }
    }
}
