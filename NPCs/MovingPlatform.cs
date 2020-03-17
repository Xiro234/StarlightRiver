using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;

namespace StarlightRiver.NPCs
{
    class MovingPlatform : ModNPC
    {
        public override bool Autoload(ref string name)
        {
            return GetType().IsSubclassOf(typeof(MovingPlatform));
        }
        public virtual void SafeSetDefaults() { }
        public sealed override void SetDefaults()
        {
            SafeSetDefaults();

            npc.lifeMax = 1;
            npc.immortal = true;
            npc.noGravity = true;
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
