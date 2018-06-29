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
   public class MemeScreen3 : GameScreen
    {

        Texture2D connection;
        Vector2 connectloss;

        public MemeScreen3()
        {
            connectloss = new Vector2(50, 323);
        }

        public override void LoadContent(ContentManager content)
        {
            base.LoadContent(content);
            connection = Content.Load<Texture2D>("connection");
        }

        public override void UnloadContent()
        {
            base.UnloadContent();
            connection.Dispose();
        }

        public override void Update()
        {
            base.Update();

            if (Keyboard.GetState().IsKeyDown(Keys.Escape))
            {
                ScreenManager.Instance.ChangeScreen(new MemeScreen());
            }

        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(connection, connectloss);
        }


    }
}
