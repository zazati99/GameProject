using System;

using Microsoft.Xna.Framework;

using GameProject.GameScreens;
using GameProject.GameObjects.ObjectComponents;
using Microsoft.Xna.Framework.Content;

namespace GameProject.GameObjects
{
    public class Ground : GameObject
    {
        // Test sak (kasnke behåller om det funkar bra)
        public enum GROUND_TYPE
        {
            DIRT,
            STONE
        }

        // Create a type fo ground
        public static Ground MakeGround(GROUND_TYPE groundType)
        {
            Ground ground = null;
            switch (groundType)
            {
                case GROUND_TYPE.DIRT:
                    ground = new Ground();
                    break;
                case GROUND_TYPE.STONE:
                    ground = new Stone();
                    break;
            }
            return ground;
        }

        // lmao hp
        public int groundDurability;

        // Ground type
        public GROUND_TYPE GroundType;

        // Initialize Ground
        public override void Initialize(GameScreen screen)
        {
            base.Initialize(screen);

            HitBox hitBox = new HitBox();
            BoxCollider collider = new BoxCollider();
            collider.Size = new Vector2(32, 32);
            hitBox.SetCollider(collider);
            AddComponent(hitBox);
            hitBox.Solid = true;

            InitializeGround();
        }

        // Initialize stats for specific grounds
        public virtual void InitializeGround()
        {
            GroundType = GROUND_TYPE.DIRT;

            groundDurability = 5;

            Sprite sprite = new Sprite();
            AddComponent(sprite);
            sprite.AddTexture(CreateRectangle(new Vector2(32, 32), Color.Brown));
        }

        // Load Content maymay
        public override void LoadContent(ContentManager content)
        {
            base.LoadContent(content);
        }

        // deal damage to ground
        public void TakeDamage(int damage)
        {
            groundDurability -= damage;
            if (groundDurability <= 0)
            {
                DestroyObject();
            }
        }
    }
}
