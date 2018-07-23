using GameProject.GameScreens;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;

namespace GameProject.GameObjects
{
    public class Dirt : Ground
    {
        // Constructo
        public Dirt(GameScreen gameScreen, TileMap tileMap) : base(gameScreen, tileMap)
        {
            GroundType = GROUND_TYPE.DIRT;
            groundDurability = 2;
        }

        public override void LoadContent(ContentManager content)
        {
            // Add particle system
            if (tileMap.ParticleSystems.ContainsKey(GroundType))
            {
                particleSystem = tileMap.ParticleSystems[GroundType];
            }
            else
            {
                ScreenParticleSystem system = new ScreenParticleSystem(Screen);
                system.Acceleration = new Vector2(0, .2f);
                system.AccelerationDeviation = new Vector2(0.1f);
                system.LifeSpan = 20;

                system.PositionDeviation = new Vector2(12, 12);

                system.Textures.Add(CreateRectangle(Vector2.One, new Color(40, 27, 3)));
                system.Textures.Add(CreateRectangle(Vector2.One * 2, new Color(70, 52, 18)));
                system.Textures.Add(CreateRectangle(Vector2.One, new Color(82, 71, 49)));

                tileMap.AddParticleSystem(GroundType, system);
                particleSystem = system;
            }

            // Load content
            LoadGroundTexture(GroundType, "Images/Sprites/Tiles/dirt_sprite");
            LoadGroundDestroySound(GroundType, "Sounds/Effects/Be"); //De finns 3 "bra", "Sne", "Be" och "Lun"
        }
    }
}
