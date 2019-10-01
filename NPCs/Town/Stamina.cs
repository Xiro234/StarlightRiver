using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using StarlightRiver.Ability;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace StarlightRiver.NPCs
{
    class Stamina : ModNPC
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

            if (npc.localAI[0] > 0) { npc.localAI[0]--; }
            else
            {
                Dust.NewDust(npc.position, 16, 16, mod.DustType<Dusts.Stamina>());
            }

            if (npc.Hitbox.Intersects(player.Hitbox) && player.GetModPlayer<AbilityHandler>().ability != null && player.GetModPlayer<AbilityHandler>().ability.Active && npc.localAI[0] == 0)
            {
                player.GetModPlayer<AbilityHandler>().stamina++;
                if (player.GetModPlayer<AbilityHandler>().ability is Float) { (player.GetModPlayer<AbilityHandler>().ability as Float).timer += 59; }
                npc.localAI[0] = 300;

                Main.PlaySound(SoundID.Shatter, npc.Center);
            }

            if (Main.tile[(int)npc.Center.X / 16, (int)npc.Center.Y / 16].type != mod.TileType("StaminaGem"))
            {
                Helper.Kill(npc);
            }
        }
    }
}
