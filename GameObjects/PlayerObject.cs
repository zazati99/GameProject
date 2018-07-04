using System;

using GameProject.GameUtils;
using GameProject.GameScreens;
using GameProject.GameObjects.ObjectComponents;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Graphics;

namespace GameProject.GameObjects
{
    public class PlayerObject : GameObject
    {

        public override void Initialize(GameScreen screen)
        {
            base.Initialize(screen);

            Physics physics = new Physics();
            AddComponent(physics);
            physics.Solid = true;
            physics.GravityEnabled = true;

            HitBox hitBox = new HitBox();
            BoxCollider boxCollider = new BoxCollider();
            boxCollider.Size = new Vector2(32, 64);
            hitBox.SetCollider(boxCollider);
            AddComponent(hitBox);

            Sprite sprite = new Sprite();
            AddComponent(sprite);
            sprite.AddTexture(CreateRectangle(new Vector2(32, 64), Color.Blue));

            physics.Velocity = new Vector2(0, 0);
        }

        public override void Update()
        {
            if (GameInput.KeyDown(Keys.Left))
            {
                GetComponent<Physics>().Velocity.X = -2;
            }
            if (GameInput.KeyDown(Keys.Right))
            {
                GetComponent<Physics>().Velocity.X = 2;
            }
            if (!GameInput.KeyDown(Keys.Right) && !GameInput.KeyDown(Keys.Left))
            {
                GetComponent<Physics>().Velocity.X = 0;
            }

            if (GetComponent<Physics>().Grounded)
            {
                if (GameInput.KeyPressed(Keys.Space))
                {
                    GetComponent<Physics>().Velocity.Y = -6;
                }
            }

            if (GameInput.KeyPressed(Keys.X))
            {
                Vector2 hitPoint = Position;
                if (GameInput.KeyDown(Keys.Down)) hitPoint.Y += 64;

                GameObject meme = GetComponent<HitBox>().SolidAtPoint(hitPoint);
                if (meme != null)
                {
                    DestroyObject(meme);
                }
            }

            base.Update();
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);
        }

    }
}
