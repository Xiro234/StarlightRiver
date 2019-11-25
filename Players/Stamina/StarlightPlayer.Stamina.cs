using Terraria.ModLoader;
using WebmilioCommons.Networking;

namespace StarlightRiver.Players
{
    public sealed partial class StarlightPlayer : ModPlayer
    {
        private void PreUpdateStamina()
        {
            if (Stamina > 0 || Stamina < StaminaMaxTemporary)
                Stamina += StaminaRegeneration;
        }


        private void ResetEffectsStamina()
        {
            StaminaMaxTemporary = StaminaMaxPermanent;
            StaminaRegenerationMaxTemporary = StaminaRegenerationMaxPermanent;
        }


        public int Stamina { get; set; }

        public int StaminaMaxTemporary { get; set; }
        public int StaminaMaxPermanent { get; set; }

        public int StaminaRegeneration { get; set; }
        public int StaminaRegenerationMaxPermanent { get; set; }
        public int StaminaRegenerationMaxTemporary { get; set; }
    }
}
