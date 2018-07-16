using System;

using Microsoft.Xna.Framework;

using GameProject.GameScreens;
using GameProject.GameObjects.ObjectComponents;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace GameProject.GameObjects
{
    public class Stone : Ground
    {
        // Constructor
        public Stone(GameScreen gameScreen) : base(gameScreen)
        {

        }

        // Ground Initiaization
        public override void InitializeGround()
        {
            GroundType = GROUND_TYPE.STONE;

            groundDurability = 10;
        }

        // Loads content
        public override void LoadContent(ContentManager content, TileMap tileMap)
        {

            {
                if (tileMap.TileSets.ContainsKey("stone_sprite"))
                {
                    tileTexture = tileMap.TileSets["stone_sprite"];
                }
                else
                {
                    tileMap.TileSets.Add("stone_sprite", content.Load<Texture2D>("stone_sprite"));
                    tileTexture = tileMap.TileSets["stone_sprite"];
                }

                UpdateTile();

                Ground[] grounds = GetSurroundingGrounds();
                for (int i = 0; i < grounds.Length; i++)
                {
                    grounds[i].UpdateTile();
                }
            }

        }

    }
}
