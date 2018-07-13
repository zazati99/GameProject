using System;

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
            collider.Size = new Vector2(32, 32);
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
        public override void LoadContent(ContentManager content)
        {
            tileTexture = content.Load<Texture2D>("DirtTile");

            UpdateTile();

            Ground[] grounds = GetSurroundingGrounds();
            for (int i = 0; i < grounds.Length; i++)
            {
                grounds[i].UpdateTile();
            }
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
            if (GroundAtPlace(Position + new Vector2(32, 0)) is Ground groundRight)
            {
                groundList.Add(groundRight);
            }
            if (GroundAtPlace(Position + new Vector2(-32, 0)) is Ground groundLeft)
            {
                groundList.Add(groundLeft);
            }
            if (GroundAtPlace(Position + new Vector2(0, 32)) is Ground groundDown)
            {
                groundList.Add(groundDown);
            }
            if (GroundAtPlace(Position + new Vector2(0, -32)) is Ground groundUp)
            {
                groundList.Add(groundUp);
            }

            return groundList.ToArray();
        }

        // Update Tile
        public void UpdateTile()
        {
            // Check tiles in different positions
            bool right = IsGroundAtPlace(Position + new Vector2(32, 0));
            bool down = IsGroundAtPlace(Position + new Vector2(0, 32));
            bool left = IsGroundAtPlace(Position + new Vector2(-32, 0));
            bool up = IsGroundAtPlace(Position + new Vector2(0, -32));

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
