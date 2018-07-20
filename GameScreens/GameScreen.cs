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

        TileMap[] tileMaps;

        // Constructor
        public GameScreen()
        {
            TileMaps = new List<TileMap>();
            GameObjects = new List<GameObject>();
            ScreenBackgrounds = new List<ScreenBackground>();
            ScreenParticleSystems = new List<ScreenParticleSystem>();
            Camera = new ScreenCamera();
            Camera.Initialize();

            tileMaps = new TileMap[9];
        }

        // Load content for screen
        public virtual void LoadContent(ContentManager content)
        {
            Content = new ContentManager(content.ServiceProvider, "Content");

            for (int i = 0; i < GameObjects.Count; i++)
            {
                GameObjects[i].LoadContent(Content);
            }

            //AddTileMap(GameFileManager.LoadTileMap(this, TileMap.TileMapsRight[0], new Vector2(0, 64)));
            //AddTileMap(GameFileManager.LoadTileMap(this, TileMap.TileMapsLeft[0], new Vector2(MainGame.TILE_SIZE.X * 8, 64)));
            //AddTileMap(GameFileManager.LoadTileMap(this, TileMap.TileMapsRight[0], Vector2.Zero));

            for (int y = 0; y < 3; y++)
            {
                for (int x = 0; x < 3; x++)
                {
                    TileMap tm = GameFileManager.LoadTileMap(this, TileMap.TileMapsRight[0], new Vector2(0 + MainGame.TILE_SIZE.X * 8 * x, 64 + MainGame.TILE_SIZE.Y * 8 * y));
                    tileMaps[y * 3 + x] = tm;
                    tm.LoadContent(Content);
                }
            }

            for (int i = 0; i < tileMaps.Length; i++)
            {
                tileMaps[i].Load();
            }

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
            PlayerObject player = null;
            for (int i = 0; i < GameObjects.Count; i++)
            {
                if (GameObjects[i] is PlayerObject p) player = p;
            }

            for (int i = 0; i < tileMaps.Length; i++)
            {
                if (tileMaps[i].IsLoaded)
                {
                    if (Vector2.Distance(player.Position, tileMaps[i].Position) > 250)
                    {
                        tileMaps[i].Unload();
                    }
                } else
                {
                    if (Vector2.Distance(player.Position, tileMaps[i].Position) < 250)
                    {
                        tileMaps[i].Load();
                    }
                }
            }

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
            if (GameInput.KeyPressed(Keys.F6))
            {
                tileMaps[0].Unload();
            }
            if (GameInput.KeyPressed(Keys.F7))
            {
                tileMaps[0].Load();
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
