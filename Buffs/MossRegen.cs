using Terraria;
using Terraria.ModLoader;

namespace StarlightRiver.Buffs
{
    public class MossRegen : ModBuff
    {
        public override void SetDefaults()
        {
            DisplayName.SetDefault("Mending Moss");
            Description.SetDefault("You are being healed!");
        }

        public override void Update(Player player, ref int buffIndex)
        {
            player.lifeRegen += 10;
        }
    }
}