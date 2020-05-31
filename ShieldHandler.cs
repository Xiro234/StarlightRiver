﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace StarlightRiver
{
    internal class ShieldHandler : GlobalNPC
    {
        public int MaxShield;
        public int Shield;

        public int MaxRed;
        public int Red;

        public override bool InstancePerEntity => true;

        public override void OnHitByProjectile(NPC npc, Projectile projectile, int damage, float knockback, bool crit)
        {
            if (Shield > 0)
            {
                for (int k = 0; k <= 10; k++)
                {
                    Dust.NewDustPerfect(npc.Center, ModContent.DustType<Dusts.Starlight>(), Vector2.Normalize(npc.Center - projectile.Center).RotatedByRandom(1.1f) * Main.rand.NextFloat(20), 0, default, 0.6f);
                }

                Shield -= damage * 2 / (projectile.GetGlobalProjectile<ShieldBreakingProjectile>().Piercing ? 5 : 10);

                if (Shield <= 0)
                {
                    Main.PlaySound(SoundID.NPCDeath37, npc.Center);
                    for (int k = 0; k <= 30; k++)
                    {
                        Dust.NewDustPerfect(npc.Center, ModContent.DustType<Dusts.Starlight>(), Vector2.One.RotatedByRandom(6.28f) * Main.rand.NextFloat(25));
                    }
                }

                CombatText.NewText(new Rectangle((int)npc.position.X, (int)npc.position.Y + 12, npc.width, 0), new Color(60, 220, 255),
                    damage * 2 / (projectile.GetGlobalProjectile<ShieldBreakingProjectile>().Piercing ? 5 : 10));
                Main.PlaySound(SoundID.NPCHit34, npc.Center);
            }

            if (Red > 0 && projectile.GetGlobalProjectile<ShieldBreakingProjectile>().RedHurting)
            {
                Red -= projectile.damage * 2;
                CombatText.NewText(npc.Hitbox, new Color(250, 100, 100), projectile.damage * 2);
            }
        }

        public override void OnHitByItem(NPC npc, Player player, Item item, int damage, float knockback, bool crit)
        {
            if (Shield > 0)
            {
                for (int k = 0; k <= 10; k++)
                {
                    Dust.NewDustPerfect(npc.Center, ModContent.DustType<Dusts.Starlight>(), Vector2.Normalize(npc.Center - player.Center).RotatedByRandom(1.1f) * Main.rand.NextFloat(20), 0, default, 0.6f);
                }

                Shield -= damage * 2 / (item.GetGlobalItem<ShieldBreakingItem>().Piercing ? 5 : 10);

                if (Shield <= 0)
                {
                    Main.PlaySound(SoundID.NPCDeath37, npc.Center);
                    for (int k = 0; k <= 30; k++)
                    {
                        Dust.NewDustPerfect(npc.Center, ModContent.DustType<Dusts.Starlight>(), Vector2.One.RotatedByRandom(6.28f) * Main.rand.NextFloat(25));
                    }
                }

                CombatText.NewText(new Rectangle((int)npc.position.X, (int)npc.position.Y + 12, npc.width, 0), new Color(60, 220, 255),
                    damage * 2 / (item.GetGlobalItem<ShieldBreakingItem>().Piercing ? 5 : 10));
                Main.PlaySound(SoundID.NPCHit34, npc.Center);
            }

            if (Red > 0 && item.GetGlobalItem<ShieldBreakingItem>().RedHurting)
            {
                Red -= item.damage * 2;
                CombatText.NewText(npc.Hitbox, new Color(250, 100, 100), item.damage * 2);
            }
        }

        public override bool StrikeNPC(NPC npc, ref double damage, int defense, ref float knockback, int hitDirection, ref bool crit)
        {
            if (Shield > 0)
            {
                damage *= (Main.expertMode) ? 0.3f : 0.5f;
            }
            if (Red > 0)
            {
                damage *= 0;
                return false;
            }
            return true;
        }

        public override void PostAI(NPC npc)
        {
            if (Shield > MaxShield) Shield = MaxShield;
            if (Shield < 0) Shield = 0;

            if (Red > MaxRed) Red = MaxRed;
            if (Red < 0) Red = 0;
        }

        public override void PostDraw(NPC npc, SpriteBatch spriteBatch, Color drawColor)
        {
            if (Shield > 0 && npc.modNPC != null)
            {
                Vector2 drawpos = npc.Center - Main.screenPosition;
                Texture2D tex = ModContent.GetTexture(npc.modNPC.Texture);
                spriteBatch.Draw(tex, drawpos, npc.frame, new Color(80, 230, 255) * (0.5f + (float)Math.Sin(LegendWorld.rottime * 2) * 0.2f),
                    npc.rotation, npc.Size / 2, npc.scale + 0.1f + (float)Math.Sin(LegendWorld.rottime * 4) * 0.05f, 0, 0);
            }
            base.PostDraw(npc, spriteBatch, drawColor);
        }

        public override bool? DrawHealthBar(NPC npc, byte hbPosition, ref float scale, ref Vector2 position)
        {
            SpriteBatch spriteBatch = Main.spriteBatch;
            Vector2 drawpos = position - Main.screenPosition;
            Color color = new Color(60, 50 + (int)(Shield / (float)MaxShield * 170f), 255);

            if (Shield > 0)
            {
                Rectangle target = new Rectangle((int)drawpos.X - 16, (int)drawpos.Y + 12, 34, 6);
                spriteBatch.Draw(ModContent.GetTexture("StarlightRiver/GUI/ShieldBar0"), target, color * Lighting.Brightness((int)npc.position.X / 16, (int)npc.position.Y / 16));

                Rectangle target2 = new Rectangle((int)drawpos.X - 17, (int)drawpos.Y + 10, (int)(Shield / (float)MaxShield * 36f), 10);
                spriteBatch.Draw(ModContent.GetTexture("StarlightRiver/GUI/ShieldBar1"), target2, color * Lighting.Brightness((int)npc.position.X / 16, (int)npc.position.Y / 16));
            }

            if (Red > 0)
            {
                int offset = Shield > 0 ? 8 : 0;
                Rectangle target = new Rectangle((int)drawpos.X - 16, (int)drawpos.Y + 12 + offset, 34, 6);
                spriteBatch.Draw(ModContent.GetTexture("StarlightRiver/GUI/ShieldBar0"), target, Color.Red * Lighting.Brightness((int)npc.position.X / 16, (int)npc.position.Y / 16));

                Rectangle target2 = new Rectangle((int)drawpos.X - 17, (int)drawpos.Y + 10 + offset, (int)(Red / (float)MaxRed * 36f), 10);
                spriteBatch.Draw(ModContent.GetTexture("StarlightRiver/GUI/ShieldBar1"), target2, Color.Red * Lighting.Brightness((int)npc.position.X / 16, (int)npc.position.Y / 16));
                return true;
            }
            return base.DrawHealthBar(npc, hbPosition, ref scale, ref position);
        }
    }

    public class ShieldBreakingItem : GlobalItem
    {
        public bool Piercing = false;
        public bool RedHurting = false;
        public override bool InstancePerEntity => true;
        public override bool CloneNewInstances => true;

        public override void SetDefaults(Item item)
        {
            if (item.hammer > 0)
            {
                Piercing = true;
            }
        }

        public override void ModifyTooltips(Item item, List<TooltipLine> tooltips)
        {
            if (Piercing)
            {
                TooltipLine line = new TooltipLine(mod, "Pierce", "2x Damage to Shields")
                {
                    overrideColor = new Color(140, 220, 255)
                };
                tooltips.Add(line);
            }
            if (RedHurting)
            {
                TooltipLine line = new TooltipLine(mod, "RedHurt", "Can Damage [PH]REDHEALTH")
                {
                    overrideColor = new Color(255, 210, 210)
                };
                tooltips.Add(line);
            }
        }
    }

    public class ShieldBreakingProjectile : GlobalProjectile
    {
        public bool Piercing = false;
        public bool RedHurting = false;
        public override bool InstancePerEntity => true;
    }
}