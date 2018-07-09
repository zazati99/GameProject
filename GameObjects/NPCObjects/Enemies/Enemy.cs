using GameProject.GameObjects.ObjectComponents;
using GameProject.GameScreens;

using Microsoft.Xna.Framework;

namespace GameProject.GameObjects
{
    public class Enemy : NPC
    {
        // HP of enemy
        public int HP;

        // Initialize stuff
        public override void Initialize(GameScreen screen)
        {
            base.Initialize(screen);

            HitBox hitBox = new HitBox();
            BoxCollider collider = new BoxCollider();
            collider.Size = new Vector2(32, 64);
            hitBox.SetCollider(collider);
            AddComponent(hitBox);

            Physics physics = new Physics();
            AddComponent(physics);
            physics.Solid = true;
            physics.GravityEnabled = true;

            Sprite sprite = new Sprite();
            AddComponent(sprite);
            sprite.AddTexture(CreateRectangle(new Vector2(32, 64), Color.Purple));

            HP = 3;
        }

        // Take damage
        public virtual void TakeDamage(int damage)
        {
            HP -= damage;
            if (HP <= 0) DestroyObject();
        }
    }
}
