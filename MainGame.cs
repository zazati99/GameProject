﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

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

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            ScreenManager.Instance.Initialize();

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

            // Loads content in ScreenManager
            ScreenManager.Instance.LoadContent(Content);
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

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Azure);

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
    }
}
