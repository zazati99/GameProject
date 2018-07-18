using System;
using System.Collections.Generic;
using System.Threading;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;

using System.Xml.Serialization;

using GameProject.GameUtils;
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
        public List<TileMap> TileMaps;

        // List of GameObjects
        public List<GameObject> GameObjects;

        // List of mfin Backgrounds
        public List<ScreenBackground> ScreenBackgrounds;

        // List of particleSystems
        public List<ScreenParticleSystem> ScreenParticleSystems;

        // Screen Camera
        public ScreenCamera Camera;

        // Constructor
        public GameScreen()
        {
            TileMaps = new List<TileMap>();
            GameObjects = new List<GameObject>();
            ScreenBackgrounds = new List<ScreenBackground>();
            ScreenParticleSystems = new List<ScreenParticleSystem>();
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

            AddTileMap(GameFileManager.LoadTileMap(this, TileMap.TileMapsRight[0], new Vector2(0, 64)));
            AddTileMap(GameFileManager.LoadTileMap(this, TileMap.TileMapsLeft[0], new Vector2(MainGame.TILE_SIZE.X * 8, 64)));
            //AddTileMap(GameFileManager.LoadTileMap(this, TileMap.TileMapsRight[0], Vector2.Zero));

            ScreenBackground Background1 = new ScreenBackground();
            Background1.LoadContent(content, "dirt_background");
            ScreenBackgrounds.Add(Background1);

            ScreenParticleSystem particles = new ScreenParticleSystem(this);
            particles.Acceleration = new Vector2(0, .2f);
            particles.Speed = new Vector2(0, -4);
            particles.SpeedDeviation = new Vector2(4, 1);
            particles.Position = new Vector2(100, -25);
            particles.Textures.Add(GameObject.CreateRectangle(Vector2.One, Color.Gray));
            particles.Textures.Add(GameObject.CreateRectangle(Vector2.One, Color.DarkGray));
            particles.LifeSpan = 90;
            ScreenParticleSystems.Add(particles);

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

            // Update particles
            for (int i = 0; i < ScreenParticleSystems.Count; i++)
            {
                ScreenParticleSystems[i].Update();
            }

            // Updates
            if (GameInput.KeyPressed(Keys.F10))
            {
                ScreenParticleSystems[0].Emit(5);
            }
            if (GameInput.KeyPressed(Keys.F6))
            {
                AddTileMap(GameFileManager.LoadTileMap(this, "GameProject/Content/TestTile", TileMaps[TileMaps.Count - 2].Position + new Vector2(16 * MainGame.TILE_SIZE.X, 0)));
            }
            if (GameInput.KeyPressed(Keys.F7))
            {
                TileMap map = TileMaps[TileMaps.Count - 1];
                map.DestroyTileMap();

                map = null;
                GC.Collect();
            }

            // Update camera
            Camera.Update();
        }

        // Draws everything on screen
        public virtual void Draw(SpriteBatch spriteBatch)
        {
            // Draws Game Objects
            for (int i = 0; i < GameObjects.Count; i++)
            {
                GameObjects[i].Draw(spriteBatch);
            }

            // Draws Screen backgrounds
            for (int i = 0; i < ScreenBackgrounds.Count; i++)
            {
                ScreenBackgrounds[i].Draw(spriteBatch);
            }

            // Draw particle systems
            for (int i = 0; i < ScreenParticleSystems.Count; i++)
            {
                ScreenParticleSystems[i].Draw(spriteBatch);
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

        public void SetTileMaps(int width, int height, int[] poi, int start)
        {
            TileMap[] maps = new TileMap[width * height];

            int mapPosition = start;

            // Loop through each floor
            for (int floor = 0; floor < height; floor++)
            {
                // if start is not on this floor, continue
                if (start > (floor + 1) * width - 1)
                    continue;

                // Check for points of interest on floor
                bool isPoiOnFloor = false;
                List<int> poiOnFloor = new List<int>();

                for (int i = 0; i < poi.Length; i++)
                {
                    if (poi[i] < (floor + 1) * width - 1 && poi[i] > floor * width)
                    {
                        isPoiOnFloor = true;
                        poiOnFloor.Add(poi[i]);
                    }
                }

                bool[] foundPoi = new bool[poiOnFloor.Count];
                for (int i = 0; i < foundPoi.Length; i++) foundPoi[i] = false;

                // Go through rooms
                while (true)
                {
                    // is there room to the right and left?
                    bool right = mapPosition < (floor + 1) * width - 1;
                    bool left = mapPosition > floor * width;

                    // Decide where to go
                    if (right && left)
                    {
                        if (GameMath.RandomRange(0, 1) == 1)
                        {
                            mapPosition++;
                        }
                        else mapPosition--;
                    }
                    else if (right)
                    {
                        mapPosition++;
                    }
                    else mapPosition--;
                    
                    //  Has the points of interests been found?
                    if (checkBoolArray(foundPoi))
                    {

                    }
                    else
                    {
                        for (int i = 0; i < poiOnFloor.Count; i++)
                        {
                            if (poiOnFloor[i] == mapPosition)
                            {
                                foundPoi[i] = true;
                            }
                        }
                    }
                }
            }
        }

        bool checkBoolArray(bool[] bools)
        {
            for (int i = 0; i < bools.Length; i++)
            {
                if (!bools[i]) return false; 
            }
            return true;
        }

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
