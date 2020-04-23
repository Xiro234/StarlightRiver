using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ModLoader;
using Terraria.ID;

namespace StarlightRiver.Items.Guardian
{
    class ExampleMace : Mace
    {
        public ExampleMace() : base(10, 6, 48, 4) { }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Iron Mace");
        }
        public override void SafeSetDefaults()
        {
            item.damage = 10;
            item.useTime = 15;
            item.useAnimation = 15;
            item.noUseGraphic = true;
            item.useStyle = 1;
            item.UseSound = SoundID.Item1;
            item.noMelee = true;
        }
        public override bool UseItem(Player player)
        {
            SpawnProjectile(ModContent.ProjectileType<ExampleMaceProjectile>(), player);
            return true;
        }
    }
    class ExampleMaceProjectile : MaceProjectile
    {
        public override void SafeSetDefaults()
        {
            projectile.timeLeft = 10;
            projectile.width = 16;
            projectile.height = 16;
        }
    }
}
