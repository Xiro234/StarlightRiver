using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace StarlightRiver.NPCs.Hostile
{
    class BoneMine : ModNPC
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Bone Mine");
        }
        public override void SetDefaults()
        {
            npc.width = 50;
            npc.knockBackResist = 0f;
            npc.height = 50;
            npc.lifeMax = 200;
            npc.HitSound = SoundID.NPCHit8;
            npc.DeathSound = SoundID.NPCDeath12;
            npc.noGravity = true;
            npc.damage = 100;
            npc.aiStyle = -1;
            npc.lavaImmune = true;
        }

        public override bool CheckDead()
        {
            return true;
        }

        public override bool PreDraw(SpriteBatch spriteBatch, Color drawColor)
        {
            spriteBatch.Draw(ModContent.GetTexture(Texture), npc.position - Main.screenPosition + new Vector2((float)Math.Sin(npc.ai[0]) * 4f, 0), drawColor);
            return false;
        }

        public override void AI()
        {
            if (npc.ai[1] == 0)
            {
                npc.ai[0] += 0.02f;
                if (npc.ai[0] >= 6.28f) npc.ai[0] = 0;

                if (Main.player.Any(player => Vector2.Distance(player.Center, npc.Center) <= 64)) //arm
                {
                    npc.ai[1] = 1;
                    npc.ai[0] = 0;
                }

                if (Main.player.Any(player => Vector2.Distance(player.Center, npc.Center) <= 128)) //warning ring
                {
                    Dust.NewDustPerfect(npc.Center + Vector2.One.RotatedByRandom(6.28f) * 42, ModContent.DustType<Dusts.Stamina>());
                }
            }
            else
            {
                npc.ai[0]++;
                if(npc.ai[0] % 10 == 0) Main.PlaySound(SoundID.MaxMana, (int)npc.Center.X, (int)npc.Center.Y, 1, 1, 0.5f); //warning beep
                if (npc.ai[0] >= 45) Helper.Kill(npc); //detonate
            }
        }
    }
}
