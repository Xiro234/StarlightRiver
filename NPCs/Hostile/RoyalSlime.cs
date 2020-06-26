using static Terraria.ModLoader.ModContent;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace StarlightRiver.NPCs.Hostile
{
    internal class RoyalSlime : ModNPC
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Royal Servant");
            Main.npcFrameCount[npc.type] = 2;
        }

        public override void SetDefaults()
        {
            npc.width = 32;
            npc.height = 26;
            npc.damage = 18;
            npc.defense = 12;
            npc.lifeMax = 50;
            npc.HitSound = SoundID.NPCHit1;
            npc.DeathSound = SoundID.NPCDeath1;
            npc.value = 500f;
            npc.knockBackResist = 0.2f;
            npc.aiStyle = 1;
            animationType = NPCID.BlueSlime;
        }

        public override Color? GetAlpha(Color drawColor)
        {
            return Lighting.GetColor((int)npc.position.X / 16, (int)npc.position.Y / 16) * 0.75f;
        }

        public override bool PreDraw(SpriteBatch spriteBatch, Color drawColor)
        {
            spriteBatch.Draw(GetTexture("StarlightRiver/NPCs/Hostile/RoyalSlimeCharm"), npc.position - Main.screenPosition + new Vector2(0, 4), Lighting.GetColor((int)npc.position.X / 16, (int)npc.position.Y / 16));
            return true;
        }

        public override void AI()
        {
            npc.TargetClosest(true);
            Player player = Main.player[npc.target];
            if (npc.localAI[3] == 0)
            {
                Dust.NewDust(npc.position, 32, 26, DustType<Dusts.Gold2>());
            }

            if (Vector2.Distance(player.Center, npc.Center) <= 64 && npc.localAI[3] == 0)
            {
                for (float k = 0; k <= 6.28; k += 0.1f) { Dust.NewDustPerfect(npc.Center, DustType<Dusts.Gold2>(), new Vector2((float)Math.Cos(k), (float)Math.Sin(k)) * 4); }
                player.velocity += Vector2.Normalize(player.Center - npc.Center) * 8;
                npc.localAI[3] = 240;
            }

            if (npc.localAI[3] > 0)
            {
                npc.localAI[3]--;
            }
        }

        public override float SpawnChance(NPCSpawnInfo spawnInfo)
        {
            return (spawnInfo.player.ZoneOverworldHeight && Main.dayTime && Math.Abs(spawnInfo.player.position.X - Main.spawnTileX * 16) > (Main.maxTilesX * 16) / 6) ? 1f : 0f;
        }
    }
}