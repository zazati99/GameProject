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

            Sprite sprite = new Sprite(this);
            AddComponent(sprite);
            sprite.AddTexture(CreateRectangle(new Vector2(24, 24), Color.Gray));
        }

        // Loads content
        public override void LoadContent(ContentManager content)
        {
            
        }

        // Draws the normal way (for now)
        public override void Draw(SpriteBatch spriteBatch)
        {
            for (int i = 0; i < ObjectComponents.Count; i++)
            {
                ObjectComponents[i].Draw(spriteBatch);
            }
        }
    }
}
