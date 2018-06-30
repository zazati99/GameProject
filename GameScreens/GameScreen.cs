using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;

using System.Xml.Serialization;

namespace GameProject.GameScreens
{
   [Serializable]
    public class GameScreen
    {
        // Content manager used in screen
        [XmlIgnore] public ContentManager Content;

        // Constructor
        public GameScreen()
        {
            
        }

        // Load content for screen
        public virtual void LoadContent(ContentManager content)
        {
            Content = new ContentManager(content.ServiceProvider, "Content");
        }

        // Unloads content on screen
        public virtual void UnloadContent()
        {
            
        }

        // Updates everything on screen
        public virtual void Update()
        {

        }

        // Draws everything on screen
        public virtual void Draw(SpriteBatch spriteBatch)
        {
            
        }

        // Draw GUI elements on screen
        public virtual void DrawGUI(SpriteBatch spriteBatch)
        {

        }
    }
}
