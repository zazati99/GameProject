using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            currentScreen = new MemeScreen();
        }

        // Loads content lul
        public void LoadContent(ContentManager content)
        {
            Content = new ContentManager(content.ServiceProvider, "Content");

            currentScreen.LoadContent(Content);
        }

        // Unloads content lul
        public void UnloadContent()
        {
            Content.Unload();
        }

        // Updaterar currentScreen och kanske annat i framtiden
        public void Update()
        {
            currentScreen.Update();
        }

        // Målar currentScreen och kanske annat i framtiden
        public void Draw(SpriteBatch spriteBatch)
        {
            currentScreen.Draw(spriteBatch);
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
