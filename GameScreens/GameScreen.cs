﻿using System;
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
    public class GameScreen
    {
        // Content manager used in screen
        public ContentManager Content;

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
            // Lists
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
