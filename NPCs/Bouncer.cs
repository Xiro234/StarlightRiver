using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using StarlightRiver.Ability;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace StarlightRiver.NPCs
{
    class Bouncer : ModNPC
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("");
        }
        public override void SetDefaults()
        {
            npc.width = 22;
            npc.height = 22;
            npc.knockBackResist = 0f;
            npc.aiStyle = -1;
            npc.lifeMax = 1;
            npc.noGravity = true;
            npc.dontTakeDamage = true;
            npc.dontTakeDamageFromHostiles = true;
            npc.dontCountMe = true;
            npc.behindTiles = true;
        }
        public override void AI()
        {
            npc.TargetClosest(true);
            Player player = Main.player[npc.target];

            if (npc.Hitbox.Intersects(player.Hitbox) && player.GetModPlayer<AbilityHandler>().ability is Dash && player.GetModPlayer<AbilityHandler>().ability.Active)
            {
                player.GetModPlayer<AbilityHandler>().ability.Active = false;

                if (player.velocity.Length() != 0)
                {
                    player.velocity = Vector2.Normalize(player.velocity) * -15f;
                }

                Main.PlaySound(SoundID.Shatter, npc.Center);
                for (int k = 0; k <= 30; k++)
                {
                    int dus = Dust.NewDust(npc.position, 48, 32, mod.DustType("Glass"), Main.rand.Next(-16, 15), Main.rand.Next(-16, 15), 0, default, 1.3f);
                    Main.dust[dus].customData = npc.Center;
                }
            }
            if(Main.tile[(int)npc.Center.X / 16, (int)npc.Center.Y / 16].type != mod.TileType("Bounce"))
            {
                Helper.Kill(npc);
            }
        }
    }
}
