using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using StarlightRiver.Abilities;
using Terraria.ID;

namespace StarlightRiver.Items.Accessories
{
    public class StaminaRing : SmartAccessory
    {
        public StaminaRing() : base("Band of Endurance", "Increased Stamina Regeneration") { }
        public override void SafeSetDefaults() { item.rare = ItemRarityID.Blue; }
        public override void SafeUpdateEquip(Player player)
        {
            AbilityHandler mp = player.GetModPlayer<AbilityHandler>();
            mp.StatStaminaMaxTemp += 1;
        }
    }
}
