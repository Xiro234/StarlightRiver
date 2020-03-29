using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace StarlightRiver.Items.Herbology
{
    public class Bottle : ModItem
    {
        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("A bottle made of thick vitric glass");
            DisplayName.SetDefault("Vitric Bottle");
        }

        public override void SetDefaults()
        {
            item.width = 16;
            item.height = 16;
            item.maxStack = 250;
            item.rare = 2;
        }
    }

    public class QuickPotion : ModItem
    {
        private readonly string ItemName;
        private readonly string ItemTooltip;
        private readonly int Time;
        private readonly int BuffID;
        private readonly int Rare;
        public QuickPotion(string name, string tooltip, int time, int buffID, int rare = 1)
        {
            ItemName = name;
            ItemTooltip = tooltip;
            Time = time;
            BuffID = buffID;
            Rare = rare;
        }
        public override bool Autoload(ref string name)
        {
            return GetType().IsSubclassOf(typeof(QuickPotion));
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault(ItemName);
            Tooltip.SetDefault(ItemTooltip);
        }
        public override void SetDefaults()
        {
            item.width = 20;
            item.height = 28;
            item.useStyle = ItemUseStyleID.EatingUsing;
            item.useAnimation = 15;
            item.useTime = 15;
            item.useTurn = true;
            item.UseSound = SoundID.Item3;
            item.maxStack = 30;
            item.consumable = true;
            item.rare = Rare;
            item.value = Item.buyPrice(gold: 1);
            item.buffType = BuffID;
            item.buffTime = Time;
        }
    }

}
