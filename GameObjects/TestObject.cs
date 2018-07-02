using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System.Xml.Serialization;

namespace GameProject.GameObjects
{

    public class TestObject : GameObject
    {

        Texture2D tex;

        public Vector2 kek;

        public override void LoadContent(ContentManager content)
        {
            //base.LoadContent(content);
            tex = content.Load<Texture2D>("jeff");
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            //base.Draw(spriteBatch);
            spriteBatch.Draw(tex, kek);
        }

    }
}
