using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ModLoader;

namespace StarlightRiver.Items.Accessories
{
    public abstract class SmartAccessory : ModItem
    {
        internal bool Equipped { get; set; }
        private readonly string ThisName;
        private readonly string ThisTooltip;
        public SmartAccessory(string name, string tooltip)
        {
            ThisName = name;
            ThisTooltip = tooltip;
        }
        public override bool Autoload(ref string name)
        {
            StarlightPlayer.ResetEffectsEvent += ResetEquip;
            return true;
        }
        private void ResetEquip()
        {
            Equipped = false;
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault(ThisName);
            Tooltip.SetDefault(ThisTooltip);
        }
        public virtual void SafeSetDefaults() { }
        public sealed override void SetDefaults()
        {
            SafeSetDefaults();
            item.width = 32;
            item.height = 32;
            item.accessory = true;
        }

        public virtual void SafeUpdateAccessory(Player player, bool hideVisual) { }
        public sealed override void UpdateAccessory(Player player, bool hideVisual)
        {
            SafeUpdateAccessory(player, hideVisual);
            Equipped = true;
        }
    }
}
