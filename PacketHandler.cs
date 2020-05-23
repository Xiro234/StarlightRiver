using System.IO;
using Terraria.ModLoader;
using StarlightRiver.Abilities;
using Terraria;

namespace StarlightRiver
{
    public partial class StarlightRiver : Mod
    {
        public enum SLRPacketType : byte
        {
            ability = 0
        }
        public override void HandlePacket(BinaryReader reader, int whoAmI)
        {
            switch (reader.ReadByte())
            {
                case (byte)SLRPacketType.ability:
                    Player player = Main.player[reader.ReadInt32()];
                    AbilityHandler mp = player.GetModPlayer<AbilityHandler>();
                    Ability ab = mp.Abilities[reader.ReadInt32()];

                    if (player != Main.LocalPlayer) ab.OnCast();
                    ab.Active = reader.ReadBoolean();
                    ab.Timer = reader.ReadInt32();
                    break;
            }
        }
    }
}
