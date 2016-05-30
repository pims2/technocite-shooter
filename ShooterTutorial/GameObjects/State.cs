using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace ShooterTutorial.GameObjects
{
    abstract class State
    {
        public virtual void OnEnter()
        {
        }

        public virtual void OnUpdate(GameTime gameTime)
        {
        }

        public virtual void OnExit()
        {
        }
    }
}
