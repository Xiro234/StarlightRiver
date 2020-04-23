using Microsoft.Xna.Framework;
using System.Linq;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace StarlightRiver.Items.Celumite
{
    class CelumiteSword : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Arc Cleaver");
            Tooltip.SetDefault("Accumulates charge on hit \nRight click to release");
        }
        public override void SetDefaults()
        {
            item.damage = 40;
            item.melee = true;
            item.width = 36;
            item.height = 38;
            item.useTime = 30;
            item.useAnimation = 30;
            item.useStyle = 1;
            item.knockBack = 2f;
            item.value = 10000;
            item.rare = 3;
            item.UseSound = SoundID.Item1;
            item.autoReuse = false;
            item.useTurn = true;
        }
        public override bool AltFunctionUse(Player player)
        {
            return true;
        }
        public override bool UseItem(Player player)
        {
            if (player.altFunctionUse == 2)
            {
                int index = Projectile.NewProjectile(Main.MouseWorld, Vector2.Zero, ModContent.ProjectileType<Projectiles.WeaponProjectiles.LightningNode>(), 120, 0, player.whoAmI, 1, 500);
                NPC npc = Main.npc.FirstOrDefault(n => n.Hitbox.Contains(Main.MouseWorld.ToPoint()));
                Helper.DrawElectricity(player.Center, npc == null ? Main.projectile[index].Center : npc.Center, ModContent.DustType<Dusts.Electric>());
            }
            return true;
        }
    }
}
