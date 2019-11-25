using StarlightRiver.Players;
using Terraria;
using Terraria.DataStructures;
using WebmilioCommons.Items.Standard;

namespace StarlightRiver.Items.Accessories.Stamina
{
    public sealed class BandofStamina : StandardAccessory, IHandlePlayerPreHurtEquipped
    {
        public BandofStamina() : base("Band of Stamina", "Increases stamina regeneration", 16, 16)
        {
        }


        public bool PlayerPreHurtEquipped(StarlightPlayer starlightPlayer, bool pvp, bool quiet, ref int damage, ref int hitDirection, ref bool crit, ref bool customDamage, ref bool playSound, ref bool genGore, ref PlayerDeathReason damageSource)
        {
            starlightPlayer.Stamina += 1;
            return true;
        }
    }
}