using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

using GameProject.GameUtils;
using GameProject.GameObjects.ObjectComponents;

namespace GameProject.GameObjects
{
    public class MiningTool
    {
        // Targeted ground object
        Ground target;

        // Collider that will check for target
        Collider collider;

        // Player object
        PlayerObject player;

        // Mining timer
        Timer miningTimer;

        // MiningStats
        public int MiningDamage;
        public int MiningSpeed;

        // Initialize tool
        public virtual void Initialize(PlayerObject player)
        {
            this.player = player;

            collider = new BoxCollider();
            if (collider is BoxCollider bc)
            {
                bc.Size = new Vector2(32, 32);
                bc.Offset = new Vector2(-16, -16);
            }

            // Mining Timer
            miningTimer = new Timer();

            // stats
            MiningDamage = 1;
            MiningSpeed = 5;
        }

        // Determinee a target
        public virtual void DetermineTarget()
        {
            Vector2 horizontalHitPoint = player.Position;
            Vector2 verticalHitPoint = player.Position;

            if (GameInput.InputDown(GameInput.Up) || GameInput.LeftStick.Y >= .2f) verticalHitPoint += new Vector2(0, -21);
            if (GameInput.InputDown(GameInput.Down) || GameInput.LeftStick.Y <= -.2f) verticalHitPoint += new Vector2(0, 21);

            if (GameInput.InputDown(GameInput.Left) || GameInput.LeftStick.X <= -.2f) horizontalHitPoint += new Vector2(-5, 0);
            if (GameInput.InputDown(GameInput.Right) || GameInput.LeftStick.X >= .2f) horizontalHitPoint += new Vector2(5, 0);

            if (CheckToolCollisin(horizontalHitPoint) is Ground hGround)
            {
                target = hGround;
            }
            else if (CheckToolCollisin(verticalHitPoint) is Ground vGround)
            {
                target = vGround;
            }
            else target = null;
        }

        // Dig 
        public virtual void Dig()
        {
            miningTimer.Tick();
            if (GameInput.InputDown(GameInput.Dig))
            {
                if (target != null)
                {
                    if (miningTimer.Check())
                    {
                        target.TakeDamage(MiningDamage);
                        miningTimer.Set(MiningSpeed);
                    }
                }
            }
        }

        // Draw highlight on ground
        public virtual void Draw(SpriteBatch spriteBatch)
        {
            if (target != null)
            {
                ShapeRenderer.FillRectangle(spriteBatch, target.Position, new Vector2(32, 32), 0, Color.Red);
            }
        }

        // is tool touchin anything?
        protected virtual Ground CheckToolCollisin(Vector2 position)
        {
            for (int i = 0; i < player.Screen.GameObjects.Count; i++)
            {
                if (player.Screen.GameObjects[i] is Ground ground)
                {
                    if (ground.GetComponent<HitBox>().HitBoxCollider.IsColliding(collider, ground.Position, position))
                        return ground;
                }
            }
            return null;
        }
    }
}
