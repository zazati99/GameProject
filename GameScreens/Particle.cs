using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GameProject.GameScreens
{
    public class Particle
    {
        // Particle system this boy belongs to
        ScreenParticleSystem system;

        // le texture
        public Texture2D Texture;

        // particlee properties
        public Vector2 Position;
        public Vector2 Speed;
        public int LifeSpan;

        // Constructor
        public Particle(ScreenParticleSystem system)
        {
            this.system = system;
        }

        // Update particle
        public void Update()
        {
            if ((--LifeSpan) <= 0) system.Particles.Remove(this);
            Position += Speed;
        }

        // Draw particle
        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw
            (
                Texture,
                Position
            );
        }
    }
}
