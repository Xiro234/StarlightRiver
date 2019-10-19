using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace StarlightRiver.Items
{
    public class QuickMaterial : ModItem
    {
        string Matname;
        string Mattooltip;
        int Maxstack;
        int Value;
        int Rare;

        public QuickMaterial(string name, string tooltip, int maxstack, int value, int rare)
        {
            Matname = name;
            Mattooltip = tooltip;
            Maxstack = maxstack;
            Value = value;
            Rare = rare;
        }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault(Matname);
            Tooltip.SetDefault(Mattooltip); 
        }
        public override void SetDefaults()
        {
            item.width = 16;
            item.height = 16;
            item.maxStack = Maxstack;
            item.value = Value;
            item.rare = Rare;
        }
    }

    public class QuickTileItem : ModItem
    {
        public string Itemname;
        public string Itemtooltip;
        int Tiletype;
        int Rare;
        public QuickTileItem(string name, string tooltip, int placetype, int rare)
        {
            Itemname = name;
            Itemtooltip = tooltip;
            Tiletype = placetype;
            Rare = rare;
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault(Itemname);
            Tooltip.SetDefault(Itemtooltip);
        }

        public override void SetDefaults()
        {
            item.width = 16;
            item.height = 16;
            item.maxStack = 999;
            item.useTurn = true;
            item.autoReuse = true;
            item.useAnimation = 15;
            item.useTime = 10;
            item.useStyle = 1;
            item.consumable = true;
            item.createTile = Tiletype;

        }
    }
}
