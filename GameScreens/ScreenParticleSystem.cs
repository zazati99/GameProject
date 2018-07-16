using System;
using System.Collections.Generic;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using GameProject.GameUtils;

namespace GameProject.GameScreens
{
    public class ScreenParticleSystem
    {
        // Screen that this systeme belongs to
        GameScreen screen;

        // List of particles
        public List<Particle> Particles;

        // Particle textures
        public List<Texture2D> Textures;

        // System depth
        float LayerDepth;

        // particle properties
        public Vector2 Position;
        public Vector2 PositionDeviation;
        public Vector2 Speed;
        public Vector2 SpeedDeviation;
        public Vector2 Acceleration;
        public Vector2 AccelerationDeviation;
        public int LifeSpan;
        public int LifeSpanDeviation;

        // constructor
        public ScreenParticleSystem(GameScreen screen)
        {
            this.screen = screen;

            Particles = new List<Particle>();
            Textures = new List<Texture2D>();

            LayerDepth = .5f;

            LifeSpan = 30;
            LifeSpanDeviation = 0;
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
            for (int i = 0; i < amount; i++)
            {
                Particle particle = new Particle(this);

                particle.Texture = Textures[GameMath.RandomRange(0, Textures.Count)];

                particle.Position = Position + new Vector2(GameMath.RandomRange((int)-PositionDeviation.X / 2, (int)PositionDeviation.X / 2), GameMath.RandomRange((int)-PositionDeviation.Y / 2, (int)PositionDeviation.Y / 2));
                particle.Speed = Speed + new Vector2(GameMath.RandomRange((int)-SpeedDeviation.X / 2, (int)SpeedDeviation.X / 2), GameMath.RandomRange((int)-SpeedDeviation.Y / 2, (int)SpeedDeviation.Y / 2));
                particle.Acceletation = Acceleration + new Vector2(GameMath.RandomRange((int)-AccelerationDeviation.X / 2, (int)AccelerationDeviation.X / 2), GameMath.RandomRange((int)-AccelerationDeviation.Y / 2, (int)AccelerationDeviation.Y / 2));
                particle.LifeSpan = LifeSpan + GameMath.RandomRange(-LifeSpanDeviation / 2, LifeSpanDeviation / 2);
                Particles.Add(particle);
            } 
        }
    }
}
