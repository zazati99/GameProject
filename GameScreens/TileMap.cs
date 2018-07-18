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
        public static List<string> TileMapsUp = new List<string> { "GameProject/Content/TileUp" };
        public static List<string> TileMapsRight = new List<string> { "GameProject/Content/TileRight" };
        public static List<string> TileMapsDown = new List<string> { "GameProject/Content/TileDown" };
        public static List<string> TileMapsLeft = new List<string> { "GameProject/Content/TileLeft" };
        public static List<string> TileMapsNone = new List<string> { "GameProject/Content/TileNone" };
        public static List<string> TileMapsLeftRight = new List<string> { "GameProject/Content/TileLeftRight" };
        public static List<string> TileMapsUpDown = new List<string> { "GameProject/Content/TileUpDown" };
        public static List<string> TileMapsUpRight = new List<string> { "GameProject/Content/TileUpRight" };
        public static List<string> TileMapsUpLeft = new List<string> { "GameProject/Content/TileUpLeft" };
        public static List<string> TileMapsDownRight = new List<string> { "GameProject/Content/DownRight" };
        public static List<string> TileMapsDownLeft = new List<string> { "GameProject/Content/TileDownLeft" };
        public static List<string> TileMapsUpDownRight = new List<string> { "GameProject/Content/TileUpDownRight" };
        public static List<string> TileMapsUpDownLeft = new List<string> { "GameProject/Content/TileUpDownLeft" };
        public static List<string> TileMapsLeftRightUp = new List<string> { "GameProject/Content/TileLeftRightUp" };
        public static List<string> TileMapsLeftRightDown = new List<string> { "GameProject/Content/TileLeftRightDown" };
        public static List<string> TileMapsAll = new List<string> { "GameProject/Content/TileAll" };

        // List of GameObjects
        public List<GameObject> GameObjects;

        // Dictionary of TileSets
        public Dictionary<string, Texture2D> TileSets;

        // Dictionary of Particle systems
        public Dictionary<string, ScreenParticleSystem> ParticleSystems;

        // GameScreen screen
        public GameScreen Screen;

        // Content Manager for Tile mape
        public ContentManager Content;

        // Position of tile Map
        public Vector2 Position;

        // Size in tiles
        public Vector2 Size;

        // Openings
        public bool OpeningUp;
        public bool OpeningRight;
        public bool OpeningDown;
        public bool OpeningLeft;

        // Constructor and Initialization
        public TileMap(GameScreen gameScreen)
        {
            Screen = gameScreen;
            GameObjects = new List<GameObject>();

            TileSets = new Dictionary<string, Texture2D>();
            ParticleSystems = new Dictionary<string, ScreenParticleSystem>();
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

        public void AddTileSet(string nameAndPath)
        {

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

    // TileMap data 
    public class TileMapData
    {
        public bool right;
        public bool down;
        public bool left;
        public bool up;

        public void CreateTileMap()
        {

        }
    }
}
