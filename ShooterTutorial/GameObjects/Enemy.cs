using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ShooterTutorial.Utilities;
using ShooterTutorial.Configuration;
using ShooterTutorial;


namespace ShooterTutorial.GameObjects
{
    public class Enemy : IUpdateable2, IDrawable2, IPositionable, ICollidable
    {
        private StateMachine _stateMachine;

        // animation represneting the enemy.
        public Animation EnemyAnimation;

        // The postion of the enemy ship relative to the 
        // top of left corner of the screen
        public Vector2 Position {
            get { return _position; }
            set { _position = value; }
        }
        private Vector2 _position;

        private Weapon _weapon;

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

        public CollisionLayer CollisionGroup
        {
            get
            {
                return CollisionLayer.Enemy;
            }
        }

        public CollisionLayer CollisionLayers
        {
            get
            {
                return CollisionLayer.Laser | CollisionLayer.Player;
            }
        }

        public Rectangle BoundingRectangle
        {
            get
            {
                return new Rectangle(
                        (int)Position.X,
                        (int)Position.Y,
                        Width,
                        Height);
            }
        }

        private Game1 _game;

        //private static ConfigurationValue<int> InitialHealth = ConfigurationManager.create( "enemy.Health", 3, "Enemy health at init" );

        [Configuration.Configuration("enemy.Health", Description = "Enemy health at init")]
        private static int InitialHealth = 3;

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
            _position = movement.getPosition();
            _weapon = new Weapon(game, this, (float)Math.PI);
            _weapon.CollisionLayers = CollisionLayer.Player | CollisionLayer.Laser;

            // set the enemy to be active
            _Active = true;

            // set the health of the enemy
            Health = InitialHealth;

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

            _position = Movement.getPosition();

            // Update the postion of the anaimation
            EnemyAnimation.Position = _position;

            // Update the animation;
            EnemyAnimation.Update(gameTime);

            /* If the enenmy is past the screen or its
             * health reaches 0 then deactivate it. */
            if (_position.X < -Width)
            {
                //deactivate the enemy
                _Active = false;

            }

            _weapon.Shoot();
            _weapon.Update(gameTime);
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

        public void OnCollision(ICollidable other)
        {
            if (other.CollisionGroup == CollisionLayer.Laser)
            {
                if ((other.CollisionLayers & CollisionLayer.Enemy) != 0)
                {
                    DealDamage(1);
                }
            }
            else if (other.CollisionGroup == CollisionLayer.Player)
            {
                DealDamage(10);
            }
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
                if (_enemy.Health <= 2)
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
                _enemy.Movement = SinusoidaleMovement.Create(_enemy.Position, 0, 180f, 10f);
            }

            public override void OnUpdate(GameTime gameTime)
            {
                if (_enemy.Health <= 1)
                {
                    _enemy._stateMachine.ChangeState(new Phase3State(_enemy));
                }
            }
        }

        class Phase3State : State
        {
            private Enemy _enemy;

            public Phase3State(Enemy enemy)
            {
                _enemy = enemy;
            }

            public override void OnEnter()
            {
                _enemy.EnemyAnimation.Color = Color.White;
                _enemy.Movement = LinearMovement.create(_enemy.Position, 0, 0, 5f);
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
