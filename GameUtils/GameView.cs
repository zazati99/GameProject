using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameProject.GameUtils
{
    public class GameView
    {

        // View values (position rotation, etc)
        static Vector2 Position; // center position of view in room
        static Vector2 View; // Width and height of View in pixels
        static Vector2 Size; // Size of window on screen
        static float Rotation; // rotation of view in radians

        // Default values
        static Vector2 DefaultView; // Default view
        static Vector2 DefaultSize; // Default size

        // Is needed to change some stuff
        GraphicsDeviceManager graphics;
        
        public GameView(GraphicsDeviceManager graphics)
        {
            this.graphics = graphics;

            Position = new Vector2(384/2, 216/2);

            DefaultView = new Vector2(384, 216);
            View = DefaultView;

            DefaultSize = new Vector2(1280, 720);
            setSize(DefaultSize);

            Rotation = 0;
        }

        // Set Port
        public void setSize(Vector2 size)
        {
            Size = size;

            graphics.PreferredBackBufferWidth = (int)DefaultSize.X;
            graphics.PreferredBackBufferHeight = (int)DefaultSize.Y;
        }

        // Switch between windowed and fullscreen
        public void SwitchFullscreen()
        {
            // Switches and applies
            graphics.IsFullScreen = !graphics.IsFullScreen;
            graphics.ApplyChanges();

            if (graphics.IsFullScreen)
            {
                // sets the view size to screen resolution
                setSize(new Vector2(GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width, GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height));

                // Fixes aspec ratio
                View.Y = View.X * (Size.Y / Size.X);
            }
            else
            {
                // Set default values
                setSize(DefaultSize);
                View = DefaultView;
            }
        }

        // memes the view just right
        public Matrix GetTransformation()
        {
            return Matrix.CreateTranslation(new Vector3(-Position, 0))
                 * Matrix.CreateScale(new Vector3(Size.X / View.X, Size.Y / View.Y, 0))
                 * Matrix.CreateRotationZ(Rotation)
                 * Matrix.CreateTranslation(new Vector3(Size / 2, 0));
        }

        // memes the UI just Right
        public Matrix GetUITransformation()
        {
            return Matrix.CreateScale(new Vector3(Size.X / View.X, Size.Y / View.Y, 0));
        }

        #region GetSetMethods

        // Sets rotation of view
        public static void SetRotation(float rotation)
        {
            Rotation = rotation;
        }

        // Sets position of view
        public static void SetPosition(Vector2 position)
        {
            Position = position;
        }

        // Get rotation
        public static float GetRotation()
        {
            return Rotation;
        }

        // Get Position
        public static Vector2 GetPosition()
        {
            return Position;
        }

        // Get view
        public static Vector2 GetView()
        {
            return View;
        }

        #endregion
    }
}
