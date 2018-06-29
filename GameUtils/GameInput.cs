using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Xna.Framework.Input;

namespace GameProject.GameUtils
{
    public class GameInput
    {

        // Keyboard states
        static KeyboardState currentKeyboardState;
        static KeyboardState oldKeyboardState;

        // ska köras innan något annat i MainGame
        public static void UpdateStart()
        {
            currentKeyboardState = Keyboard.GetState();
        }

        // Ska köras i slutet av update i MainGame
        public static void UpdateEnd()
        {
            oldKeyboardState = currentKeyboardState;
        }

        #region GetInputFunctions

        // är knapp nedtrykt!?!?
        public static bool KeyDown(Keys key)
        {
            return currentKeyboardState.IsKeyDown(key);
        }

        // har Du TRYCKT PÅ KNAPPEN?
        public static bool KeyPressed(Keys key)
        {
            return currentKeyboardState.IsKeyDown(key) && !oldKeyboardState.IsKeyDown(key);
        }

        #endregion

    }
}
