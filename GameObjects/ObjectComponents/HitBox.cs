using System;

using Microsoft.Xna.Framework;

namespace GameProject.GameObjects.ObjectComponents
{
    public class HitBox : ObjectComponent
    {
        // Collider
        public Collider HitBoxCollider;

        // Important variables
        public bool Solid; // will this hitbox interact with solid physics objects?

        // Initialize HitBox
        public override void Initialize(GameObject gameObject)
        {
            base.Initialize(gameObject);
            Solid = false;

            if (gameObject.GetComponent<Physics>() is Physics physics)
            {
                physics.AddHitBox(this);
            }
        }

        // set a collider
        public void SetCollider(Collider collider)
        {
            HitBoxCollider = collider;
        }

        #region Collision Functions

        // Check for collision with objects that has a solid hitbox
        public bool SolidMeeting(Vector2 position)
        {
            for (int i = 0; i < gameObject.Screen.GameObjects.Count; i++)
            {
                GameObject o = gameObject.Screen.GameObjects[i];
                if (o == gameObject) continue;
                
                if (o.GetComponent<HitBox>() is HitBox hitBox)
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

        // Object at place
        public GameObject ObjectMeeting<T>(Vector2 position)
        {
            for (int i = 0; i < gameObject.Screen.GameObjects.Count; i++)
            {
                if (gameObject.Screen.GameObjects[i] is T)
                {
                    GameObject o = gameObject.Screen.GameObjects[i];
                    if (o.GetComponent<HitBox>() is HitBox hitBox)
                    {
                        if (HitBoxCollider.IsColliding(hitBox.HitBoxCollider, position, o.Position))
                            return o;
                    }
                }
            }

            return null;
        }

        #endregion
    }
}
