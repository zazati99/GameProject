using System;
using System.Collections.Generic;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

using GameProject.GameScreens;
using GameProject.GameObjects.ObjectComponents;

namespace GameProject.GameObjects
{
    [Serializable]
    public class GameObject
    {
        // Game screen that object is in
        public GameScreen Screen;

        // List of object components
        public List<ObjectComponent> ObjectComponents;

        // Position of GameObject in screen
        public Vector2 Position;

        // Initializes components
        public virtual void Initialize(GameScreen screen)
        {
            Screen = screen;
            ObjectComponents = new List<ObjectComponent>();
        }

        // Load Content
        public virtual void LoadContent(ContentManager content)
        {
            
        }

        // Unload content
        public virtual void UnloadContent()
        {
            for (int i = 0; i < ObjectComponents.Count; i++)
            {
                ObjectComponents[i].UnloadContent();
            }
        }

        // Updates components
        public virtual void Update()
        {
            for (int i = 0; i < ObjectComponents.Count; i++)
            {
                ObjectComponents[i].Update();
            }
        }

        // Draws componentes
        public virtual void Draw(SpriteBatch spriteBatch)
        {
            for (int i = 0; i < ObjectComponents.Count; i++)
            {
                ObjectComponents[i].Draw(spriteBatch);
            }
        }

        // Draws gui
        public virtual void DrawGUI(SpriteBatch spriteBatch)
        {
            for (int i = 0; i < ObjectComponents.Count; i++)
            {
                ObjectComponents[i].DrawGUI(spriteBatch);
            }
        }

        #region Object functions

        // Add a component
        public void AddComponent(ObjectComponent objectComponent)
        {
            ObjectComponents.Add(objectComponent);
            objectComponent.Initialize(this);
        }

        // Get specific component
        public T GetComponent<T>()
        {
            object component = null;
            for (int i = 0; i < ObjectComponents.Count; i++)
            {
                if (ObjectComponents[i] is T)
                {
                    component = ObjectComponents[i];
                    return (T)component;
                }
            }
            return (T)component;
        }

        // Destroy this object
        public void DestroyObject()
        {
            Screen.GameObjects.Remove(this);
            UnloadContent();
        }

        // Destroy other object
        public void DestroyObject(GameObject gameObject)
        {
            Screen.GameObjects.Remove(gameObject);
            gameObject.UnloadContent();
        }

        // Create a rectangle texture (should only bhe used for testing)
        public Texture2D CreateRectangle(Vector2 size, Color color)
        {
            Texture2D rect = new Texture2D(MainGame.Graphics.GraphicsDevice, (int)size.X, (int)size.Y);

            Color[] data = new Color[(int)(size.X * size.Y)];
            for (int i = 0; i < data.Length; ++i) data[i] = color;
            rect.SetData(data);

            return rect;
        }

        #endregion
    }
}
