using GameProject.GameObjects.ObjectComponents;
using GameProject.GameScreens;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using System.Collections.Generic;

namespace GameProject.GameObjects
{
    public class NPC : GameObject, IMovable
    {
        // Constructor and initialization
        public NPC(GameScreen gameScreen) : base(gameScreen)
        {

        }

        public override void Update()
        {
            base.Update();
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);
        }

        public void HorizontalMovement(float targetSpeed)
        {
            throw new System.NotImplementedException();
        }

        public void Jump()
        {
            throw new System.NotImplementedException();
        }

        public void StopMoving()
        {
            throw new System.NotImplementedException();
        }
    }
}
