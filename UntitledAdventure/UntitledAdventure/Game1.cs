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

        Texture2D background, menuBackground;
        Camera2D camera;
        Player player;
        Enemy enemy1;
        Button butt1, butt2;

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

        private int pX = 0;
        private int pY = 0;

        Matrix viewMatix;

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
                    background = Content.Load<Texture2D>("Background");
                    pX = (background.Width / 2);
                    pY = (background.Height / 2);
                    player = new Player(Content.Load<Texture2D>("Player_Test"), pX, pY);
                    camera.Position = new Vector2((pX - (sW / 2)), (pY - (sH / 2)));
                    enemy1 = new Enemy(Content.Load<Texture2D>("Enemy_Test"), 200, 300);

                    pX = player.x;
                    pY = player.y;
                    break;
            }
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// game-specific content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
            //Content.Unload(); Will unload font...
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
                    // Draw shit in here!!
                    // Typical Order:
                    //  1. Background/Screen            - Done in this class
                    //  2. Objects/Enemies              - Classes store in this class array
                    //  3. Player/Character             - Own object, stored in variable
                    //  4. UI/Map/Pause Menu/etc...     - Set, done in this class (Method perhaps)
                    spriteBatch.Begin(transformMatrix: viewMatix);
                    spriteBatch.Draw(background, new Rectangle(0, 0, background.Width, background.Height), Color.GhostWhite);
                    spriteBatch.DrawString(font, "Boobies", new Vector2((sW / 2), (sH / 2)), Color.GhostWhite);
                    enemy1.draw(spriteBatch);

                    player.draw(spriteBatch);
                    break;

                case GameStates.Paused:
                    spriteBatch.Begin(transformMatrix: viewMatix);
                    spriteBatch.Draw(background, new Rectangle(0, 0, background.Width, background.Height), Color.GhostWhite);
                    spriteBatch.DrawString(font, "Boobies", new Vector2((sW / 2), (sH / 2)), Color.GhostWhite);
                    enemy1.draw(spriteBatch);

                    // Pause menu here - trick here is to stop calling the Update and just read in mouse click positions...
                    player.draw(spriteBatch);
                    break;
            }

            spriteBatch.End();
            base.Draw(gameTime);
        }

        private void menuUpdate(GameTime gameTime)
        {
            //if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
            //{
            //    Exit();
            //}

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
                pY -= 2;
                player.y = pY;
            }

            if (Keyboard.GetState().IsKeyDown(Keys.S))
            {
                camera.Position += new Vector2(0, 120) * deltaTime;
                pY += 2;
                player.y = pY;
            }

            if (Keyboard.GetState().IsKeyDown(Keys.A))
            {
                camera.Position -= new Vector2(120, 0) * deltaTime;
                pX -= 2;
                player.x = pX;
            }

            if (Keyboard.GetState().IsKeyDown(Keys.D))
            {
                camera.Position += new Vector2(120, 0) * deltaTime;
                pX += 2;
                player.x = pX;
            }

            if (Keyboard.GetState().IsKeyDown(Keys.NumPad1))
            {
                player.castAbility(Content.Load<Texture2D>("Other2_Test"));
            }

            player.update();
        }
    }
}
