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

        // Screeen manager memes
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

        public ScreenManager()
        {
            currentScreen = new MemeScreen();
        }

        public void LoadContent(ContentManager content)
        {
            Content = new ContentManager(content.ServiceProvider, "Content");

            currentScreen.LoadContent(Content);
        }

        public void UnloadContent()
        {
            Content.Unload();
        }

        public void Update()
        {
            currentScreen.Update();
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            currentScreen.Draw(spriteBatch);
        }

    }
}
