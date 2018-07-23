using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using GameProject.GameObjects;

using GameProject.GameUtils;

using System;

namespace GameProject.GameScreens
{
    public class ScreenManager
    {

        // Screeen manager memes (komemr åt funktioner genom ScreenManager.Instance...)
        static ScreenManager instance;
        public static ScreenManager Instance
        {
            get
            {
                if (instance == null)
                    instance = new ScreenManager();

                return instance;
            }
        }

        // ContentManager
        ContentManager Content;

        // Current screen
        GameScreen currentScreen;

        // Constructor meme
        public ScreenManager()
        {
            
        }

        // Loads content lul
        public void LoadContent(ContentManager content)
        {
            Content = new ContentManager(content.ServiceProvider, "Content");

            currentScreen = new TestScreen();
            currentScreen.LoadContent(Content);
        }

        // Unloads content lul
        public void UnloadContent()
        {
            Content.Unload();
            currentScreen.UnloadContent();
        }

        // Updaterar currentScreen och kanske annat i framtiden
        public void Update()
        {
            currentScreen.Update();

            if (GameInput.KeyPressed(Microsoft.Xna.Framework.Input.Keys.F9))
            {
                //GameFileManager.SaveTileMap(currentScreen, "TestTile");
            }
        }

        // Målar currentScreen och kanske annat i framtiden
        public void Draw(SpriteBatch spriteBatch)
        {
            currentScreen.Draw(spriteBatch);
        }

        // Målar GUI i current screen
        public void DrawGUI(SpriteBatch spriteBatch)
        {
            currentScreen.DrawGUI(spriteBatch);
        }

        // byter screen (kan kommas åt överallt genom ScreenManager.Instante.ChangeScreen(screen))
        public void ChangeScreen(GameScreen screen)
        {
            currentScreen.UnloadContent();
            currentScreen = null;
            GC.Collect();

            currentScreen = screen;

            screen.LoadContent(Content);
        }
    }
}
