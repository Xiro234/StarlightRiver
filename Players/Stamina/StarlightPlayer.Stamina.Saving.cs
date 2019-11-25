using Terraria.ModLoader.IO;

namespace StarlightRiver.Players
{
    public sealed partial class StarlightPlayer
    {
        private void SaveStamina(TagCompound tag)
        {
            tag.Add(nameof(Stamina), Stamina);
            tag.Add(nameof(StaminaMaxPermanent), StaminaMaxPermanent);
            tag.Add(nameof(StaminaRegenerationMaxPermanent), StaminaRegenerationMaxPermanent);
        }

        private void LoadStamina(TagCompound tag)
        {
            Stamina = tag.GetInt(nameof(Stamina));
            StaminaMaxPermanent = tag.GetInt(nameof(StaminaMaxPermanent));
            StaminaRegenerationMaxPermanent = tag.GetInt(nameof(StaminaRegenerationMaxPermanent));
        }
    }
}
