﻿using System;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

using GameProject.GameScreens;
using GameProject.GameObjects.ObjectComponents;
using System.Collections.Generic;

namespace GameProject.GameObjects
{
    public class Ground : GameObject
    {
        // Test sak (kasnke behåller om det funkar bra)
        public enum GROUND_TYPE
        {
            DIRT,
            STONE
        }

        // Create a type fo ground
        public static Ground MakeGround(GameScreen screen, GROUND_TYPE groundType)
        {
            Ground ground = null;
            switch (groundType)
            {
                case GROUND_TYPE.DIRT:
                    ground = new Ground(screen);
                    break;
                case GROUND_TYPE.STONE:
                    ground = new Stone(screen);
                    break;
            }
            return ground;
        }

        // lmao hp
        public int groundDurability;

        // Tile variables
        Texture2D tileTexture;
        Rectangle sourceRectangle;

        // Ground type
        public GROUND_TYPE GroundType;

        public Ground(GameScreen gameScreen) : base(gameScreen)
        {
            HitBox hitBox = new HitBox(this);
            BoxCollider collider = new BoxCollider();
            collider.Size = MainGame.TILE_SIZE;
            hitBox.SetCollider(collider);
            AddComponent(hitBox);
            hitBox.Solid = true;

            InitializeGround();
        }

        // Initialize stats for specific grounds
        public virtual void InitializeGround()
        {
            GroundType = GROUND_TYPE.DIRT;
            groundDurability = 5;
        }

        // Load Content maymay
        public override void LoadContent(ContentManager content, TileMap tileMap)
        {
            if (tileMap.TileSets.ContainsKey("DirtTile"))
            {
                tileTexture = tileMap.TileSets["DirtTile"];
            } else
            {
                tileMap.TileSets.Add("DirtTile", content.Load<Texture2D>("DirtTile"));
                tileTexture = tileMap.TileSets["DirtTile"];
            }

            UpdateTile();

            Ground[] grounds = GetSurroundingGrounds();
            for (int i = 0; i < grounds.Length; i++)
            {
                grounds[i].UpdateTile();
            }
        }

        //Update
        public override void Update()
        {
            base.Update();
        }

        // Draw the correct tile
        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(tileTexture, position: Position - Vector2.One, sourceRectangle: sourceRectangle, layerDepth: 0.4f);
        }

        // deal damage to ground
        public void TakeDamage(int damage)
        {
            groundDurability -= damage;
            if (groundDurability <= 0)
            {
                Ground[] grounds = GetSurroundingGrounds();
                DestroyObject();

                for (int i = 0; i < grounds.Length; i++)
                {
                    grounds[i].UpdateTile();
                }
            }
        }

        // Add physics to ground
        public void AddPhysics()
        {
            Physics physics = new Physics(this);
            physics.GravityEnabled = true;
            physics.Solid = true;
            AddComponent(physics);

            GetComponent<HitBox>().Solid = false;
        }

        // Does a ground object exist here?
        public bool IsGroundAtPlace(Vector2 position)
        {
            for (int i = 0; i < Screen.GameObjects.Count; i++)
            {
                if (Screen.GameObjects[i] is Ground ground)
                {
                    if (ground.Position == position) return true;
                }
            }
            return false;
        }

        // Get gorund object att this place
        public Ground GroundAtPlace(Vector2 position)
        {
            for (int i = 0; i < Screen.GameObjects.Count; i++)
            {
                if (Screen.GameObjects[i] is Ground ground)
                {
                    if (ground.Position == position) return ground;
                }
            }
            return null;
        }

        // Get surrounding Tiles
        public Ground[] GetSurroundingGrounds()
        {
            List<Ground> groundList = new List<Ground>();

            for (int i = 0; i < Screen.GameObjects.Count; i++)
            {
                if (Screen.GameObjects[i] is Ground ground)
                {
                    if (ground.Position == Position + new Vector2(MainGame.TILE_SIZE.X, 0)) groundList.Add(ground);
                    else if (ground.Position == Position + new Vector2(-MainGame.TILE_SIZE.X, 0)) groundList.Add(ground);
                    else if (ground.Position == Position + new Vector2(0, MainGame.TILE_SIZE.Y)) groundList.Add(ground);
                    else if (ground.Position == Position + new Vector2(0, -MainGame.TILE_SIZE.Y)) groundList.Add(ground);
                }
            }

            return groundList.ToArray();
        }

        // Update Tile
        public void UpdateTile()
        {
            // Check tiles in different positions
            bool right = IsGroundAtPlace(Position + new Vector2(MainGame.TILE_SIZE.X, 0));
            bool down = IsGroundAtPlace(Position + new Vector2(0, MainGame.TILE_SIZE.Y));
            bool left = IsGroundAtPlace(Position + new Vector2(-MainGame.TILE_SIZE.X, 0));
            bool up = IsGroundAtPlace(Position + new Vector2(0, -MainGame.TILE_SIZE.Y));

            // different combinations of tiles
            Point tile = new Point();
            if (!right && !down && !left && !up) tile = new Point(0, 0);
            else if (right && down && !left && !up) tile = new Point(1, 0);
            else if(!right && down && left && !up) tile = new Point(2, 0);
            else if(!right && !down && left && up) tile = new Point(3, 0);
            else if(right && !down && !left && up) tile = new Point(0, 1);
            else if(!right && down && !left && !up) tile = new Point(1, 1);
            else if(!right && !down && !left && up) tile = new Point(2, 1);
            else if(right && !down && !left && !up) tile = new Point(3, 1);
            else if(!right && !down && left && !up) tile = new Point(0, 2);
            else if(right && down && left && !up) tile = new Point(1, 2);
            else if(right && !down && left && up) tile = new Point(2, 2);
            else if(!right && down && left && up) tile = new Point(3, 2);
            else if(right && down && !left && up) tile = new Point(0, 3);
            else if(right && !down && left && !up) tile = new Point(1, 3);
            else if(!right && down && !left && up) tile = new Point(2, 3);
            else if(right && down && left && up) tile = new Point(3, 3);

            // Change sourceRectangle
            sourceRectangle = new Rectangle(tile * new Point(34, 34), new Point(34, 34));
        }
    }
}
