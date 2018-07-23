using System;

using GameProject.GameObjects.ObjectComponents;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;

namespace GameProject.GameObjects
{
    public class StartShovel : MiningTool
    {
        // Constructor
        public StartShovel(PlayerObject player) : base(player)
        {
            MiningSpeed = 30;
            MiningDamage = 1;
        }

        public override void LoadContent(ContentManager content)
        {
            base.LoadContent(content);

            // Load Sprite
            Sprite miningSprite = new Sprite(player);

            miningSprite.AddTexture(player.Screen.Content, "Images/Sprites/Player/Mining/Shovel/player_mining1");
            miningSprite.AddTexture(player.Screen.Content, "Images/Sprites/Player/Mining/Shovel/player_mining2");
            miningSprite.AddTexture(player.Screen.Content, "Images/Sprites/Player/Mining/Shovel/player_mining2");
            miningSprite.AddTexture(player.Screen.Content, "Images/Sprites/Player/Mining/Shovel/player_mining3");
            miningSprite.AddTexture(player.Screen.Content, "Images/Sprites/Player/Mining/Shovel/player_mining4");
            miningSprite.AddTexture(player.Screen.Content, "Images/Sprites/Player/Mining/Shovel/player_mining5");

            miningSprite.SpriteOffset = new Vector2(-22, -28);
            miningSprite.ImageSpeed = 1f / (MiningSpeed / 6f);

            player.SpriteManager.AddSprite("miningSprite", miningSprite);
        }
    }
}
