using StarlightRiver.Food;
using Terraria;
using Terraria.ModLoader;
using System.Linq;

namespace StarlightRiver.Buffs
{
    public class FoodBuff : ModBuff
    {
        public override void SetDefaults()
        {
            DisplayName.SetDefault("Nourished");
            Description.SetDefault("Nourised by rich food, granting:\n");
            Main.debuff[Type] = true;
        }

        public override void ModifyBuffTip(ref string tip, ref int rare)
        {
            FoodBuffHandler mp = Main.LocalPlayer.GetModPlayer<FoodBuffHandler>();
            foreach (Item item in mp.Consumed.Where(n => n.modItem is Ingredient))
            {
                tip += (item.modItem as Ingredient).ItemTooltip + "\n";
            }
        }
        public override void Update(Player player, ref int buffIndex)
        {
            FoodBuffHandler mp = player.GetModPlayer<FoodBuffHandler>();
            foreach (Item item in mp.Consumed.Where(n => n.modItem is Ingredient))
            {
                (item.modItem as Ingredient).BuffEffects(player, mp.Multiplier);
            }
        }
    }

    public class Full : ModBuff
    {
        public override void SetDefaults()
        {
            DisplayName.SetDefault("Full");
            Description.SetDefault("Cannot consume anymore rich food");
            Main.debuff[Type] = true;
        }

        public override void Update(Player player, ref int buffIndex)
        {

        }
    }
}
