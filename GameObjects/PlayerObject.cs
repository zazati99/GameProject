using System;

using GameProject.GameUtils;
using GameProject.GameScreens;
using GameProject.GameObjects.ObjectComponents;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Graphics;

namespace GameProject.GameObjects
{
    public class PlayerObject : GameObject, IMovable
    {
        // Movement variables
        float maxSpeed;
        float accelerationSpeed;
        float airAccelerationSpeed;
        float slowDownSpeed;
        float airSlowDownSpeed;

        // Jumping
        float maxJumpHeight;
        float minJumpHeight;
        int jumpBuffer; // Lets you jump even if you press button too early

        // Components
        Physics physics;
        HitBox hitBox;
        Sprite sprite;

        public override void Initialize(GameScreen screen)
        {
            base.Initialize(screen);

            // Components
            physics = new Physics();
            AddComponent(physics);
            physics.Solid = true;
            physics.GravityEnabled = true;

            hitBox = new HitBox();
            BoxCollider boxCollider = new BoxCollider();
            boxCollider.Size = new Vector2(32, 64);
            boxCollider.Offset = new Vector2(-16, -32);
            hitBox.SetCollider(boxCollider);
            AddComponent(hitBox);

            sprite = new Sprite();
            AddComponent(sprite);
            sprite.AddTexture(CreateRectangle(new Vector2(32, 64), Color.Blue));
            sprite.SpriteOffset = new Vector2(-16, -32);

            // Movement initialization
            maxSpeed = 2;
            accelerationSpeed = .5f;
            airAccelerationSpeed = .25f;
            slowDownSpeed = .25f;
            airSlowDownSpeed = .1f;

            maxJumpHeight = 6;
            minJumpHeight = 2.5f;
            jumpBuffer = 0;
        }

        // Update components and do other logic
        public override void Update()
        {
            // Horizontal movement controlls
            bool left = GameInput.KeyDown(Keys.Left);
            bool right = GameInput.KeyDown(Keys.Right);
            if (left || right)
            {
                HorizontalMovement(((right ? 1 : 0) - (left ? 1 : 0)) * maxSpeed);
            }
            else StopMoving();

            // Jumping controlls
            if (GameInput.KeyPressed(Keys.Z))
                jumpBuffer = 3;

            if (physics.Grounded)
            {
                if (jumpBuffer > 0)
                    Jump();
            }

            if (!GameInput.KeyDown(Keys.Z))
                physics.Velocity.Y = Math.Max(physics.Velocity.Y, -minJumpHeight);

            jumpBuffer--;

            // Test mining meme
            if (GameInput.KeyPressed(Keys.X))
            {
                Vector2 horizontalHitPoint = Position;
                Vector2 verticalHitPoint = Position;

                if (GameInput.KeyDown(Keys.Up)) verticalHitPoint += new Vector2(0, -5);
                if (GameInput.KeyDown(Keys.Down)) verticalHitPoint += new Vector2(0, 5);

                if (GameInput.KeyDown(Keys.Left)) horizontalHitPoint += new Vector2(-5, 0);
                if (GameInput.KeyDown(Keys.Right)) horizontalHitPoint += new Vector2(5, 0);

                if (hitBox.ObjectMeeting<Ground>(horizontalHitPoint) is Ground hGround)
                {
                    DestroyObject(hGround);
                }
                else if (hitBox.ObjectMeeting<Ground>(verticalHitPoint) is Ground vGround)
                {
                    DestroyObject(vGround);
                }
            }

            base.Update();
        }

        // Drawing sprite and other things
        public override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);
            ShapeRenderer.FillRectangle(spriteBatch, Position, new Vector2(2, 2), 0, Color.Red);
        }

        #region Movement

        // Move horizontally
        public void HorizontalMovement(float targetSpeed)
        {
            physics.Velocity.X = GameMath.Approach(physics.Velocity.X, targetSpeed, physics.Grounded ? accelerationSpeed : airAccelerationSpeed);
        }

        //Stop this lmao
        public void StopMoving()
        {
            physics.Velocity.X = GameMath.Approach(physics.Velocity.X, 0, physics.Grounded ? slowDownSpeed : airSlowDownSpeed);
        }

        // jump
        public void Jump()
        {
            physics.Velocity.Y = -maxJumpHeight;
        }

        #endregion
    }
}
