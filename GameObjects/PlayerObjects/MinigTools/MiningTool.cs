using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

using GameProject.GameUtils;
using GameProject.GameObjects.ObjectComponents;
using System.Collections.Generic;

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

            List<Ground> hGround = CheckToolCollisin(horizontalHitPoint);
            List<Ground> vGround = CheckToolCollisin(verticalHitPoint);

            if (hGround.Count != 0)
            {
                Ground ground = hGround[0];
                for(int i = 1; i < hGround.Count; i++)
                {
                    if (Vector2.DistanceSquared(player.Position, hGround[i].Position + new Vector2(16, 16)) < Vector2.DistanceSquared(player.Position, ground.Position + new Vector2(16, 16)))
                        ground = hGround[i];
                }
                target = ground;
            }
            else if (vGround.Count != 0)
            {
                Ground ground = vGround[0];
                for (int i = 1; i < vGround.Count; i++)
                {
                    if (Vector2.DistanceSquared(player.Position, vGround[i].Position + new Vector2(16,16)) < Vector2.DistanceSquared(player.Position, ground.Position + new Vector2(16, 16)))
                        ground = vGround[i];
                }
                target = ground;
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

        // Atac
        public virtual void Attack()
        {
            if (GameInput.InputPressed(GameInput.Dig))
            {
                Vector2 attackPos = player.Position;
                if (GameInput.InputDown(GameInput.Right)) attackPos += new Vector2(32, 0);
                if (GameInput.InputDown(GameInput.Left)) attackPos += new Vector2(-32, 0);

                if (CheckEnemyCollision(attackPos) is Enemy e)
                {
                    e.TakeDamage(1);
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

        // is tool touchin ground?
        protected virtual List<Ground> CheckToolCollisin(Vector2 position)
        {
            List<Ground> groundList = new List<Ground>();
            for (int i = 0; i < player.Screen.GameObjects.Count; i++)
            {
                if (player.Screen.GameObjects[i] is Ground ground)
                {
                    if (ground.GetComponent<HitBox>().HitBoxCollider.IsColliding(collider, ground.Position, position))
                        groundList.Add(ground);
                }
            }
            return groundList;
        }

        // is tool touching enemy!?
        protected virtual Enemy CheckEnemyCollision(Vector2 position)
        {

            for (int i = 0; i < player.Screen.GameObjects.Count; i++)
            {
                if (player.Screen.GameObjects[i] is Enemy e)
                {
                    if (e.GetComponent<HitBox>().HitBoxCollider.IsColliding(collider, e.Position, position))
                        return e;
                }
            }
            return null;
        }
    }
}
