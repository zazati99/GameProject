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
    public class MemeScreen2 : GameScreen
    {

        Texture2D jeff;
        Vector2 jeffPosition;

        public MemeScreen2()
        {
            jeffPosition = new Vector2(200, 200);
        }

        public override void LoadContent(ContentManager content)
        {
            base.LoadContent(content);
            jeff = Content.Load<Texture2D>("jeff");
        }

        public override void UnloadContent()
        {
            base.UnloadContent();
            jeff.Dispose();
        }

        public override void Update()
        {
            base.Update();

            if (Keyboard.GetState().IsKeyDown(Keys.Escape))
            {
                ScreenManager.Instance.ChangeScreen(new MemeScreen3());
            }

        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(jeff, jeffPosition);
        }

    }
}
