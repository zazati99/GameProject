using System.Collections.Generic;
using Microsoft.Xna.Framework.Graphics;

namespace GameProject.GameObjects.ObjectComponents
{
    public class SpriteManager : ObjectComponent
    {
        // Sprite Dictionary
        Dictionary<string, Sprite> Sprites;

        // current sprite
        string key;

        // Le constructor
        public SpriteManager(GameObject gameObject) : base(gameObject)
        {
            Sprites = new Dictionary<string, Sprite>();
            key = "";
        }

        // Update sprites
        public override void Update()
        {
            Sprites[key].Update();
        }

        // Draw Sprites
        public override void Draw(SpriteBatch spriteBatch)
        {
            Sprites[key].Draw(spriteBatch);
        }

        // Add a sprite
        public void AddSprite(string name, Sprite sprite)
        {
            Sprites.Add(name, sprite);
        }

        // Change sprite
        public void ChangeSprite(string name)
        {
            key = name;
        }

        // Change flip status on all sprites
        public void ChangeFlipOnAllSprites(bool flipStatus)
        {
            foreach (KeyValuePair<string, Sprite> item in Sprites)
            {
                item.Value.FlipSprite = flipStatus;
            }
        }

        // Get current sprite
        public Sprite GetCurrentSprite()
        {
            return Sprites[key];
        }

        // Get sprite
        public Sprite GetSprite(string spriteName)
        {
            return Sprites[spriteName];
        }
    }
}
