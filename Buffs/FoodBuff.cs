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
            int[] Powers = Main.LocalPlayer.GetModPlayer<FoodBuffHandler>().Powers;
            int[] Buffs = Main.LocalPlayer.GetModPlayer<FoodBuffHandler>().Buffs;

            tip = ("+ " + Powers[0] + getBuffName(Buffs[0]) +
                  ((Buffs[1] != 0) ? "\n+ " + Powers[1] + getBuffName(Buffs[1]) : "") +
                  ((Buffs[2] != 0) ? "\n+ " + Powers[2] + getBuffName(Buffs[2]) : "")
                   );
        }
        public override void Update(Player player, ref int buffIndex)
        {
            player.GetModPlayer<FoodBuffHandler>().Fed = true;
        }

        public string getBuffName(int ID)
        {
            switch (ID)
            {
                case 1: return "% Damage";
                case 6: return " Defense";
            }
            return "ERROR";
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
            player.GetModPlayer<FoodBuffHandler>().Full = true;
        }
    }
}
