using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ShooterTutorial.Utilities;
using ShooterTutorial;


namespace ShooterTutorial.GameObjects
{
    public class Enemy : IUpdateable2, IDrawable2
    {
        private StateMachine _stateMachine;

        // animation represneting the enemy.
        public Animation EnemyAnimation;

        // The postion of the enemy ship relative to the 
        // top of left corner of the screen
        public Vector2 Position;

        public IMovement Movement;

        public int Layer
        {
            get { return 2; }
        }

        // state of the enemy ship
        public bool Active
        {
            get { return _Active; }
        }

        private bool _Active;

        // Hit points of the enemy, if this goes
        // to zero the enemy dies.      
        public int Health;

        // the amount of damage that the enemy
        // ship inflicts on the player.
        public int Damage;

        // the amount of the score enemy is worth.
        public int Value;

        // Get the width of the enemy ship
        public int Width
        {
            get { return EnemyAnimation.FrameWidth; }
        }

        // Get the height of the enemy ship.
        public int Height
        {
            get { return EnemyAnimation.FrameHeight; }
        }

        private Game1 _game;

        public void Initialize(
            Game1 game,
            Animation animation,
            IMovement movement)
        {
            _game = game;

            // load the enemy ship texture;
            EnemyAnimation = animation;

            Movement = movement;

            // set the postion of th enemy ship
            Position = movement.getPosition();

            // set the enemy to be active
            _Active = true;

            // set the health of the enemy
            Health = 100;

            // Set the amount of damage the enemy does
            Damage = 10;

            // Set how fast the enemy moves.
            //            enemyMoveSpeed = 10;

            // set the value of the enemy
            Value = 100;

            _stateMachine = new StateMachine(new Phase1State(this));
        }

        public void Update(Game game, GameTime gameTime)
        {
            if (Health <= 0)
            {
                _stateMachine.ChangeState(new DeadState(this));
            }

            _stateMachine.Update(gameTime);

            // the enemy always moves to the left.

            Movement.update(gameTime);

            Position = Movement.getPosition();

            // Update the postion of the anaimation
            EnemyAnimation.Position = Position;

            // Update the animation;
            EnemyAnimation.Update(gameTime);

            /* If the enenmy is past the screen or its
             * health reaches 0 then deactivate it. */
            if (Position.X < -Width)
            {
                //deactivate the enemy
                _Active = false;

            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            // draw the animation
            EnemyAnimation.Draw(spriteBatch, Movement.getRotation());
        }

        public void DealDamage(int damage)
        {
            Health -= damage;
        }

        class Phase1State : State
        {
            private Enemy _enemy;

            public Phase1State(Enemy enemy)
            {
                _enemy = enemy;
            }

            public override void OnEnter()
            {
                _enemy.EnemyAnimation.Color = Color.Green;
            }

            public override void OnUpdate(GameTime gameTime)
            {
                if (_enemy.Health <= 50)
                {
                    _enemy._stateMachine.ChangeState(new Phase2State(_enemy));
                }
            }
        }

        class Phase2State : State
        {
            private Enemy _enemy;

            public Phase2State(Enemy enemy)
            {
                _enemy = enemy;
            }

            public override void OnEnter()
            {
                _enemy.EnemyAnimation.Color = Color.Red;
            }

            public override void OnUpdate(GameTime gameTime)
            {
            }
        }

        class DeadState : State
        {
            private Enemy _enemy;

            public DeadState(Enemy enemy)
            {
                _enemy = enemy;
            }

            public override void OnEnter()
            {
                _enemy._game.AddExplosion(_enemy.Position);
                _enemy._Active = false;
            }
        }
    }
}
