﻿using StarlightRiver.Core;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace StarlightRiver.Items.Temple
{
    class TempleLens : SmartAccessory
    {
        public TempleLens() : base("Ancient Lens", "+ 3 % Critical Strike Chance\nCritical strikes inflict glowing") { }
        public override void SafeSetDefaults()
        {
            item.rare = ItemRarityID.Blue;
        }
        public override void SafeUpdateEquip(Player player)
        {
            player.meleeCrit += 3;
            player.rangedCrit += 3;
            player.magicCrit += 3;
        }
        public override bool Autoload(ref string name)
        {
            StarlightPlayer.ModifyHitNPCEvent += ModifyHurtLens;
            StarlightProjectile.ModifyHitNPCEvent += ModifyProjectileLens;
            return true;
        }

        private void ModifyHurtLens(Player player, Item item, NPC target, ref int damage, ref float knockback, ref bool crit)
        {
            if (Equipped(player) && crit)
            {
                target.AddBuff(ModContent.BuffType<Buffs.Illuminant>(), 300);
            }
        }

        private void ModifyProjectileLens(Projectile projectile, NPC target, ref int damage, ref float knockback, ref bool crit, ref int hitDirection)
        {
            if (Equipped(Main.player[projectile.owner]) && crit)
            {
                target.AddBuff(ModContent.BuffType<Buffs.Illuminant>(), 300);
            }
        }
    }
}
