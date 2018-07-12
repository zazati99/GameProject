using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Graphics;

namespace GameProject.GameObjects.ObjectComponents
{
    public class Dialogue : ObjectComponent
    {
        // Dialogue branches
        public Dictionary<string, DialogueBranch> DialogueBranches;

        // Key for branches
        public string Key;

        // Is the Dialogue started?
        bool dialogueStarted;

        // Constructor and initialization
        public Dialogue(GameObject gameObject) : base(gameObject)
        {
            DialogueBranches = new Dictionary<string, DialogueBranch>();
            Key = "";
        }

        // Update things
        public override void Update()
        {
            if (dialogueStarted)
                DialogueBranches[Key].Update();
        }

        // Draw the dialogue
        public override void DrawGUI(SpriteBatch spriteBatch)
        {
            if (dialogueStarted)
                DialogueBranches[Key].DrawGUI(spriteBatch);
        }

        // Add a dialogue branch
        public void AddDialogueBranch(string key, DialogueBranch branch)
        {
            DialogueBranches.Add(key, branch);
        }

        // Start dialogue
        public void StartDialogue()
        {
            dialogueStarted = true;
        }

        // End dialogue
        public void EndDialogue()
        {
            dialogueStarted = false;
        }
    }
}
