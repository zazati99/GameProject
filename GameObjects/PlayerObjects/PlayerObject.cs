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

        // Mining tool
        MiningTool miningTool;

        public PlayerObject(GameScreen gameScreen) : base(gameScreen)
        {
            // Components
            physics = new Physics(this);
            AddComponent(physics);
            physics.Solid = true;
            physics.GravityEnabled = true;

            hitBox = new HitBox(this);
            BoxCollider boxCollider = new BoxCollider();
            boxCollider.Size = new Vector2(12, 40);
            boxCollider.Offset = new Vector2(-6, -20);
            hitBox.SetCollider(boxCollider);
            AddComponent(hitBox);

            sprite = new Sprite(this);
            AddComponent(sprite);
            sprite.AddTexture(gameScreen.Content, "player_idle");
            sprite.SpriteOffset = new Vector2(-23, -28);

            // MAke this the tafget of the Screen camera
            Screen.Camera.SetTarget(this);

            // Mining tool;
            miningTool = new MiningTool();
            miningTool.Initialize(this);

            // Movement initialization
            maxSpeed = 2;
            accelerationSpeed = .5f;
            airAccelerationSpeed = .25f;
            slowDownSpeed = .25f;
            airSlowDownSpeed = .1f;

            maxJumpHeight = 4.9f;
            minJumpHeight = 2.5f;
            jumpBuffer = 0;
        }

        // Update components and do other logic
        public override void Update()
        {
            // Horizontal movement controlls
            bool left = GameInput.InputDown(GameInput.Left);
            bool right = GameInput.InputDown(GameInput.Right);
            if (left || right)
            {
                HorizontalMovement(((right ? 1 : 0) - (left ? 1 : 0)) * maxSpeed);
            }
            else if (GameInput.LeftStick.X != 0)
            {
                HorizontalMovement(GameInput.LeftStick.X * maxSpeed);
            }
            else StopMoving();

            // Jumping controlls
            if (GameInput.InputPressed(GameInput.Jump))
                jumpBuffer = 3;

            if (physics.Grounded)
            {
                if (jumpBuffer > 0)
                    Jump();
            }

            if (!GameInput.InputDown(GameInput.Jump))
                physics.Velocity.Y = Math.Max(physics.Velocity.Y, -minJumpHeight);

            jumpBuffer--;

            // Mining
            miningTool.DetermineTarget();
            miningTool.Attack();
            miningTool.Dig();

            base.Update();

            if (GameInput.InputPressed(GameInput.Dig) && !MainGame.GAME_PAUSED)
            {
                if (ObjectAtPosition<IActivatable>(Position) is IActivatable IA)
                {
                    IA.Activate();
                }
            }
        }

        // Drawing sprite and other things
        public override void Draw(SpriteBatch spriteBatch)
        { 
            base.Draw(spriteBatch);
            miningTool.Draw(spriteBatch);

            spriteBatch.DrawString(GameFonts.font, physics.Velocity.X.ToString(), Position - new Vector2(GameFonts.font.MeasureString(physics.Velocity.X.ToString()).X/2, 48), Color.Black);
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
