using System;

using Microsoft.Xna.Framework;

using GameProject.GameScreens;
using GameProject.GameObjects.ObjectComponents;

namespace GameProject.GameObjects
{
    public class Ground : GameObject
    {
        // INitialize ground
        public override void Initialize(GameScreen screen)
        {
            base.Initialize(screen);

            HitBox hitBox = new HitBox();
            BoxCollider collider = new BoxCollider();
            collider.Size = new Vector2(32, 32);
            hitBox.SetCollider(collider);
            AddComponent(hitBox);
            hitBox.Solid = true;

            Sprite sprite = new Sprite();
            AddComponent(sprite);
            sprite.AddTexture(CreateRectangle(new Vector2(32, 32), Color.BlueViolet));
        }

    }
}
