using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Input.Touch;
using ShooterTutorial.GameObjects;
using ShooterTutorial.Utilities;
using ShooterTutorial.Configuration;
using ShooterTutorial.ShooterContentTypes;
using Windows.Data.Json;
using System.IO;
using Windows.Storage;
using System.Threading.Tasks;

namespace ShooterTutorial
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Game
    {
        // A movement speed for the player.
        private const float PlayerMoveSpeed = 8;

        GraphicsDeviceManager _graphics;
        SpriteBatch _spriteBatch;
        GraphicScene _scene;
        Player _player;
        public Weapon _weapon;
        CollisionManager _collisionManager;
        WeaponManager _weaponManager;
        EnemyFactory _enemyFactory;

        Texture2D _mainBackground;
        ParallaxingBackground _bgLayer1;
        ParallaxingBackground _bgLayer2;
        Rectangle _rectBackground;
        const float Scale = 1f;


        // Keyboard states
        KeyboardState _currentKeyboardState;
        KeyboardState _prevKeyboardState;

        // Gamepad states
        GamePadState _currentGamePadState;
        GamePadState _prevGamePadState;

        // Mouse states
        MouseState _currentMouseState;
        MouseState _prevMouseState;

        // texture to hold the laser.
        Texture2D laserTexture;

        EntityList<Laser> laserBeams;


        // The rate at which enemies appear.
        TimeSpan enemySpawnTime;
        TimeSpan previousSpawnTime;

        TimeSpan powerupSpawnTime;
        TimeSpan previousPowerupSpawnTime;
        float powerupMinSpawnTime = 2.0f;
        float powerupMaxSpawnTime = 8.0f;

        //Enemies
        Texture2D enemyTexture;
        EntityList<Enemy> enemies;

        Powerup powerup;

        // a random number gen
        Random random;

        // Collections of explosions
        EntityList<Explosion> explosions;

        //Texture to hold explosion animation.
        Texture2D explosionTexture;

        ConfigurationTable<int> test = ConfigurationManager.createTable<int>( "test", "table test" );

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            _scene = new GraphicScene();
            _collisionManager = new CollisionManager();
            _weaponManager = new WeaponManager();
            _enemyFactory = new EnemyFactory();

            _player = new Player();
            _scene.Add(_player);
            _collisionManager.Add(_player);
            _weapon = new Weapon(this, _player);

            _bgLayer1 = new ParallaxingBackground();
            _bgLayer2 = new ParallaxingBackground();
            _rectBackground = new Rectangle(0, 0, GraphicsDevice.Viewport.Width, GraphicsDevice.Viewport.Height);

            TouchPanel.EnabledGestures = GestureType.FreeDrag;

            // init our laser
            laserBeams = new EntityList<Laser>(_scene, _collisionManager);

            // Initialize the enemies list
            enemies = new EntityList<Enemy>(_scene, _collisionManager);

            //used to determine how fast the enemies will respawn.
            enemySpawnTime = TimeSpan.FromSeconds(1.0f);

            // init our random number generator
            random = new Random();

            explosions = new EntityList<Explosion>(_scene, _collisionManager);
            
            base.Initialize();

            ConfigurationManager.logConfiguration();

            var task2 = readJson("enemies");
            task2.ContinueWith( task =>
           {
               var content = task.Result;

               try
               {
                   JsonArray config = (JsonArray)JsonArray.Parse(content);
                   _enemyFactory.Initialize(config);
               }
               catch (Exception e)
               {

               }
           });
        }

        private async Task<string> readJson( string file_path )
        {
            var folder = Windows.ApplicationModel.Package.Current.InstalledLocation;
            var file = await folder.GetFileAsync("Content\\Config\\" + file_path + ".json");
            var contents = await Windows.Storage.FileIO.ReadTextAsync(file);

            return contents;
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            var task = readJson( "global" );
            task.Wait();
            var content = task.Result;

            try
            {
                JsonObject config = (JsonObject)JsonObject.Parse(content);
                ConfigurationManager.LoadConfiguration(config);
            }
            catch(Exception e)
            {
                
            }

            // Create a new SpriteBatch, which can be used to draw textures.
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            // Load the player resources
            Rectangle titleSafeArea = GraphicsDevice.Viewport.TitleSafeArea;    
            var playerPosition = new Vector2(titleSafeArea.X, titleSafeArea.Y + titleSafeArea.Height/2);
            
            Texture2D playerTexture = Content.Load<Texture2D>("Graphics\\shipAnimation");
            Animation playerAnimation = new Animation();
            playerAnimation.Initialize(playerTexture, playerPosition, 115, 69, 8, 30, Color.White, 1, true);
            
            _player.Initialize(this, playerAnimation, playerPosition);

            // Load the background.
            _bgLayer1.Initialize(Content, "Graphics/bgLayer1", GraphicsDevice.Viewport.Width, GraphicsDevice.Viewport.Height, -1);
            _bgLayer2.Initialize(Content, "Graphics/bgLayer2", GraphicsDevice.Viewport.Width, GraphicsDevice.Viewport.Height, -2);
            _mainBackground = Content.Load<Texture2D>("Graphics/mainbackground");

            // load the enemy texture.
            enemyTexture = Content.Load<Texture2D>("Graphics\\mineAnimation");

            // load th texture to serve as the laser.
            laserTexture = Content.Load<Texture2D>("Graphics\\laser");

            // Load the exploision sprite strip
            explosionTexture = Content.Load<Texture2D>("Graphics\\explosion");

        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            // User inputs.
            // Save the previous state of the keyboard and game pad so we can determine single key/button presses
            _prevGamePadState = _currentGamePadState;
            _prevKeyboardState = _currentKeyboardState;
            _prevMouseState = _currentMouseState;
            // Read the current state of the keyboard and gamepad and store it.
            _currentKeyboardState = Keyboard.GetState();
            _currentGamePadState = GamePad.GetState(PlayerIndex.One);
            _currentMouseState = Mouse.GetState();
            
            UpdatePlayer(gameTime);
            _bgLayer1.Update(gameTime);
            _bgLayer2.Update(gameTime);

            _weapon.Update(gameTime);

            // update lasers
            UpdateLasers(gameTime);

            // update the enemies
            UpdateEnemies(gameTime);

            UpdatePowerup(gameTime);

            // update collisons
            _collisionManager.Update();

            UpdateExplosions(gameTime);

            base.Update(gameTime);
        }

        private void UpdateExplosions(GameTime gameTime)
        {
            explosions.Update(this, gameTime);
        }

        void UpdatePlayer(GameTime gameTime)
        {
            _player.Update(gameTime);

            // Touch inputs
            while (TouchPanel.IsGestureAvailable)
            {
                GestureSample gesture = TouchPanel.ReadGesture();
                
                if (gesture.GestureType == GestureType.FreeDrag)
                    _player.Position += gesture.Delta;
            }

            // Get Mouse State
            Vector2 mousePosition = new Vector2(_currentMouseState.X, _currentMouseState.Y);
            if (_currentMouseState.LeftButton == ButtonState.Pressed)
            {
                Vector2 posDelta = mousePosition - _player.Position;
                posDelta.Normalize();
                posDelta = posDelta * PlayerMoveSpeed;
                _player.Position = _player.Position + posDelta;
            }


            // Thumbstick controls
            _player.Position += _currentGamePadState.ThumbSticks.Left * PlayerMoveSpeed;

            Vector2 movement = new Vector2();

            // Keyboard/DPad
            if (_currentKeyboardState.IsKeyDown(Keys.Left) || _currentGamePadState.DPad.Left == ButtonState.Pressed)
            {
                movement.X -= +PlayerMoveSpeed;
            }
            if (_currentKeyboardState.IsKeyDown(Keys.Right) || _currentGamePadState.DPad.Right == ButtonState.Pressed)
            {
                movement.X += PlayerMoveSpeed;
            }

            if (_currentKeyboardState.IsKeyDown(Keys.Up) || _currentGamePadState.DPad.Up == ButtonState.Pressed)
            {
                movement.Y -= PlayerMoveSpeed;
            }
            if (_currentKeyboardState.IsKeyDown(Keys.Down) || _currentGamePadState.DPad.Down == ButtonState.Pressed)
            {
                movement.Y += PlayerMoveSpeed;
            }

            if (_currentKeyboardState.IsKeyDown(Keys.Space) || _currentGamePadState.Buttons.X == ButtonState.Pressed)
            {
                _weapon.Shoot();
            }

            if (_currentKeyboardState.IsKeyDown(Keys.NumPad1))
            {
                _weapon = new Weapon(this, _player);
            }

            if (_currentKeyboardState.IsKeyDown(Keys.NumPad2))
            {
                _weapon = new TripleWeapon(this, _player);
            }

            if (_currentKeyboardState.IsKeyDown(Keys.NumPad3))
            {
                _weapon = new AdnWeapon(this, _player);
            }

            if (_currentKeyboardState.IsKeyDown(Keys.NumPad4))
            {
                _weapon = new WaveWeapon(this, _player);
            }

            Vector2 final_player_position = new Vector2();
            final_player_position = _player.Position + movement;

            // Make sure that the player does not go out of bounds
            final_player_position.X = MathHelper.Clamp(final_player_position.X, 0, GraphicsDevice.Viewport.Width - _player.Width);
            final_player_position.Y = MathHelper.Clamp(final_player_position.Y, 0, GraphicsDevice.Viewport.Height - _player.Height);

            _player.Position = final_player_position;
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here
            // Start drawing
            _spriteBatch.Begin();

            // Draw background.
            _spriteBatch.Draw(_mainBackground, _rectBackground, Color.White);
            _bgLayer1.Draw(_spriteBatch);
            _bgLayer2.Draw(_spriteBatch);

            _scene.Draw(_spriteBatch);

            // Stop drawing
            _spriteBatch.End(); 


            base.Draw(gameTime);
        }
        
        protected void UpdateLasers(GameTime gameTime)
        {
            laserBeams.Update(this, gameTime);
        }

        public void AddLaser(IMovement movement, CollisionLayer collision_layers)
        {
            Animation laserAnimation = new Animation();

            // initlize the laser animation
            laserAnimation.Initialize(laserTexture,
                _player.Position,
                46,
                16,
                1,
                30,
                Color.White,
                1f,
                true);
         
            laserBeams.Add().Initialize(this, laserAnimation, movement, collision_layers);
            
            /* todo: add code to create a laser. */
            //laserSoundInstance.Play();
        }

        protected void UpdateEnemies(GameTime gameTime)
        {
            // spawn a new enemy every 1.5 seconds.
            if (gameTime.TotalGameTime - previousSpawnTime > enemySpawnTime)
            {
                previousSpawnTime = gameTime.TotalGameTime;

                // add an enemy
                AddEnemy();
            }

            enemies.Update(this, gameTime);
        }

        protected void UpdatePowerup(GameTime gameTime)
        {
            if (powerup != null)
            {
                powerup.Update(gameTime);

                if (!powerup.Active)
                {
                    _collisionManager.Remove(powerup);
                    _scene.Remove(powerup);
                    powerup = null;
                    previousPowerupSpawnTime = gameTime.TotalGameTime;


                    powerupSpawnTime =TimeSpan.FromSeconds(powerupMinSpawnTime + ( powerupMaxSpawnTime - powerupMinSpawnTime ) * random.NextDouble());
                }
            }
            else
            {
                if (gameTime.TotalGameTime - previousPowerupSpawnTime > powerupSpawnTime)
                {
                    previousPowerupSpawnTime = gameTime.TotalGameTime;

                    // randomly generate the postion of the enemy
                    Vector2 position = new Vector2(
                        random.Next(100, GraphicsDevice.Viewport.Width - 100),
                        random.Next(100, GraphicsDevice.Viewport.Height - 100)
                        );


                    var weapon = _weaponManager.GetRandomWeapon( this, _player);

                    powerup = new Powerup(weapon.GetPowerupAnimation(), position, weapon);
                    _collisionManager.Add(powerup);
                    _scene.Add(powerup);
                }
            }

        }

        protected void AddEnemy()
        {
            // create the animation object
            Animation enemyAnimation = new Animation();

            // Init the animation with the correct 
            // animation information
            enemyAnimation.Initialize(enemyTexture,
                Vector2.Zero,
                47,
                61,
                8,
                30,
                Color.White,
                1f,
                true);

            // randomly generate the postion of the enemy
            Vector2 position = new Vector2(
                GraphicsDevice.Viewport.Width + enemyTexture.Width / 2,
                random.Next(100, GraphicsDevice.Viewport.Height - 100));

            // create an enemy
            Enemy enemy = new Enemy();

            var m = new LinearMovement(position, new Vector2(-1f, 0f));
            m.MoveSpeed = 10f;

            // Add the enemy to the active enemies list
            enemies.Add().Initialize(this, enemyAnimation, m);

        }

        public void AddExplosion(Vector2 enemyPosition)
        {
            Animation explosionAnimation = new Animation();

            explosionAnimation.Initialize(explosionTexture,
                enemyPosition,
                134,
                134,
                12,
                30,
                Color.White,
                1.0f,
                true);

            explosions.Add().Initialize(explosionAnimation, enemyPosition);
        }
    }
}
    