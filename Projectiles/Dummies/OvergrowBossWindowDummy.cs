using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using StarlightRiver.Abilities;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace StarlightRiver.Projectiles.Dummies
{
    class OvergrowBossWindowDummy : ModNPC
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("");
        }
        public override string Texture => "StarlightRiver/Invisible";
        public override void SetDefaults()
        {
            npc.width = 100;
            npc.height = 100;
            npc.knockBackResist = 0;
            npc.aiStyle = -1;
            npc.lifeMax = 1;
            npc.immortal = true;
            npc.noGravity = true;
            npc.behindTiles = true;
        }

        public override void AI()
        {
            for(float k = 0; k <= 6.28f; k+= 0.2f)
            {
                Lighting.AddLight(npc.Center + Vector2.One.RotatedBy(k) * 23 * 16, new Vector3(1, 1, 0.7f));
            }
        }

        public override bool PreDraw(SpriteBatch spriteBatch, Color lightColor)
        {
            Vector2 pos = npc.Center;
            Vector2 dpos = pos - Main.screenPosition;

            Texture2D frametex = ModContent.GetTexture("StarlightRiver/Tiles/Overgrow/WindowFrame");
            Texture2D glasstex = ModContent.GetTexture("StarlightRiver/Tiles/Overgrow/WindowGlass");

            spriteBatch.Draw(frametex, dpos, frametex.Frame(), Color.White, 0, frametex.Frame().Size() / 2, 1, 0, 0);
            spriteBatch.Draw(glasstex, dpos, glasstex.Frame(), Color.White * 0.2f, 0, glasstex.Frame().Size() / 2, 1, 0, 0);

            return false;
        }
    }
}
