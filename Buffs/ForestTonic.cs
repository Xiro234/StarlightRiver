using Terraria;
using Terraria.ModLoader;

namespace StarlightRiver.Buffs
{
    public class ForestTonic : ModBuff
    {
        public override void SetDefaults()
        {
            DisplayName.SetDefault("Forest Tonic");
            Description.SetDefault("Immunity to poision + regeneration");
        }

        public override void Update(Player player, ref int buffIndex)
        {
            player.lifeRegen += 2;
            if (player.poisoned)
            {
                player.poisoned = false;
            }
        }
    }
}
