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

        Vector2 hitPoint;

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
            boxCollider.Offset = new Vector2(-16, -32);
            hitBox.SetCollider(boxCollider);
            AddComponent(hitBox);

            Sprite sprite = new Sprite();
            AddComponent(sprite);
            sprite.AddTexture(CreateRectangle(new Vector2(32, 64), Color.Blue));
            sprite.SpriteOffset = new Vector2(-16, -32);

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
                if (GameInput.KeyPressed(Keys.Z))
                {
                    GetComponent<Physics>().Velocity.Y = -6;
                }
            }

            hitPoint = Position + new Vector2(0, -5);
            if (GameInput.KeyDown(Keys.Up)) hitPoint = new Vector2(Position.X, hitPoint.Y - 48);
            if (GameInput.KeyDown(Keys.Down)) hitPoint = new Vector2(Position.X, hitPoint.Y + 48);
            if (GameInput.KeyDown(Keys.Left)) hitPoint = new Vector2(hitPoint.X -32, Position.Y - 5);
            if (GameInput.KeyDown(Keys.Right)) hitPoint = new Vector2(hitPoint.X + 32, Position.Y - 5);
            if (GameInput.KeyPressed(Keys.X))
            {
                if (ObjectAtPosition<Ground>(hitPoint) is Ground upperGround)
                {
                    DestroyObject(upperGround);
                } 
                else if (ObjectAtPosition<Ground>(hitPoint + new Vector2(0, 10)) is Ground lowerGround)
                {
                    DestroyObject(lowerGround);
                }
            }

            base.Update();
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);
            ShapeRenderer.FillRectangle(spriteBatch, Position, new Vector2(2, 2), 0, Color.Red);
            ShapeRenderer.FillRectangle(spriteBatch, hitPoint, new Vector2(2, 2), 0, Color.Red);
        }

    }
}
