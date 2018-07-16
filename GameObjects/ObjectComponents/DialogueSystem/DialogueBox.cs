using GameProject.GameUtils;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GameProject.GameObjects.ObjectComponents
{
    public class DialogueBox
    {
        // The branch this box belongs to
        DialogueBranch branch;

        // Box variables
        SpriteFont font;
        string fullText;
        string displayedText;
        float charIndex;

        public float textSpeed;
        public bool skipable;

        Vector2 position;
        Vector2 boxSize;
        Vector2 textOffset;

        // Constructor and Initialization
        public DialogueBox(DialogueBranch branch)
        {
            // Dialogue branch
            this.branch = branch;

            // variables
            font = GameFonts.font;
            textSpeed = .1f;
            charIndex = 0;
            skipable = false;

            displayedText = "";
            boxSize = new Vector2(225, 45);
            position = new Vector2(GameView.GetView().X / 2 - boxSize.X / 2, GameView.GetView().Y - boxSize.Y - 10);
            textOffset = new Vector2(4, 2);
        }

        // Update the box and text
        public void Update()
        {
            // Add textSpeed to charIndex
            charIndex += textSpeed;
            if (charIndex >= fullText.Length)
            {
                charIndex = fullText.Length;
            }

            // Change box or skip
            if (GameInput.InputPressed(GameInput.Dig))
            {
                if (charIndex == fullText.Length)
                {
                    branch.ChangeBox();
                } else
                {
                    if (skipable)
                        charIndex = fullText.Length;
                }
            }

            // Makes substring that will be displayed
            displayedText = fullText.Substring(0, (int)charIndex);
        }

        // Draw the box and text
        public void DrawGUI(SpriteBatch spriteBatch)
        {
            ShapeRenderer.FillRectangle(spriteBatch, position, boxSize, 0, Color.Black);

            spriteBatch.DrawString(font, displayedText, position + textOffset, Color.White);
        }

        // Set text
        public void SetText(string text)
        {
            fullText = text;
        }

        // Reset text
        public void ResetText()
        {
            displayedText = "";
            charIndex = 0;
        }
    }
}
