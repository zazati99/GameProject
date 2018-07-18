using System;

using GameProject.GameUtils;
using GameProject.GameScreens;
using GameProject.GameObjects.ObjectComponents;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

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

            sprite.AddTexture(gameScreen.Content, "Images/Sprites/Player//Animations/Walking/player_walk1");
            sprite.AddTexture(gameScreen.Content, "Images/Sprites/Player//Animations/Walking/player_walk2");
            sprite.AddTexture(gameScreen.Content, "Images/Sprites/Player//Animations/Walking/player_walk3");
            sprite.AddTexture(gameScreen.Content, "Images/Sprites/Player//Animations/Walking/player_walk4");
            sprite.AddTexture(gameScreen.Content, "Images/Sprites/Player//Animations/Walking/player_walk5");
            sprite.AddTexture(gameScreen.Content, "Images/Sprites/Player//Animations/Walking/player_walk6");

            sprite.ImageSpeed = 0.1f;

            sprite.SpriteOffset = new Vector2(-22, -28);

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

        // Load content
        public override void LoadContent(ContentManager content)
        {
            miningTool.LoadContent(content);
        }

        // Update components and do other logic
        public override void Update()
        {  
            // When you walking
            if (GameInput.ControllerMode)
            {
                if (GameInput.LeftStick.X != 0 && !MainGame.GAME_PAUSED)
                {
                    HorizontalMovement(GameInput.LeftStick.X * maxSpeed);
                }
                else StopMoving();
            }
            else
            {
                bool left = GameInput.InputDown(GameInput.Left);
                bool right = GameInput.InputDown(GameInput.Right);
                if ((left || right) && !MainGame.GAME_PAUSED)
                {
                    HorizontalMovement(((right ? 1 : 0) - (left ? 1 : 0)) * maxSpeed);
                }
                else StopMoving();
            }

            // Check if paused, if not perform stuff
            if (!MainGame.GAME_PAUSED)
            {

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

                // Mining and attacking
                miningTool.DetermineTarget();
                miningTool.Attack();
                miningTool.Dig();

                // ACtivate object that is touched
                if (GameInput.InputPressed(GameInput.Dig))
                {
                    if (ObjectAtPosition<IActivatable>(Position) is IActivatable IA)
                    {
                        IA.Activate();
                    }
                }
            }

            base.Update();
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
