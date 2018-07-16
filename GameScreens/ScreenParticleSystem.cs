using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GameProject.GameScreens
{
    public class ScreenParticleSystem
    {
        // List of particles
        public List<Particle> Particles;

        // constructor
        public ScreenParticleSystem()
        {
            Particles = new List<Particle>();
        }

        // Update particle system
        public void Update()
        {
            for (int i = 0; i < Particles.Count; i++)
            {
                Particles[i].Update();
            }
        }

        // Draw particle system
        public void Draw(SpriteBatch spriteBatch)
        {
            for (int i = 0; i < Particles.Count; i++)
            {
                Particles[i].Draw(spriteBatch);
            }
        }

        // Emit particles
        public void Emit(int amount)
        {
            
        }
    }
}
