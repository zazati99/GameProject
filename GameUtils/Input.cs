using Microsoft.Xna.Framework.Input;

namespace GameProject.GameUtils
{
    public class Input
    {
        // Values
        public Keys Key;
        public Buttons Button;

        // Constructor meme
        public Input(Keys key, Buttons button)
        {
            Key = key;
            Button = button;
        }
        public Input()
        {

        }
    }
}
