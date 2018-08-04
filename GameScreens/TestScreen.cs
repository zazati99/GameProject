using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;

using GameProject.GameObjects;
using GameProject.GameUtils;

using System;
using System.Threading;
using System.IO;

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

        // Constructor
        public TestScreen() : base()
        {
            tileMaps = new TileMap[400];
            LoadThread = new Thread(FixTiles);
        }

        // Load content
        public override void LoadContent(ContentManager content)
        {
            base.LoadContent(content);

            // Test save meme
            if (Directory.Exists("Save"))
            {
                tileMaps = GameFileManager.LoadTileMapArray("Save/Map", this, new Point(20, 20), new Vector2(0, 64));
                for (int i = 0; i < tileMaps.Length; i++)
                {
                    tileMaps[i].LoadContent(Content);
                }
            } else
            {
                for (int y = 0; y < 20; y++)
                {
                    for (int x = 0; x < 20; x++)
                    {
                        TileMap tm = GameFileManager.LoadTileMap(this, TileMap.TileMapsRight[0], new Vector2(0 + MainGame.TILE_SIZE.X * 8 * x, 64 + MainGame.TILE_SIZE.Y * 8 * y));
                        tileMaps[y * 20 + x] = tm;
                        tm.LoadContent(Content);
                    }
                }
            }
            

            player = new PlayerObject(this);
            AddGameObject(player);

            ScreenBackground Background1 = new ScreenBackground();
            Background1.LoadContent(content, "dirt_background");
            ScreenBackgrounds.Add(Background1);

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
            GameFileManager.SaveTileMapArray(tileMaps, "Save/Map");
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
            for (int i = 0; i < 20; i++)
            {
                if (Math.Abs(player.Position.Y - tileMaps[i * 20].Position.Y - 96) > 350)
                {
                    if (!tileMaps[i * 20].IsLoaded) continue;
                    for (int j = 0; j < 20; j++)
                    {
                        tileMaps[i * 20 + j].Unload();
                    }
                    continue;
                }

                for (int j = 0; j < 20; j++)
                {
                    if (Math.Abs(player.Position.X - tileMaps[i * 20 + j].Position.X - 96) > 400)
                    {
                        tileMaps[i * 20 + j].Unload();
                    } else
                    {
                        tileMaps[i * 20 + j].Load();
                    }
                }
            }
        }

        // Fix tiles in loaded tileMaps
        void FixTiles()
        {
            while (threadRunning)
            {
                for (int i = 0; i < tileMaps.Length; i++)
                {
                    if (tileMaps[i].IsLoaded)
                    {
                        tileMaps[i].FixTiles();
                    }
                }
                Thread.Sleep(175);
            }
        }
    }
}
