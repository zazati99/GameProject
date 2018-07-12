using System;
using System.Collections.Generic;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;

using System.Xml.Serialization;

using GameProject.GameUtils;
using GameProject.GameObjects;
using GameProject.GameObjects.ObjectComponents;
using System.Threading;

namespace GameProject.GameScreens
{

    [Serializable]
    public class GameScreen
    {
        // Content manager used in screen
        [XmlIgnore] public ContentManager Content;

        // List of Game Objects
        public List<TileMap> TileMaps;

        // List of GameObjects
        public List<GameObject> GameObjects;

        // Screen Camera
        public ScreenCamera Camera;

        // LoadingTileMaps
        TileMap loadingTileMap;
        bool loadingTileMapReady = false;

        // Constructor
        public GameScreen()
        {
            TileMaps = new List<TileMap>();
            GameObjects = new List<GameObject>();
            Camera = new ScreenCamera();
            Camera.Initialize();
        }

        // Load content for screen
        public virtual void LoadContent(ContentManager content)
        {
            Content = new ContentManager(content.ServiceProvider, "Content");

            for (int i = 0; i < GameObjects.Count; i++)
            {
                GameObjects[i].LoadContent(Content);
            }

            /*
            PlayerObject player = new PlayerObject();
            player.Position.X = 100;
            player.Position.Y = 0;
            AddObject(player);
            */

            Enemy npc = new Enemy(this);
            AddGameObject(npc);
            npc.Position.X = 200;
            npc.Position.Y = -100;
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
            // Update objects
            for (int i = 0; i < GameObjects.Count; i++)
            {
                GameObjects[i].Update();
            }

            if (GameInput.KeyPressed(Keys.F6))
            {
                AddTileMap(GameFileManager.LoadTileMap(this, "GameProject/Content/TestTile", TileMaps[TileMaps.Count - 2].Position + new Vector2(16 * 32, 0)));
            }
            if (GameInput.KeyPressed(Keys.F7))
            {
                TileMaps[TileMaps.Count-1].DestroyTileMap();
            }

            // Update camera
            Camera.Update();
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

        // Add a Tile map
        public void AddTileMap(TileMap tileMap)
        {
            TileMaps.Add(tileMap);
            tileMap.LoadContent(Content);
        }

        // Add a gameObject
        public void AddGameObject(GameObject gameObject)
        {
            GameObjects.Add(gameObject);
            gameObject.LoadContent(Content);
        }

        #endregion
    }
}
