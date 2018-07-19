using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;

using GameProject.GameUtils;
using GameProject.GameObjects.ObjectComponents;
using System.Collections.Generic;

namespace GameProject.GameObjects
{
    public class MiningTool
    {
        // lmao who did this?
        Texture2D targetTexture;

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
                bc.Size = new Vector2(12, 12);
                bc.Offset = new Vector2(-6, -13);
            }

            // Mining Timer
            miningTimer = new Timer();

            // stats
            MiningDamage = 1;
            MiningSpeed = 30;

            Sprite miningSprite = new Sprite(player);

            miningSprite.AddTexture(player.Screen.Content, "Images/Sprites/Player/Mining/Shovel/player_mining1");
            miningSprite.AddTexture(player.Screen.Content, "Images/Sprites/Player/Mining/Shovel/player_mining2");
            miningSprite.AddTexture(player.Screen.Content, "Images/Sprites/Player/Mining/Shovel/player_mining3");
            miningSprite.AddTexture(player.Screen.Content, "Images/Sprites/Player/Mining/Shovel/player_mining4");

            miningSprite.SpriteOffset = new Vector2(-22, -28);
            miningSprite.ImageSpeed = .15f;

            player.SpriteManager.AddSprite("miningSprite", miningSprite);
        }

        // Load content
        public virtual void LoadContent(ContentManager content) {

            targetTexture = content.Load<Texture2D>("Images/Sprites/Player/Mining/Crosshair");
        }

        // Determinee a target
        public virtual void DetermineTarget()
        {
            Vector2 hitPoint = player.Position;
            if (GameInput.InputDown(GameInput.Up) || GameInput.LeftStick.Y >= .2f) hitPoint.Y -= 24;
            if (GameInput.InputDown(GameInput.Down) || GameInput.LeftStick.Y <= -.2f) hitPoint.Y += 24;
            if (GameInput.InputDown(GameInput.Left) || GameInput.LeftStick.X <= -.2f) hitPoint.X -= 8;
            if (GameInput.InputDown(GameInput.Right) || GameInput.LeftStick.X >= .2f) hitPoint.X += 8;

            List<Ground> grounds = CheckToolCollisin(hitPoint);
            if (grounds.Count != 0)
            {
                Ground temp = grounds[0];
                float shortestDistance = Vector2.DistanceSquared(player.Position, grounds[0].Position + MainGame.TILE_SIZE / 2);
                for (int i = 1; i < grounds.Count; i++)
                {
                    float distance;
                    if ((distance = Vector2.DistanceSquared(player.Position, grounds[i].Position + MainGame.TILE_SIZE/2)) < shortestDistance)
                    {
                        temp = grounds[i];
                        shortestDistance = distance;
                    }
                }
                target = temp;
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
                    player.SpriteManager.ChangeSprite("miningSprite");
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
                spriteBatch.Draw(targetTexture, target.Position, layerDepth: 0);
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
