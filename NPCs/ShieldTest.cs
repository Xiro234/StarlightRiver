using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace StarlightRiver.NPCs
{
    public class ShieldTest : ModNPC
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Shielded Guy");
        }
        public override void SetDefaults()
        {
            npc.width = 28;
            npc.knockBackResist = 0f;
            npc.height = 48;
            npc.lifeMax = 500;
            npc.noGravity = true;
            npc.damage = 0;
            npc.defense = 10;
            npc.aiStyle = -1;
            npc.GetGlobalNPC<ShieldHandler>().MaxShield = 50;
            npc.GetGlobalNPC<ShieldHandler>().Shield = 50;
        }

        public override void AI()
        {

        }
    }
}