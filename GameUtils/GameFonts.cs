using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace GameProject.GameUtils
{
    public class GameFonts
    {
        // GameFonts xd
        public static SpriteFont font;

        // Load the fonts
        public static void LoadContent(ContentManager content)
        {
            font = content.Load<SpriteFont>("font");
        }
    }
}
