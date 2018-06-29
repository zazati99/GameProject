using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;

namespace GameProject.GameScreens
{
    public class GameScreen
    {

        public ContentManager Content;

        public GameScreen()
        {
            
        }

        public virtual void LoadContent(ContentManager content)
        {
            Content = new ContentManager(content.ServiceProvider, "Content");
        }

        public virtual void UnloadContent()
        {
            
        }

        public virtual void Update()
        {

        }

        public virtual void Draw(SpriteBatch spriteBatch)
        {
            
        }

    }
}
