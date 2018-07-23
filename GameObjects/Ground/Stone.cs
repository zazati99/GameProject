using System;

using Microsoft.Xna.Framework;

using GameProject.GameScreens;
using GameProject.GameObjects.ObjectComponents;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Audio;

namespace GameProject.GameObjects
{
    public class Stone : Ground
    {
        // Constructor
        public Stone(GameScreen gameScreen, TileMap tileMap) : base(gameScreen, tileMap)
        {
            GroundType = GROUND_TYPE.STONE;
            groundDurability = 3;
        }

        // Loads content
        public override void LoadContent(ContentManager content)
        {
            LoadGroundTexture(GroundType, "Images/Sprites/Tiles/stone_sprite");
            LoadGroundDestroySound(GroundType, "Sounds/Effects/Stoned");
        }
    }
}
