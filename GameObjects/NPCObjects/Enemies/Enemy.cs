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
            collider.Offset = new Vector2(-10, -16);
            hitBox.SetCollider(collider);
            AddComponent(hitBox);

            Physics physics = new Physics(this);
            AddComponent(physics);
            physics.Solid = true;
            physics.GravityEnabled = true;

            Sprite sprite = new Sprite(this);
            AddComponent(sprite);
            sprite.SpriteOffset = new Vector2(-24,-24);
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

            if (Math.Abs(player.Position.X - Position.X) > 32)
            {
                float speed = 2f * Math.Sign(player.Position.X - Position.X);
                HorizontalMovement(speed);
            }
            else
            { 
                StopMoving();
            }

            if (player.Position.Y < Position.Y - 20) Jump();

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
            physics.Velocity.X = GameMath.Approach(physics.Velocity.X, targetSpeed, physics.Grounded ? .25f : .15f);
        }

        //Stop this lmao
        public new void StopMoving()
        {
            Physics physics = GetComponent<Physics>();
            physics.Velocity.X = GameMath.Approach(physics.Velocity.X, 0, physics.Grounded ? .15f : .1f);
        }

        // Le jump
        public new void Jump()
        {
            Physics physics = GetComponent<Physics>();
            if (physics.Grounded) physics.Velocity.Y = -5;
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);
            DrawHitBox(spriteBatch);
            ShapeRenderer.FillRectangle(spriteBatch, Position, Vector2.One, 0, Color.Red);
        }
    }


}
