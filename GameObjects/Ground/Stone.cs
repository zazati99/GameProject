using System;

using Microsoft.Xna.Framework;

using GameProject.GameScreens;
using GameProject.GameObjects.ObjectComponents;
using Microsoft.Xna.Framework.Content;

namespace GameProject.GameObjects
{
    public class Stone : Ground
    {

        public override void InitializeGround()
        {
            GroundType = GROUND_TYPE.STONE;

            groundDurability = 10;

            Sprite sprite = new Sprite();
            AddComponent(sprite);
            sprite.AddTexture(CreateRectangle(new Vector2(32, 32), Color.Gray));
        }

    }
}
