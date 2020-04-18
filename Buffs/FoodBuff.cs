using StarlightRiver.Food;
using Terraria;
using Terraria.ModLoader;

namespace StarlightRiver.Buffs
{
    public class FoodBuff : ModBuff
    {
        public override void SetDefaults()
        {
            DisplayName.SetDefault("Nourished");
            Description.SetDefault("Erroneous Buff! Please report me to the devs!");
            Main.debuff[Type] = true;
        }

        public override void ModifyBuffTip(ref string tip, ref int rare)
        {

        }
        public override void Update(Player player, ref int buffIndex)
        {
            FoodBuffHandler mp = player.GetModPlayer<FoodBuffHandler>();
            foreach (Item item in mp.Consumed)
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
