using System;

using GameProject.GameObjects;

using Microsoft.Xna.Framework.Graphics;

namespace GameProject.GameObjects.ObjectComponents
{
    public class ObjectComponent
    {
        // GameObject that component live in xd
        protected GameObject gameObject;

        // Initialize the component
        public ObjectComponent(GameObject gameObject)
        {
            this.gameObject = gameObject;
        }

        // Unloads content
        public virtual void UnloadContent()
        {

        }

        // Update component
        public virtual void Update()
        {

        }

        // Draw component
        public virtual void Draw(SpriteBatch spriteBatch)
        {

        }

        // Draw component
        public virtual void DrawGUI(SpriteBatch spriteBatch)
        {

        }
    }
}
