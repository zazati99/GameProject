using GameProject.GameObjects.ObjectComponents;
using GameProject.GameScreens;
using GameProject.GameUtils;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace GameProject.GameObjects
{
    public class Enemy : NPC , IActivatable
    {
        // HP of enemy
        public int HP;

        public void Activate()
        {
            GetComponent<Dialogue>().StartDialogue();
        }

        public Enemy(GameScreen gameScreen) : base(gameScreen)
        {
            HitBox hitBox = new HitBox(this);
            BoxCollider collider = new BoxCollider();
            collider.Size = new Vector2(17, 40);
            collider.Offset = new Vector2(16, 8);
            hitBox.SetCollider(collider);
            AddComponent(hitBox);

            Physics physics = new Physics(this);
            AddComponent(physics);
            physics.Solid = true;
            physics.GravityEnabled = true;

            Sprite sprite = new Sprite(this);
            AddComponent(sprite);
            sprite.AddTexture(gameScreen.Content, "Images/Sprites/Enemy/gravling");
            
            Dialogue dialogue = new Dialogue(this);

            DialogueBranch dialogueBranch = new DialogueBranch(dialogue);
            dialogue.AddDialogueBranch("", dialogueBranch);

            DialogueBox dialogueBox = new DialogueBox(dialogueBranch);
            dialogueBranch.AddDialogueBox(dialogueBox);
            dialogueBox.skipable = true;
            dialogueBox.textSpeed = 0.01f;
            dialogueBox.SetText("Satans POTATis!\nMy name jefffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffff");

            DialogueBox dialogueBox2 = new DialogueBox(dialogueBranch);
            dialogueBox2.skipable = true;
            dialogueBranch.AddDialogueBox(dialogueBox2);
            dialogueBox2.textSpeed = .5f;
            dialogueBox2.SetText("lmao lmao lmao lmao lmao lmao\nlmao lmao lmao lmao lmao lmao\nlmao lmao lmao lmao lmao lmao");

            ObjectComponents.Add(dialogue);

            HP = 200;
        }

        // Update components and do other logic
        public override void Update()
        {
            GameObject player = GetGameObject<PlayerObject>();

            if (DistanceToObject(player) > 20)
            {
                float speed = 8f * Math.Sign(player.Position.X - Position.X);
                HorizontalMovement(speed);
            }
            else
            { 
                StopMoving();
            }

            base.Update();
        }

        // Take damage
        public virtual void TakeDamage(int damage)
        {
            HP -= damage;
            if (HP <= 0) DestroyObject();
        }

        // Move horizontally
        public new void HorizontalMovement(float targetSpeed)
        {
            Physics physics = GetComponent<Physics>();
            physics.Velocity.X = GameMath.Approach(physics.Velocity.X, targetSpeed, physics.Grounded ? 1f : 1f);
        }

        //Stop this lmao
        public new void StopMoving()
        {
            Physics physics = GetComponent<Physics>();
            physics.Velocity.X = GameMath.Approach(physics.Velocity.X, 0, physics.Grounded ? 1f : 1f);
        }

        /* public override void Draw(SpriteBatch spriteBatch)
         {
             base.Draw(spriteBatch);
             if (GetComponent<HitBox>().HitBoxCollider is BoxCollider col)
             {
                 ShapeRenderer.DrawRectangle(spriteBatch, Position + col.Offset, col.Size, Color.Azure);
             }
         }*/
    }


}
