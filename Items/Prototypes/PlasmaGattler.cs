using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ModLoader;
using Terraria.ID;

namespace StarlightRiver.Items.Prototypes
{
    class PlasmaGattler : PrototypeWeapon
    {
        public PlasmaGattler() : base(3000, BreakType.Time) { }
        private int Heat { get; set; }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Plasmic Converter");
            Tooltip.SetDefault("'convert' your foes into dust\nless accurate over time");
        }
        public override void SetDefaults()
        {
            item.damage = 20;
            item.useStyle = 4;
            item.useTime = 5;
            item.useAnimation = 5;
            item.UseSound = SoundID.Item75;
            item.autoReuse = true;
        }
        public override bool SafeUseItem(Player player)
        {
            player.channel = true;
            Vector2 dir = Vector2.Normalize(player.Center - Main.MouseWorld);
            Projectile.NewProjectile(player.Center + dir * -10, dir.RotatedByRandom(Heat / 250f) * -4, ModContent.ProjectileType<Projectiles.WeaponProjectiles.PlasmaGattlerPulse>(), 
                item.damage, item.knockBack, player.whoAmI, Heat);
            //Main.NewText(Heat, new Color(Heat, 100, 200 - Heat));
            if(Heat <= 200) Heat += 10;
            return true;
        }
        public override void SafeUpdateInventory(Player player)
        {
            if (!player.channel && Heat > 0) Heat--;
        }
    }
}
