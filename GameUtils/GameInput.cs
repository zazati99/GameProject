using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace GameProject.GameUtils
{
    public class GameInput
    {

        // Keyboard states
        static KeyboardState currentKeyboardState;
        static KeyboardState oldKeyboardState;

        // GamePad States
        static GamePadState currentGamePadState;
        static GamePadState oldGamePadState;

        // Inputs
        public static Input Right = new Input(Keys.Right, Buttons.DPadRight);
        public static Input Left = new Input(Keys.Left, Buttons.DPadLeft);
        public static Input Up = new Input(Keys.Up, Buttons.DPadUp);
        public static Input Down = new Input(Keys.Down, Buttons.DPadDown);
        public static Input Jump = new Input(Keys.Z, Buttons.A);
        public static Input Dig = new Input(Keys.X, Buttons.X);

        public static Vector2 LeftStick; // Left stick on controller

        // Controller mode
        public static bool ControllerMode;

        // ska köras innan något annat i MainGame
        public static void UpdateStart()
        {
            // Get keyboardstates
            currentKeyboardState = Keyboard.GetState();
            currentGamePadState = GamePad.GetState(0);

            // Check if controller or keyboard mode
            if (currentGamePadState.IsConnected)
            {
                bool keyboardButtonPressed = currentKeyboardState.GetPressedKeys().Length > 0;
                GamePadState emptyInput = new GamePadState(Vector2.Zero, Vector2.Zero, 0, 0, 0);
                if (currentGamePadState != emptyInput && !keyboardButtonPressed)
                {
                    ControllerMode = true;
                }
                else if (keyboardButtonPressed) ControllerMode = false;
            }
            else ControllerMode = false;

            // Get left thumbsitck
            LeftStick.X = GameMath.Clamp(currentGamePadState.ThumbSticks.Left.X * 1.1f, -1, 1);
            LeftStick.Y = GameMath.Clamp(currentGamePadState.ThumbSticks.Left.Y * 1.1f, -1, 1);
        }

        // Ska köras i slutet av update i MainGame
        public static void UpdateEnd()
        {
            oldKeyboardState = currentKeyboardState;
            oldGamePadState = currentGamePadState;
        }

        #region Get Input Functions

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

        // Är gamepad knappen nere?!?!
        public static bool ButtonDown(Buttons button)
        {
            return currentGamePadState.IsButtonDown(button);
        }

        // Har du tryckt på Gamepad knappen?!?!
        public static bool ButtonPressed(Buttons button)
        {
            return currentGamePadState.IsButtonDown(button) && !oldGamePadState.IsButtonDown(button);
        }

        // Input Pressed
        public static bool InputPressed(Input input)
        {
            return KeyPressed(input.Key) || ButtonPressed(input.Button);
        }

        // Input Down
        public static bool InputDown(Input input)
        {
            return KeyDown(input.Key) || ButtonDown(input.Button);
        }

        #endregion

    }
}
