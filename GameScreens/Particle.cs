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
        public Vector2 Acceletation;
        public float LifeSpan;

        // Constructor
        public Particle(ScreenParticleSystem system)
        {
            this.system = system;
        }

        // Update particle
        public void Update()
        {
            if ((LifeSpan -= MainGame.GAME_SPEED) <= 0) system.Particles.Remove(this);
            Speed += Acceletation * MainGame.GAME_SPEED;
            Position += Speed * MainGame.GAME_SPEED;
        }

        // Draw particle
        public void Draw(SpriteBatch spriteBatch, float layerDepth)
        {
            spriteBatch.Draw
            (
                Texture,
                Position,
                layerDepth: layerDepth
            );
        }
    }
}
