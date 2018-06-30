using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;

using GameProject.GameUtils;

namespace GameProject.GameScreens
{
    public class MemeScreen : GameScreen
    {

        Texture2D pepe;
        Texture2D connectionLost;

        Vector2 pepePosition = new Vector2(0,0);
        int speed = 5;

        public override void LoadContent(ContentManager content)
        {
            base.LoadContent(content);
            pepe = Content.Load<Texture2D>("PepeDab");
            connectionLost = Content.Load<Texture2D>("connection");
        }

        public override void UnloadContent()
        {
            base.UnloadContent();
            pepe.Dispose();
        }

        public override void Update()
        {
            base.Update();

            pepePosition.X += speed;
            if (pepePosition.X >= 200 || pepePosition.X <= 0) speed = -speed;
            
            if (GameInput.KeyPressed(Keys.Escape) || GameInput.ButtonPressed(Buttons.B))
            {
                ScreenManager.Instance.ChangeScreen(new MemeScreen2());
            }
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(pepe, pepePosition);
        }

        public override void DrawGUI(SpriteBatch spriteBatch)
        {
            base.DrawGUI(spriteBatch);
            spriteBatch.Draw(connectionLost, Vector2.Zero);
        }

    }
}
