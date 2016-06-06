using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShooterTutorial.Utilities
{
    public enum CollisionLayer
    {
        Player = 1,
        PowerUp = 2,
        Enemy = 4,
        Laser = 8
    };

    public interface ICollidable
    {
        CollisionLayer CollisionGroup { get; }
        CollisionLayer CollisionLayers { get; }
        Rectangle BoundingRectangle { get; }
        void OnCollision(ICollidable other);
    }
}
