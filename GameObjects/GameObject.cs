using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Xml.Serialization;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

using GameProject.GameScreens;

namespace GameProject.GameObjects
{
    [Serializable]
    public class GameObject
    {
        // Game screen that object is in
        GameScreen gameScreen;

        Texture2D texture;

        public Vector2 Position;
        public string TexturePath;

        // Initializes components
        public virtual void Initialize(GameScreen screen)
        {

        }

        // Load Content
        public virtual void LoadContent(ContentManager content)
        {
            texture = content.Load<Texture2D>(TexturePath);
        }

        // Unload content
        public virtual void UnloadContent()
        {
            
        }

        // Updates components
        public virtual void Update()
        {

        }

        // Draws componentes
        public virtual void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, Position);
        }

        // Draws gui
        public virtual void DrawGUI(SpriteBatch spriteBatch)
        {

        }
    }
}
