using System;

using Microsoft.Xna.Framework;

namespace GameProject.GameObjects.ObjectComponents
{
    public class BoxCollider : Collider
    {
        // Size
        public Vector2 Size;

        // Collision
        public override bool IsColliding(Collider collider, Vector2 p1, Vector2 p2)
        {
            return collider.Collision(this, p2, p1);
        }

        // Collision with point
        public override bool IsCollidingWithPoint(Vector2 p1, Vector2 point)
        {
            return p1.X < point.X &&
                   p1.X + Size.X > point.X &&
                   p1.Y < point.Y  &&
                   p1.Y + Size.Y > point.Y;
        }

        // Collision code
        public override bool Collision(BoxCollider otherCollider, Vector2 p1 ,Vector2 p2)
        {
            return p1.X < p2.X + otherCollider.Size.X &&
                   p1.X + Size.X > p2.X &&
                   p1.Y < p2.Y + otherCollider.Size.Y &&
                   p1.Y + Size.Y > p2.Y;
        }
    }
}
