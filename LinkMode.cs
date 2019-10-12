using System.IO;
using System.Collections.Generic;
using System.Linq;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.World.Generation;
using Microsoft.Xna.Framework;
using Terraria.GameContent.Generation;
using Terraria.ModLoader.IO;
using Terraria.DataStructures;
using Microsoft.Xna.Framework.Graphics;
using System;
using StarlightRiver.Projectiles;
using StarlightRiver.GUI;

namespace StarlightRiver
{
    public class LinkMode : ModWorld
    {
        public static bool Enabled = false;
        public static int MaxWorldHP = 1;
        public static int WorldHP = 1;

        public override void PreUpdate()
        {
            Enabled = true;
            MaxWorldHP = 0;
            foreach(Player player in Main.player.Where(player => player.active))
            {
                MaxWorldHP += player.statLifeMax2;
            }

            if (Enabled && WorldHP <= 0)
            {
                foreach (Player player in Main.player.Where(player => player.active))
                {
                    player.KillMe(PlayerDeathReason.ByCustomReason("Died with their teammates"), 99999, 0);
                }
                WorldHP = MaxWorldHP / 2;
            }

            if(WorldHP > MaxWorldHP)
            {
                WorldHP = MaxWorldHP;
            }
            Main.NewText(NetworkText.FromLiteral(WorldHP + "/" + MaxWorldHP));
        }

        public override TagCompound Save()
        {
            return new TagCompound
            {
                [nameof(Enabled)] = Enabled
            };           
        }
        public override void Load(TagCompound tag)
        {
            Enabled = tag.GetBool(nameof(Enabled));
            WorldHP = Main.LocalPlayer.statLife;
        }
    }
    public class LinkPlayer : ModPlayer
    {
        public override void PlayerConnect(Player player)
        {
            if (LinkMode.Enabled)
            {
                LinkMode.WorldHP += player.statLife;
            }
        }

        public override bool PreKill(double damage, int hitDirection, bool pvp, ref bool playSound, ref bool genGore, ref PlayerDeathReason damageSource)
        {
            if (LinkMode.Enabled && LinkMode.WorldHP > 0)
            {
                return false;
            }
            return true;
        }

        public override bool PreHurt(bool pvp, bool quiet, ref int damage, ref int hitDirection, ref bool crit, ref bool customDamage, ref bool playSound, ref bool genGore, ref PlayerDeathReason damageSource)
        {
            if (LinkMode.Enabled && LinkMode.WorldHP > 0)
            {
                LinkMode.WorldHP -= damage;
                player.statLife += damage;
            }
            return true;
        }

        int ticker;
        public override void PreUpdate()
        {
            if (LinkMode.Enabled)
            {
                if (ticker++ >= player.lifeRegenCount)
                {
                    if (player.lifeRegen > 0) { LinkMode.WorldHP++; } else if (player.lifeRegen < 0) { LinkMode.WorldHP--; }
                    ticker = 0;
                }
                LinkHP.visible = true;
            }
            else
            {
                LinkHP.visible = false;
            }
        }

        public override void GetHealLife(Item item, bool quickHeal, ref int healValue)
        {
            if ((player.controlUseItem || quickHeal) && LinkMode.Enabled)
            {
                LinkMode.WorldHP += healValue / 2;
            }
        }
    }
}