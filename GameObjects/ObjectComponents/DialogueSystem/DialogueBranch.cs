using Microsoft.Xna.Framework.Graphics;

using System.Collections.Generic;

namespace GameProject.GameObjects.ObjectComponents
{
    public class DialogueBranch
    {
        // List of bloxes
        List<DialogueBox> dialogueBoxes;

        // Dialogue maymay
        Dialogue dialogue;

        // Index of box
        public int DialogueBoxIndex;

        // Concstructor and initialization
        public DialogueBranch(Dialogue dialogue)
        {
            this.dialogue = dialogue;
            dialogueBoxes = new List<DialogueBox>();
            DialogueBoxIndex = 0;
        }

        // Update boxes xd
        public void Update()
        {
            dialogueBoxes[DialogueBoxIndex].Update();
        }

        // Draw Boxes
        public void DrawGUI(SpriteBatch spriteBatch)
        {
            dialogueBoxes[DialogueBoxIndex].DrawGUI(spriteBatch);
        }

        // Add a dialogue box
        public void AddDialogueBox(DialogueBox box)
        {
            dialogueBoxes.Add(box);
        }

        // Change box
        public void ChangeBox()
        {
            DialogueBoxIndex++;
            if (DialogueBoxIndex > dialogueBoxes.Count-1)
            {
                DialogueBoxIndex = 0;
                dialogue.EndDialogue();

                for (int i = 0; i < dialogueBoxes.Count; i++)
                {
                    dialogueBoxes[i].ResetText();
                }
            }
        }
    }
}
