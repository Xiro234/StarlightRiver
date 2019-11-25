using StarlightRiver.Players;
using Terraria.DataStructures;

namespace StarlightRiver.Items.Accessories.Stamina
{
    public interface IHandlePlayerPreHurtEquipped
    {
        bool PlayerPreHurtEquipped(StarlightPlayer starlightPlayer, bool pvp, bool quiet, ref int damage, ref int hitDirection, ref bool crit, ref bool customDamage, ref bool playSound, ref bool genGore, ref PlayerDeathReason damageSource);
    }
}