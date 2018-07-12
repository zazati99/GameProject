using System;

using Microsoft.Xna.Framework;

using GameProject.GameObjects;

namespace GameProject.GameObjects.ObjectComponents
{
    class Physics : ObjectComponent
    {
        // Static variables
        public static float GRAVITY = .25f;

        // Important variables
        public bool Solid; // Will this be able to collide with solid HitBoxes?
        public bool GravityEnabled; // Will This object be affected by gravity?
        public bool Grounded;
        public Vector2 Velocity; // Speedy bois

        // HitBox of GameObject
        HitBox objectHitBox;



        // Constructoar and initialization
        public Physics(GameObject gameObject) : base(gameObject)
        {
            Solid = false;
            GravityEnabled = false;
            Grounded = false;

            objectHitBox = gameObject.GetComponent<HitBox>();
        }

        // Update
        public override void Update()
        {
            if (Solid)
            {
                if (objectHitBox != null)
                {
                    // Gravity meme
                    if (GravityEnabled)
                    {
                        if (!objectHitBox.SolidMeeting(new Vector2(gameObject.Position.X, gameObject.Position.Y + Math.Sign(GRAVITY))))
                        {
                            Velocity.Y += GRAVITY;
                            Grounded = false;
                        }
                        else
                        {
                            Grounded = true;
                        }
                    }

                    // Horizontal collision
                    if (objectHitBox.SolidMeeting(new Vector2(gameObject.Position.X + Velocity.X, gameObject.Position.Y)))
                    {
                        gameObject.Position.X = (Velocity.X > 0) ? (int)(gameObject.Position.X) : (int)gameObject.Position.X+1;
                        while (!objectHitBox.SolidMeeting(new Vector2(gameObject.Position.X + Math.Sign(Velocity.X), gameObject.Position.Y)))
                        {
                            gameObject.Position.X += Math.Sign(Velocity.X);
                        }
                        Velocity.X = 0;
                    } gameObject.Position.X += Velocity.X; // Add velocity to position

                    // Vertical collision
                    if (objectHitBox.SolidMeeting(new Vector2(gameObject.Position.X, gameObject.Position.Y + Velocity.Y)))
                    {
                        gameObject.Position.Y = (Velocity.Y > 0) ? (int)(gameObject.Position.Y) : (int)gameObject.Position.Y + 1;
                        while (!objectHitBox.SolidMeeting(new Vector2(gameObject.Position.X, gameObject.Position.Y + Math.Sign(Velocity.Y))))
                        {
                            gameObject.Position.Y += Math.Sign(Velocity.Y);
                        }
                        Velocity.Y = 0;
                    } gameObject.Position.Y += Velocity.Y; // Add velocity to position
                }
            }
            else
            {
                if (GravityEnabled)
                {
                    Velocity.Y += GRAVITY;
                }

                gameObject.Position += Velocity;
            }
        }

        // Add a HitBox
        public void AddHitBox(HitBox hitBox)
        {
            objectHitBox = hitBox;
        }
    }
}
