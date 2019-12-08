using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ModLoader;

namespace StarlightRiver.Items.StarJuice
{
    class BountyKnife : StarjuiceStoringItem
    {
        public BountyKnife() : base(2500) { }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Hunters Dagger");
            Tooltip.SetDefault("Infuse a beast with starlight\nInfused enemies become powerful and gain abilities\nSlain enemies drop crystals");
        }
        public override void SetDefaults()
        {
            item.width = 16;
            item.height = 16;
            item.useStyle = 1;
            item.useAnimation = 10;
            item.useTime = 10;
            item.rare = 3;
            item.shoot = ModContent.ProjectileType<Projectiles.WeaponProjectiles.BountyKnife>();
            item.shootSpeed = 2;
            item.damage = 1;
        }

        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            if (charge == maxCharge)
            {
                charge = 0;
                return true;
            }
            return false;

        }
    }
}
