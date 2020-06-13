using Microsoft.Xna.Framework;
using StarlightRiver.Items.Accessories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace StarlightRiver.Items.Temple
{
    class TempleLens : SmartAccessory
    {
        public TempleLens() : base("Ancient Lens", "+ 3 % Critical Strike Chance\nCritical strikes inflict glowing") { }
        public override bool Autoload(ref string name)
        {
            StarlightPlayer.PreHurtEvent += PreHurtLens;
            return true;
        }
        private bool PreHurtLens(Player player, bool pvp, bool quiet, ref int damage, ref int hitDirection, ref bool crit, ref bool customDamage, ref bool playSound, ref bool genGore, ref PlayerDeathReason damageSource)
        {
            if (Equipped)
            {
                Main.NewText("Hey look ModPlayer hook dependant code in an Accessorie's ModItem class!");
            }
            return true;
        }
        public override void SafeSetDefaults()
        {
            item.rare = ItemRarityID.Blue;
        }
        public override void SafeUpdateAccessory(Player player, bool hideVisual)
        {
            player.meleeCrit += 3;
            player.rangedCrit += 3;
            player.magicCrit += 3;
        }
    }
}
