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

        // Cool variables
        public float ImageIndex; // current image index
        public float ImageSpeed; // speed of animtaion
        public float LayerDepth; // Depth of sprite (bör vara mellan 0.4 och 0.6)
        public Color SpriteColor; // Color of spritee (white is normal)
        public Vector2 SpriteOffset; // position offset

        // Initialize things
        public override void Initialize(GameObject gameObject)
        {
            base.Initialize(gameObject);

            Images = new List<Texture2D>();
            ImageIndex = 0;
            ImageSpeed = 0;
            SpriteColor = Color.White;
            SpriteOffset = Vector2.Zero;
            LayerDepth = 0.5f;
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
                gameObject.Position + SpriteOffset,
                color: SpriteColor,
                layerDepth: LayerDepth
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
