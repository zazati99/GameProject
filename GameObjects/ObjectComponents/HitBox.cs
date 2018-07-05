using System;

using Microsoft.Xna.Framework;

namespace GameProject.GameObjects.ObjectComponents
{
    public class HitBox : ObjectComponent
    {
        // Collider
        public Collider HitBoxCollider;

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
            this.HitBoxCollider = collider;
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
                        if (HitBoxCollider.IsColliding(hitBox.HitBoxCollider, position, o.Position))
                        {
                            return true;
                        }
                    }
                }
            }
            return false;
        }

        #endregion
    }
}
