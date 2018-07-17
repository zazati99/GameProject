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
        // Lists of rooms
        public static List<string> TileMapsUp = new List<string> {"GameProject/Content/TileUp"};
        public static List<string> TileMapsRight = new List<string> {"GameProject/Content/TileRight"};
        public static List<string> TileMapsDown = new List<string> {"GameProject/Content/TileDown"};
        public static List<string> TileMapsLeft = new List<string> {"GameProject/Content/TileLeft"};

        // List of GameObjects
        public List<GameObject> GameObjects;

        // Dictionary of TileSets
        public Dictionary<string, Texture2D> TileSets;

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
                GameObjects[i].LoadContent(Content, this);
            }
        }

        // Unload that garbage
        public void UnloadContent()
        {
            for (int i = 0; i < GameObjects.Count; i++)
            {
                GameObjects[i].UnloadContent();
            }

            foreach (KeyValuePair<string, Texture2D> entry in TileSets)
            {
                entry.Value.Dispose();
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
