using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Xna.Framework;

namespace ShooterTutorial.GameObjects
{
    public interface IMovement
    {
        void update(GameTime time);

        Vector2 getPosition();
        float getRotation();
    }
}
