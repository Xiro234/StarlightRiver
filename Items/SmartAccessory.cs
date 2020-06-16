using Terraria;
using Terraria.ModLoader;

namespace StarlightRiver.Items
{
    public abstract class SmartAccessory : ModItem
    {
        public bool Equipped(Player player)
        {
            for (int k = 2; k <= 9; k++)
                if (player.armor[k].type == item.type) return true;
            return false;
        }
        private readonly string ThisName;
        private readonly string ThisTooltip;
        //public override bool CloneNewInstances => true;
        public SmartAccessory(string name, string tooltip) : base()
        {
            ThisName = name;
            ThisTooltip = tooltip;
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
        public virtual void SafeUpdateEquip(Player player) { }
        public sealed override void UpdateEquip(Player player)
        {
            SafeUpdateEquip(player);
        }
    }
}
