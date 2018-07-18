using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

using GameProject.GameUtils;

namespace GameProject.GameScreens
{
    public class ScreenBackground
    {
        // le texture
        Texture2D texture;

        // Iterate meme
        Point iterateAmount;

        // Initialize and constructor
        public ScreenBackground()
        {

        }

        // Load the content dude
        public void LoadContent(ContentManager content, string path)
        {
            texture = content.Load<Texture2D>("Images/Background/dirt_background");
            iterateAmount = new Point((int)(GameView.GetView().X / texture.Width) + 3, (int)(GameView.GetView().Y / texture.Height) + 3);
        }

        // Unload content
        public void UnloadContent()
        {
            texture.Dispose();
        }

        // Draw things
        public void Draw(SpriteBatch spriteBatch)
        {
            // CameraPosition thing
            Vector2 StartPosition = GameView.GetPosition() - GameView.GetView() / 2;
            StartPosition.X = ((int)(StartPosition.X / texture.Width)) * texture.Width - texture.Width;
            StartPosition.Y = ((int)(StartPosition.Y / texture.Height)) * texture.Height - texture.Height;

            // iterate and draw
            for (int y = 0; y < iterateAmount.Y; y++)
            {
                for (int x = 0; x < iterateAmount.X; x++)
                {
                    spriteBatch.Draw(texture, StartPosition +  new Vector2(x * texture.Width, y * texture.Height), layerDepth: 1);
                }
            }
        }
    }
}
