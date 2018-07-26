using System;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Audio;

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
        public static Ground MakeGround(GameScreen screen, TileMap tileMap, GROUND_TYPE groundType)
        {
            switch (groundType)
            {
                case GROUND_TYPE.DIRT:
                    return new Dirt(screen, tileMap);
                case GROUND_TYPE.STONE:
                    return new Stone(screen, tileMap);
            }
            return null;
        }

        // TileMap object belongs to
        protected TileMap tileMap;

        // Ground type
        public GROUND_TYPE GroundType;

        // lmao hp
        public int groundDurability;

        // Tile content
        protected Texture2D tileTexture; // TExture of tile
        protected ScreenParticleSystem particleSystem; // paticleSystem
        protected SoundEffect destroySoundEffect; // Sound effect that will be played when destroyed
        protected Rectangle sourceRectangle; // Rectangle of tile that will ben drawn

        // Constructor
        public Ground(GameScreen gameScreen, TileMap tileMap) : base(gameScreen)
        {
            // set tilemap
            this.tileMap = tileMap;

            // Load standard properties
            HitBox hitBox = new HitBox(this);
            BoxCollider collider = new BoxCollider();
            collider.Size = MainGame.TILE_SIZE;
            hitBox.SetCollider(collider);
            AddComponent(hitBox);
            hitBox.Solid = true;
        }

        // Load Content maymay
        public override void LoadContent(ContentManager content, TileMap tileMap)
        {

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

            if (particleSystem != null)
            {
                EmitParticles();
            }

            if (groundDurability <= 0)
            {
                Ground[] grounds = GetSurroundingGrounds();
                DestroyObject();
                tileMap.GameObjects.Remove(this);

                for (int i = 0; i < grounds.Length; i++)
                {
                    grounds[i].UpdateTile();
                }

                if (destroySoundEffect != null)
                    destroySoundEffect.Play();
            }
        }

        // Emit particles from ground
        public void EmitParticles()
        {
            if (GetGameObject<PlayerObject>() is PlayerObject player)
            {
                float difX = player.Position.X - (Position.X + MainGame.TILE_SIZE.X / 2);
                float difY = player.Position.Y - (Position.Y + MainGame.TILE_SIZE.Y / 2);

                float angle = (float)Math.Atan2(difY, difX);

                particleSystem.Speed = new Vector2((float)(Math.Cos(angle) * 3), (float)(Math.Sin(angle) * 3));

                particleSystem.SpeedDeviation = new Vector2(2, 2);

                particleSystem.Position = Position + MainGame.TILE_SIZE / 2;
                particleSystem.Emit(4);
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
            sourceRectangle = new Rectangle(tile * new Point(26, 26), new Point(26, 26));
        }

        #region Load Different types of content

        // Load Ground Content
        public void LoadGroundTexture(GROUND_TYPE type, string path) // Texture
        {
            if (tileMap.TileSets.ContainsKey(type))
            {
                tileTexture = tileMap.TileSets[type];
            }
            else
            {
                tileMap.AddTileSet(type, path);
                tileTexture = tileMap.TileSets[type];
            }
        }
        public void LoadGroundDestroySound(GROUND_TYPE type, string path) // Destroy sound
        {
            if (tileMap.DestroySoundEffects.ContainsKey(GroundType))
            {
                destroySoundEffect = tileMap.DestroySoundEffects[GroundType];
            }
            else
            {
                tileMap.AddDestroySoundEffect(GroundType, path);
                destroySoundEffect = tileMap.DestroySoundEffects[GroundType];
            }
        }

        #endregion
    }
}
