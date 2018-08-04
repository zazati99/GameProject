using System;
using System.Collections.Generic;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;

using System.Xml.Serialization;

using GameProject.GameObjects;
using GameProject.GameObjects.ObjectComponents;
using Microsoft.Xna.Framework.Audio;

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
        public Dictionary<Ground.GROUND_TYPE, Texture2D> TileSets;

        // Dictionary of Particle systems
        public Dictionary<Ground.GROUND_TYPE, ScreenParticleSystem> ParticleSystems;

        // Dictionary of Sound Effects
        public Dictionary<Ground.GROUND_TYPE, SoundEffect> DestroySoundEffects;

        // GameScreen screen
        public GameScreen Screen;

        // Content Manager for Tile mape
        public ContentManager Content;

        // is tileMap loaded to screen?
        public bool IsLoaded;
        public bool IsTilesFixed;

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

            TileSets = new Dictionary<Ground.GROUND_TYPE, Texture2D>();
            ParticleSystems = new Dictionary<Ground.GROUND_TYPE, ScreenParticleSystem>();
            DestroySoundEffects = new Dictionary<Ground.GROUND_TYPE, SoundEffect>();
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

            foreach (KeyValuePair<Ground.GROUND_TYPE, Texture2D> entry in TileSets)
            {
                entry.Value.Dispose();
            }
        }

        // Add a tile set
        public void AddTileSet(Ground.GROUND_TYPE type, string nameAndPath)
        {
            TileSets.Add(type, Content.Load<Texture2D>(nameAndPath));
        }

        // Add a particle system
        public void AddParticleSystem(Ground.GROUND_TYPE type, ScreenParticleSystem system)
        {
            ParticleSystems.Add(type, system);
            Screen.ScreenParticleSystems.Add(system);
        }

        // Add Destroy sound effectr
        public void AddDestroySoundEffect(Ground.GROUND_TYPE type, string path)
        {
            DestroySoundEffects.Add(type, Content.Load<SoundEffect>(path));
        }

        // UnloadFrom Screen
        public void Unload()
        {
            if (IsLoaded)
            {
                for (int i = 0; i < GameObjects.Count; i++)
                {
                    Screen.GameObjects.Remove(GameObjects[i]);
                }
                IsLoaded = false;
                //IsTilesFixed = false;
            }
        }

        // Load Screen
        public void Load()
        {
            if (!IsLoaded)
            {
                for (int i = 0; i < GameObjects.Count; i++)
                {
                    Screen.GameObjects.Add(GameObjects[i]);
                }
                IsLoaded = true;
            }
        }

        // Fix tiles in tile map
        public void FixTiles()
        {
            try 
            {
                if (!IsTilesFixed)
                {
                    for (int i = 0; i < GameObjects.Count; i++)
                    {
                        if (GameObjects[i] is Ground g)
                        {
                            Ground[] grounds = g.GetSurroundingGrounds();
                            if (i == 0) g.UpdateTile();
                            if (grounds.Length == 0)
                            {
                                g.UpdateTile();
                            }
                            else
                            {
                                foreach (Ground ground in grounds)
                                {
                                    ground.UpdateTile();
                                }
                            }
                        }
                    }
                    IsTilesFixed = true;
                } 
            }
            catch (IndexOutOfRangeException e)
            {
                FixTiles();            }
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
