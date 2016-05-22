using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

// NOTES!
// On conflish errors go to GIT BASH/CMD
// "cd" to the GIT folder Document > JannasAdventure
// Then type "git pull"
// Then "git stash"
// Then "git pull" again - will solve the issue!

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
        Texture2D background;

        GameStates state;
        enum GameStates
        {
            Menu,
            Loading,
            Playing,
            Paused,
            Map
        };

        // Josh - Implement world space/view space/object space/window space
        private static int sW = 800; // Get Screen Width
        private static int sH = 600; // Get Screen Height

        private int pX = 1000; //(sW/2);
        private int pY = 1000; //(sH/2);

        Player player;

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
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            player = new Player(Content.Load<Texture2D>("Player_Test"), pX, pY);
            font = Content.Load<SpriteFont>("my_font");
            background = Content.Load<Texture2D>("Background");
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// game-specific content.
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
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();
            
            //World fliped when implmented background - Have changed x and y
            if(Keyboard.GetState().IsKeyDown(Keys.A))
            {
                pX++;
                player.setX(pX);
            }
            if (Keyboard.GetState().IsKeyDown(Keys.D))
            {
                pX--;
                player.setX(pX);
            }
            if (Keyboard.GetState().IsKeyDown(Keys.W))
            {
                pY++;
                player.setY(pY);
            }
            if (Keyboard.GetState().IsKeyDown(Keys.S))
            {
                pY--;
                player.setY(pY);
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

            spriteBatch.Begin();
            // Draw shit in here!!
            // Typical Order:
            //  1. Background/Screen            - Done in this class
            //  2. Objects/Enemies              - Classes store in this class array
            //  3. Player/Character             - Own object, stored in variable
            //  4. UI/Map/Pause Menu/etc...     - Set, done in this class (Method perhaps)

            spriteBatch.Draw(background, new Rectangle((pX - background.Width), (pY - background.Height), background.Width, background.Height), Color.GhostWhite);
            spriteBatch.DrawString(font, "Boobies", new Vector2((sW / 2), (sH / 2)), Color.GhostWhite);
            player.draw(spriteBatch);

            spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
