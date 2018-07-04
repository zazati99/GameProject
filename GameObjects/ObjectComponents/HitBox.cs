using System;

using Microsoft.Xna.Framework;

namespace GameProject.GameObjects.ObjectComponents
{
    public class HitBox : ObjectComponent
    {
        // Collider
        Collider collider;

        // Important variables
        public bool Solid; // will this hotbox interact with solid physics objects?

        // Initialize HitBox
        public override void Initialize(GameObject gameObject)
        {
            base.Initialize(gameObject);
            Solid = false;

            Physics physics = gameObject.GetComponent<Physics>();
            if (physics != null)
            {
                physics.AddHitBox(this);
            }
        }

        // set a collider
        public void SetCollider(Collider collider)
        {
            this.collider = collider;
        }

        #region Collision Functions

        // Check for collision with objects that has a solid hitbox
        public bool SolidMeeting(Vector2 position)
        {
            for (int i = 0; i < gameObject.Screen.GameObjects.Count; i++)
            {
                GameObject o = gameObject.Screen.GameObjects[i];
                HitBox hitBox = o.GetComponent<HitBox>();

                if (o == gameObject) continue;
                
                if (hitBox != null)
                {
                    if (hitBox.Solid)
                    {
                        if (collider.IsColliding(hitBox.collider, position, o.Position))
                        {
                            return true;
                        }
                    }
                }
            }
            return false;
        }

        // Get the solid object at specific point
        public GameObject SolidAtPoint(Vector2 point)
        {
            GameObject o = null;

            for (int i = 0; i < gameObject.Screen.GameObjects.Count; i++)
            {
                GameObject temp = gameObject.Screen.GameObjects[i];
                HitBox hitBox = temp.GetComponent<HitBox>();

                if (hitBox != null)
                {
                    if (hitBox.Solid)
                    {
                        if (hitBox.collider.IsCollidingWithPoint(temp.Position, point))
                        {
                            return o;
                        }
                    }
                }
            }

            return o;
        }

        #endregion
    }
}
