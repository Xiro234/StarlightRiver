using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using StarlightRiver.Abilities;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace StarlightRiver.NPCs.Hostile
{
    class CrystalSlime : ModNPC
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Crystal Slime");
            Main.npcFrameCount[npc.type] = 2;
        }
        public override void SetDefaults()
        {
            npc.width = 48;
            npc.height = 32;
            npc.damage = 10;
            npc.defense = 5;
            npc.lifeMax = 25;
            npc.HitSound = SoundID.NPCHit1;
            npc.DeathSound = SoundID.NPCDeath1;
            npc.value = 10f;
            npc.knockBackResist = 0.6f;
            npc.aiStyle = 1;   
        }
        public override Color? GetAlpha(Color drawColor)
        {
            return Lighting.GetColor((int)npc.position.X / 16, (int)npc.position.Y / 16) * 0.75f;
        }

        bool shielded = true;
        public override void AI()
        {
            npc.TargetClosest(true);
            Player player = Main.player[npc.target];

            if (npc.Hitbox.Intersects(player.Hitbox) && player.GetModPlayer<AbilityHandler>().ability is Dash && player.GetModPlayer<AbilityHandler>().ability.Active)
            {
                if (shielded)
                {
                    shielded = false;
                    npc.velocity += player.velocity * 0.5f;
                    player.GetModPlayer<AbilityHandler>().ability.Active = false;
                    player.velocity *= (player.velocity.X == 0) ? -0.4f : -0.2f;
                    player.immune = true;
                    player.immuneTime = 10;

                    Main.PlaySound(SoundID.Shatter, npc.Center);
                    for(int k = 0; k <= 20; k++)
                    {
                        Dust.NewDust(npc.position, 48, 32, mod.DustType("Glass2"),Main.rand.Next(-3, 2),-3,0,default,1.7f);
                    }
                }
            }

            if(shielded)
            {
                npc.immortal = true;
            }
            else
            {
                npc.immortal = false;
            }
        }

        public override void ModifyHitPlayer(Player target, ref int damage, ref bool crit)
        {
            if(target.GetModPlayer<AbilityHandler>().ability is Dash && target.GetModPlayer<AbilityHandler>().ability.Active)
            {
                target.immune = true;
                target.immuneTime = 5;
            }
        }

        public override float SpawnChance(NPCSpawnInfo spawnInfo)
        {
            return (spawnInfo.player.ZoneRockLayerHeight && Main.tile[spawnInfo.spawnTileX, spawnInfo.spawnTileY].active() && spawnInfo.player.GetModPlayer<BiomeHandler>().ZoneGlass) ? 1f : 0f;
        }

        public override void PostDraw(SpriteBatch spriteBatch, Color drawColor)
        {
            if (shielded)
            {
                Color color = new Color(255, 255, 255) * (float)Math.Sin(LegendWorld.rottime);
                spriteBatch.Draw(ModContent.GetTexture("StarlightRiver/NPCs/Hostile/Crystal"), npc.position - Main.screenPosition + new Vector2(-2, -5), Lighting.GetColor((int)npc.position.X / 16, (int)npc.position.Y / 16));
                spriteBatch.Draw(ModContent.GetTexture("StarlightRiver/NPCs/Hostile/CrystalGlow"), npc.position - Main.screenPosition + new Vector2(-3, -6), color);
            }
        }
    }
}
