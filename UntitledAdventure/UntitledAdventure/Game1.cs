using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

// NOTES!
// On conflish errors go to GIT BASH/CMD
// "cd" to the GIT folder Document > JannasAdventure
// Then type "git pull"
// Then "git stash"
// Then "git pull" again

namespace UntitledAdventure
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        SpriteFont font;
        Camera2D camera;
        Matrix viewMatix;

        private Texture2D menuBackground, background1, background2;
        private Player player;
        private Button butt1, butt2;
        List<Enemy> enemies = new List<Enemy>();

        GameStates state;
        enum GameStates
        {
            Menu,
            Loading,
            Playing,
            Paused,
            Map
        };

        private static int sW = 800; // Get Screen Width
        private static int sH = 600; // Get Screen Height

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
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
            state = GameStates.Menu;
            camera = new Camera2D(GraphicsDevice.Viewport);
            font = Content.Load<SpriteFont>("my_font");

            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            graphics.PreferredBackBufferHeight = sH;
            graphics.PreferredBackBufferWidth = sW;
            graphics.ApplyChanges();

            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            switch (state)
            {
                case GameStates.Menu:
                    // If we have a menu image
                    IsMouseVisible = true;
                    menuBackground = Content.Load<Texture2D>("menu_background");
                    butt1 = new Button(Content.Load<Texture2D>("start_button"), 400, 300);
                    butt2 = new Button(Content.Load<Texture2D>("exit_button"), 400, 400);
                    break;

                case GameStates.Loading:
                    // If we have a background image
                    IsMouseVisible = false;
                    break;

                case GameStates.Playing:
                    // player.x and player.y will obviously need tweaking once game becomes... well a game lol
                    background1 = Content.Load<Texture2D>("Background");
                    background2 = Content.Load<Texture2D>("Background");
                    player = new Player(Content.Load<Texture2D>("Player_Test"), background1.Height, background1.Width); // Player X & Y made in object
                    camera.Position = new Vector2((player.x - (sW / 2)), (player.y - (sH / 2)));
                    enemies.Add(new Enemy(Content.Load<Texture2D>("Enemy_Test"), font, 650, 500));
                    enemies.Add(new Enemy(Content.Load<Texture2D>("Enemy_Test"), font, 350, 500));
                    enemies.Add(new Enemy(Content.Load<Texture2D>("Enemy_Test"), font, 500, 650));
                    enemies.Add(new Enemy(Content.Load<Texture2D>("Enemy_Test"), font, 500, 350));
                    break;
            }
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// game-specific content.
        /// </summary>
        protected override void UnloadContent()
        {
            switch (state)
            {
                case GameStates.Menu:
                    // No sure fire way to unload/delete objects in C#
                    menuBackground.Dispose();
                    break;

                case GameStates.Loading:
                    break;

                case GameStates.Playing:
                    background1.Dispose();
                    break;
            }
            
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            switch (state)
            {
                case GameStates.Menu:
                    menuUpdate(gameTime);
                    break;

                case GameStates.Playing:
                    playingUpdate(gameTime);
                    break;

                case GameStates.Paused:
                    // call pausedUpdate(gameTime);
                    break;

                case GameStates.Map:
                    // call pausedUpdate(gameTime);
                    break;
            }

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            viewMatix = camera.GetViewMatrix();

            switch (state)
            {
                case GameStates.Menu:
                    spriteBatch.Begin();
                    spriteBatch.Draw(menuBackground, new Rectangle(0, 0, menuBackground.Width, menuBackground.Height), Color.GhostWhite);
                    butt1.draw(spriteBatch);
                    butt2.draw(spriteBatch);
                    break;

                case GameStates.Loading:
                    spriteBatch.Begin();
                    spriteBatch.Draw(menuBackground, new Rectangle(0, 0, menuBackground.Width, menuBackground.Height), Color.GhostWhite);
                    break;

                case GameStates.Playing:
                    // Typical Order:
                    //  1. Background/Screen            - Done in this class
                    //  2. Objects/Enemies              - Classes store in this class array
                    //  3. Player/Character             - Own object, stored in variable
                    //  4. UI/Map/Pause Menu/etc...     - Set, done in this class (Method perhaps)
                    spriteBatch.Begin(transformMatrix: viewMatix);
                    spriteBatch.Draw(background1, new Rectangle(0, 0, background1.Width, background1.Height), Color.GhostWhite);
                    spriteBatch.Draw(background2, new Rectangle(background1.Width, 0, background2.Width, background2.Height), Color.GhostWhite);
                    spriteBatch.DrawString(font, "BETA!", new Vector2((sW / 2), (sH / 2)), Color.MediumPurple);
                    for (int i = 0; i < enemies.Count; i++)
                    {
                        enemies[i].draw(spriteBatch);
                    }

                    player.draw(spriteBatch);
                    break;

                case GameStates.Paused:
                    spriteBatch.Begin(transformMatrix: viewMatix);
                    spriteBatch.Draw(background1, new Rectangle(0, 0, background1.Width, background1.Height), Color.GhostWhite);
                    spriteBatch.DrawString(font, "BETA!", new Vector2((sW / 2), (sH / 2)), Color.Purple);
                    for (int i = 0; i < enemies.Count; i++)
                    {
                        enemies[i].draw(spriteBatch);
                    }

                    // Pause menu here - trick here is to stop calling the Update and just read in mouse click positions...
                    player.draw(spriteBatch);
                    break;
            }

            spriteBatch.End();
            base.Draw(gameTime);
        }

        private void menuUpdate(GameTime gameTime)
        {
            if (Mouse.GetState().LeftButton == ButtonState.Pressed)
            {
                if (butt1.collision(Mouse.GetState().X, Mouse.GetState().Y, 1, 1))
                {
                    state = GameStates.Loading;
                    LoadContent();
                    Draw(gameTime);
                    state = GameStates.Playing;
                    LoadContent();
                }
                else if (butt2.collision(Mouse.GetState().X, Mouse.GetState().Y, 1, 1))
                {
                    Exit();
                }
            }
        }

        private void playingUpdate(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
            {
                // Change state to Pause!
                Exit();
            }

            // Camera movement
            var deltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;

            //// Rotation
            //if (Keyboard.GetState().IsKeyDown(Keys.Q))
            //{
            //    camera.Rotation -= deltaTime;
            //}

            //if (Keyboard.GetState().IsKeyDown(Keys.E))
            //{
            //    camera.Rotation += deltaTime;
            //}

            // Movement
            if (Keyboard.GetState().IsKeyDown(Keys.W))
            {
                camera.Position -= new Vector2(0, 120) * deltaTime;
                player.y -= 2;
                player.state = Player.PlayerStates.North;

                if (Keyboard.GetState().IsKeyDown(Keys.A))
                {
                    player.state = Player.PlayerStates.NorthWest;
                }
                else if (Keyboard.GetState().IsKeyDown(Keys.D))
                {
                    player.state = Player.PlayerStates.NorthEast;
                }
            }

            if (Keyboard.GetState().IsKeyDown(Keys.S))
            {
                camera.Position += new Vector2(0, 120) * deltaTime;
                player.y += 2;
                player.state = Player.PlayerStates.South;

                if (Keyboard.GetState().IsKeyDown(Keys.A))
                {
                    player.state = Player.PlayerStates.SouthWest;
                }
                else if (Keyboard.GetState().IsKeyDown(Keys.D))
                {
                    player.state = Player.PlayerStates.SouthEast;
                }
            }

            if (Keyboard.GetState().IsKeyDown(Keys.A))
            {
                camera.Position -= new Vector2(120, 0) * deltaTime;
                player.x -= 2;
                player.state = Player.PlayerStates.West;

                if (Keyboard.GetState().IsKeyDown(Keys.W))
                {
                    player.state = Player.PlayerStates.NorthWest;
                }
                else if (Keyboard.GetState().IsKeyDown(Keys.S))
                {
                    player.state = Player.PlayerStates.SouthWest;
                }
            }

            if (Keyboard.GetState().IsKeyDown(Keys.D))
            {
                camera.Position += new Vector2(120, 0) * deltaTime;
                player.x += 2;
                player.state = Player.PlayerStates.East;

                if (Keyboard.GetState().IsKeyDown(Keys.W))
                {
                    player.state = Player.PlayerStates.NorthEast;
                }
                else if (Keyboard.GetState().IsKeyDown(Keys.S))
                {
                    player.state = Player.PlayerStates.SouthEast;
                }
            }

            if (Keyboard.GetState().IsKeyDown(Keys.D1))
            {
                player.castAbility1(Content.Load<Texture2D>("Other2_Test"));
            }
            if (Keyboard.GetState().IsKeyDown(Keys.D2))
            {
                player.castAbility2(Content.Load<Texture2D>("Other1_Test"));
            }

            player.update(enemies);
        }
    }
}
