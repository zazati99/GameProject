using System;
using System.Collections.Generic;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;

using System.Xml.Serialization;

using GameProject.GameObjects;
using GameProject.GameObjects.ObjectComponents;

namespace GameProject.GameScreens
{

    [Serializable]
    public class GameScreen
    {
        // Content manager used in screen
        [XmlIgnore] public ContentManager Content;

        // List of Game Objects
        public List<GameObject> GameObjects;

        // Constructor
        public GameScreen()
        {
            GameObjects = new List<GameObject>();
        }

        // Initialize objects and other maymays
        public virtual void Initialize()
        {
            for (int i = 0; i < GameObjects.Count; i++)
            {
                GameObjects[i].Initialize(this);
            }
        }

        // Load content for screen
        public virtual void LoadContent(ContentManager content)
        {
            Content = new ContentManager(content.ServiceProvider, "Content");

            for (int i = 0; i < GameObjects.Count; i++)
            {
                GameObjects[i].LoadContent(Content);
            }


            for (int i = 0; i < 8; i++)
            {
                Ground ground = new Ground();
                ground.Position.X = 32*i;
                ground.Position.Y = 200;
                AddObject(ground);
            }


            PlayerObject player = new PlayerObject();
            player.Position.X = 100;
            player.Position.Y = 0;
            AddObject(player);
        }

        // Unloads content on screen
        public virtual void UnloadContent()
        {
            for (int i = 0; i < GameObjects.Count; i++)
            {
                GameObjects[i].UnloadContent();
            }
        }

        // Updates everything on screen
        public virtual void Update()
        {
            for (int i = 0; i < GameObjects.Count; i++)
            {
                GameObjects[i].Update();
            }
        }

        // Draws everything on screen
        public virtual void Draw(SpriteBatch spriteBatch)
        {
            for (int i = 0; i < GameObjects.Count; i++)
            {
                GameObjects[i].Draw(spriteBatch);
            }
        }

        // Draw GUI elements on screen
        public virtual void DrawGUI(SpriteBatch spriteBatch)
        {
            for (int i = 0; i < GameObjects.Count; i++)
            {
                GameObjects[i].DrawGUI(spriteBatch);
            }
        }

        #region Screen functions

        // Add a GameObject
        public void AddObject(GameObject gameObject)
        {
            GameObjects.Add(gameObject);
            gameObject.Initialize(this);
            gameObject.LoadContent(Content);
        }

        #endregion
    }
}
