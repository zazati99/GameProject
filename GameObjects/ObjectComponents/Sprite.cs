using System;
using System.Collections.Generic;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

using GameProject.GameObjects;

namespace GameProject.GameObjects.ObjectComponents
{
    public class Sprite : ObjectComponent
    {
        // List of textures
        List<Texture2D> Images;

        // The current displayed image
        public float ImageIndex;

        // How fast the image index will increase per frame
        public float ImageSpeed;

        // Initialize things
        public override void Initialize(GameObject gameObject)
        {
            base.Initialize(gameObject);

            Images = new List<Texture2D>();
            ImageIndex = 0;
            ImageSpeed = 0;
        }

        // Disposes images
        public override void UnloadContent()
        {
            for (int i = 0; i < Images.Count; i++)
            {
                Images[i].Dispose();
            }
        }

        // Update image
        public override void Update()
        {
            // Add Image Speed to Image Index and loop
            if (ImageSpeed > 0)
            {
                if (ImageIndex + ImageSpeed < Images.Count)
                {
                    ImageIndex += ImageSpeed;
                }
                else
                {
                    ImageIndex = ImageIndex + ImageSpeed - Images.Count;
                }
            }
            else if (ImageSpeed < 0)
            {
                if (ImageIndex + ImageSpeed >= 0)
                {
                    ImageIndex += ImageSpeed;
                }
                else
                {
                    ImageIndex = Images.Count + (ImageIndex + ImageSpeed);
                }
            }
        }

        // Draws image
        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw
            (
                Images[(int)ImageIndex],
                gameObject.Position
            );
        }

        // Add existing Texture
        public void AddTexture(Texture2D texture)
        {
            Images.Add(texture);
        }

        // Load and add a texture
        public void AddTexture(ContentManager content, string path)
        {
            Images.Add(content.Load<Texture2D>(path));
        }
    }
}
