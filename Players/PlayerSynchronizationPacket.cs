using WebmilioCommons.Networking.Packets;

namespace StarlightRiver.Players
{
    public class PlayerSynchronizationPacket : ModPlayerNetworkPacket<StarlightPlayer>
    {
        public int Stamina
        {
            get => ModPlayer.Stamina;
            set => ModPlayer.Stamina = value;
        }
    }
}