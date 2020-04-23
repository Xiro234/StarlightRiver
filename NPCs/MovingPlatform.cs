using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace StarlightRiver.NPCs
{
    abstract class MovingPlatform : ModNPC
    {
        public virtual void SafeSetDefaults() { }
        public sealed override void SetDefaults()
        {
            SafeSetDefaults();

            npc.lifeMax = 1;
            npc.immortal = true;
            npc.noGravity = true;
            npc.knockBackResist = 0; //very very important!! 
            npc.aiStyle = -1;
        }
        public override bool CheckActive()
        {
            return false;
        }
        public virtual void SafeAI() { }
        public sealed override void AI()
        {
            SafeAI();

            foreach (Player player in Main.player)
            {
                if (new Rectangle((int)player.position.X, (int)player.position.Y + (player.height - 2), player.width, 4).Intersects
                (new Rectangle((int)npc.position.X, (int)npc.position.Y, npc.width, 4)) && player.position.Y <= npc.position.Y)
                {
                    player.position += npc.velocity;
                }
            }
        }
    }
}
