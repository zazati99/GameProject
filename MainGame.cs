using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using Microsoft.Xna.Framework.Audio;

using GameProject.GameScreens;
using GameProject.GameUtils;

namespace GameProject
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class MainGame : Game
    {
        // important memes
        public static GraphicsDeviceManager Graphics;
        SpriteBatch spriteBatch;

        // Static game variables
        public static Vector2 TILE_SIZE = new Vector2(24, 24);

        public static float GAME_SPEED = 1f;
        public static bool GAME_PAUSED = false;

        static bool willChangeGameSpeed;
        static bool willChangePausedState;

        static bool nextPausedState;
        static float nextGameSpeed;

        // Game view
        GameView gameView;

        public MainGame()
        {
            Content.RootDirectory = "Content";

            Graphics = new GraphicsDeviceManager(this);
            Graphics.HardwareModeSwitch = false;
            Graphics.GraphicsProfile = GraphicsProfile.Reach;

            gameView = new GameView(Graphics);
        }

        private Song backgroundMusic;

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
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

            // Loads fonts
            GameFonts.LoadContent(Content);

            // Loads content in ScreenManager
            ScreenManager.Instance.LoadContent(Content);

           // Loads a masterpeice
            backgroundMusic = Content.Load<Song>("Sounds/Music/Memer");
            MediaPlayer.Play(backgroundMusic);
            MediaPlayer.IsRepeating = true;
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// game-specific content.
        /// </summary>
        protected override void UnloadContent()
        {
            ScreenManager.Instance.UnloadContent();
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            // Input start uppdatering
            GameInput.UpdateStart();

            // switch fullscreen
            if (GameInput.KeyPressed(Keys.F5)) gameView.SwitchFullscreen();

            // Updaterar ScreenManager och allt i den
            ScreenManager.Instance.Update();

            // Input slut uppdatering
            GameInput.UpdateEnd();

            // Fix paused things
            if (willChangeGameSpeed)
            {
                GAME_SPEED = nextGameSpeed;
                willChangeGameSpeed = false;
            }
            if (willChangePausedState)
            {
                GAME_PAUSED = nextPausedState;
                willChangePausedState = false;
            }

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(new Color(76, 51, 26));

            // Begin spritebatch and feed in values to make view correct
            spriteBatch.Begin
            (
                SpriteSortMode.BackToFront,
                BlendState.AlphaBlend,
                SamplerState.PointClamp,
                null,
                null,
                null,
                gameView.GetTransformation()
            );

            // Målar screenmanager och allt i den som GameScreens
            ScreenManager.Instance.Draw(spriteBatch);

            // end of normal spritebatch
            spriteBatch.End();

            // SpriteBatch som används för GUI
            spriteBatch.Begin
            (
                SpriteSortMode.BackToFront,
                BlendState.AlphaBlend,
                SamplerState.PointClamp,
                null,
                null,
                null,
                gameView.GetUITransformation()
            );

            // Målar screenmanager och allt i den som GameScreens
            ScreenManager.Instance.DrawGUI(spriteBatch);

            // end of normal spritebatch
            spriteBatch.End();

            base.Draw(gameTime);
        }

        // Change GameSpeed;
        public static void ChangeGameSpeed(float speed)
        {
            willChangeGameSpeed = true;
            nextGameSpeed = speed;
        }

        // Switch paused
        public static void ChangePaused(bool paused)
        {
            willChangePausedState = true;
            nextPausedState = paused;
        }
    }
}
