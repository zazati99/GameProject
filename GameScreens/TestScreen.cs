using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;

using GameProject.GameObjects;
using GameProject.GameUtils;

using System;
using System.Threading;

namespace GameProject.GameScreens
{
    public class TestScreen : GameScreen
    {
        // Array of tileMaps
        TileMap[] tileMaps;

        // Player object
        PlayerObject player;

        // Thread that loads stuff
        Thread LoadThread;
        bool threadRunning;
        bool canLoad;

        // Constructor
        public TestScreen() : base()
        {
            tileMaps = new TileMap[225];
            LoadThread = new Thread(FixTiles);
        }

        // Load content
        public override void LoadContent(ContentManager content)
        {
            base.LoadContent(content);

            for (int y = 0; y < 15; y++)
            {
                for (int x = 0; x < 15; x++)
                {
                    TileMap tm = GameFileManager.LoadTileMap(this, TileMap.TileMapsRight[0], new Vector2(0 + MainGame.TILE_SIZE.X * 8 * x, 64 + MainGame.TILE_SIZE.Y * 8 * y));
                    tileMaps[y * 15 + x] = tm;
                    tm.LoadContent(Content);
                }
            }

            player = new PlayerObject(this);
            AddGameObject(player);

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

            threadRunning = true;
            LoadThread.Start();
        }

        // Unload Content and destroy thread
        public override void UnloadContent()
        {
            base.UnloadContent();
            threadRunning = false;
        }

        // Update
        public override void Update()
        {
            CheckTileMaps();
            base.Update();
        }

        // Draw
        public override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);
        }

        // Check tileMaps
        void CheckTileMaps()
        {
            for (int i = 0; i < tileMaps.Length; i++)
            {
                if (Math.Abs(player.Position.Y - tileMaps[i].Position.Y - 96) > 350)
                {
                    tileMaps[i].Unload();
                }
                else if (Math.Abs(player.Position.X - tileMaps[i].Position.X - 96) > 400)
                {
                    tileMaps[i].Unload();
                }
                else
                {
                    tileMaps[i].Load();
                }
            }
        }

        // Fix tiles in loaded tileMaps
        void FixTiles()
        {
            while (threadRunning)
            {
                int lenght = tileMaps.Length;
                for (int i = 0; i < lenght; i++)
                {
                    if (tileMaps[i].IsLoaded)
                    {
                        if (lenght != tileMaps.Length) break;
                        tileMaps[i].FixTiles();
                    }
                }
                Thread.Sleep(150);
            }
        }
    }
}
