using System;

using GameProject.GameObjects;

using Microsoft.Xna.Framework;

namespace GameProject.GameObjects.ObjectComponents
{
    public class Collider
    {
        // Returns true if colliding with other Collider
        public virtual bool IsColliding(Collider collider, Vector2 p1, Vector2 p2)
        {
            return false;
        }

        // Collision with point
        public virtual bool IsCollidingWithPoint(Vector2 p1, Vector2 point)
        {
            return false;
        }

        // Collision with speecific collider
        public virtual bool Collision(BoxCollider boxCollider, Vector2 p1, Vector2 p2)
        {
            return false;
        }
    }
}
