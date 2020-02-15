using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ModLoader;
using Terraria.DataStructures;
using Terraria.ID;
using Microsoft.Xna.Framework;

namespace StarlightRiver.NPCs.Boss.VitricBoss
{
    /*
     * This NPC exists to handle the arena elements of the sentinel bossfight (anything that requires the base position of the fighting area)
     */
    public sealed class VitricBossArenaManager : ModNPC
    {
        public override string Texture => "StarlightRiver/Invisible";
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Vitric Boss Arena");
        }
        public override void SetDefaults()
        {
            npc.aiStyle = -1;
            npc.lifeMax = 1;
            npc.damage = 1;
            npc.defense = 0;
            npc.knockBackResist = 0f;
            npc.width = 0;
            npc.height = 0;
            npc.value = 0;
            npc.lavaImmune = true;
            npc.noGravity = true;
            npc.noTileCollide = true;
            npc.immortal = true;
        }
        public override void AI()
        {
            /*
             * AI fields:
             * 0: Timer
             * 1: Phasestate
             */

            //The boss NPC that this arena is tied to
            NPC parent;

            //Finds the active sentinel, NPC kills itself if zero or >1 is found.
            if (Main.npc.Count(n => n.active && n.type == ModContent.NPCType<VitricBoss>()) != 1) { npc.Kill(); return; }
            else parent = Main.npc.FirstOrDefault(n => n.active && n.type == ModContent.NPCType<VitricBoss>());

            //Ticks the timer
            npc.ai[0]++;

            //Creates the arena barrier which prevents players from leaving the fight or attempting to use run-n-gun tactics
            foreach(Player player in Main.player.Where(player => player.active && Vector2.Distance(player.Center, npc.Center) >= 1000))
            {
                player.velocity = Vector2.Normalize(player.Center - npc.Center) * -10;
                player.Hurt(PlayerDeathReason.ByCustomReason(player.name + " tried to escape the sentinel..."), 50, 0);
            }
            for(float k = 0; k <= 5; k ++) //visuals
            {
                float rot = Main.rand.NextFloat(6.28f);
                Dust.NewDustPerfect(npc.Center + Vector2.One.RotatedBy(rot) * 710, ModContent.DustType<Dusts.VitricBossTell>(), Vector2.One.RotatedBy(rot + 1.58f) * 20, 0, new Color(220, 250, 250), 2);
                Dust.NewDustPerfect(npc.Center + Vector2.One.RotatedBy(rot) * 710, ModContent.DustType<Dusts.Air>(), Vector2.One.RotatedBy(rot + 1.58f) * 2, 0, default, 2);
            }
        }
    }
}
