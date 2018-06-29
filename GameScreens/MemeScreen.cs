using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;


namespace GameProject.GameScreens
{
    public class MemeScreen : GameScreen
    {

        Texture2D pepe;

        public override void LoadContent(ContentManager content)
        {
            base.LoadContent(content);
            pepe = Content.Load<Texture2D>("PepeDab");
        }

        public override void Update()
        {
            base.Update();
            
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(pepe, Vector2.Zero);
        }

    }
}
