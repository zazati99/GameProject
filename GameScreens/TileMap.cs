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
    public class TileMap
    {
        // List of GameObjects
        public List<GameObject> GameObjects;

        // Dictionary of TileSets
        Dictionary<string, Texture2D> TileSets;

        // GameScreen screen
        public GameScreen Screen;

        // Content Manager for Tile mape
        public ContentManager Content;

        // Position of tile Map
        public Vector2 Position;

        // Size in tiles
        public Vector2 Size;

        // Constructor and Initialization
        public TileMap(GameScreen gameScreen)
        {
            Screen = gameScreen;
            GameObjects = new List<GameObject>();
            TileSets = new Dictionary<string, Texture2D>();
        }

        // Load the content
        public void LoadContent(ContentManager content)
        {
            Content = new ContentManager(content.ServiceProvider, "Content");

            for (int i = 0; i < GameObjects.Count; i++)
            {
                GameObjects[i].LoadContent(Content);
            }
        }

        // Unload that garbage
        public void UnloadContent()
        {
            for (int i = 0; i < GameObjects.Count; i++)
            {
                GameObjects[i].UnloadContent();
            }
        }

        // Destroy TileMap
        public void DestroyTileMap()
        {
            for (int i = 0; i < GameObjects.Count; i++)
            {
                GameObjects[i].DestroyObject();
            }
            UnloadContent();
            Screen.TileMaps.Remove(this);
        }
    }
}
