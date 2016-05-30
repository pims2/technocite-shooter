using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace ShooterTutorial.GameObjects
{
    class StateMachine
    {
        private State _state;

        public StateMachine(State state)
        {
            _state = state;
            _state.OnEnter();
        }

        public void Update(GameTime gameTime)
        {
            _state.OnUpdate(gameTime);
        }

        public void ChangeState(State state)
        {
            // :TODO: check if changing to same state

            _state.OnExit();
            _state = state;
            _state.OnEnter();
        }
    }
}
