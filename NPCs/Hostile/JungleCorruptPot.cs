using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace StarlightRiver.NPCs.Hostile
{
    public class JungleCorruptPot : ModNPC
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Corrupted Pot");
        }
        public override void SetDefaults()
        {
            npc.width = 32;
            npc.knockBackResist = 0f;
            npc.height = 32;
            npc.lifeMax = 1;
            npc.noGravity = true;
            npc.damage = 0;
            npc.aiStyle = -1;
            npc.immortal = true;
        }

        public override void AI()
        {

        }

        public override int SpawnNPC(int tileX, int tileY)
        {
            int plant = NPC.NewNPC(tileX, tileY, ModContent.NPCType<JungleCorruptSnatcher>());
            (Main.npc[plant].modNPC as JungleCorruptSnatcher).pot = this;
            return NPC.NewNPC(tileX, tileY, npc.type);
        }

        public override float SpawnChance(NPCSpawnInfo spawnInfo)
        {
            return 4;
        }
    }

    public class JungleCorruptSnatcher : ModNPC
    {
        public JungleCorruptPot pot;
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Soul Snatcher");
        }
        public override void SetDefaults()
        {
            npc.width = 32;
            npc.knockBackResist = 0f;
            npc.height = 32;
            npc.lifeMax = 350;
            npc.noGravity = true;
            npc.damage = 20;
            npc.aiStyle = -1;
        }

        public override void AI()
        {
            Vector2 offset = new Vector2(10, 10);
            if(pot != null) npc.position = pot.npc.position + offset;
        }

        public override bool PreDraw(SpriteBatch spriteBatch, Color drawColor)
        {
            if (pot != null)
            {
                Vector2 pos = Vector2.Lerp(npc.Center, pot.npc.Center, 0.5f);
                spriteBatch.Draw(ModContent.GetTexture("StarlightRiver/NPCs/Hostile/RoyalSlimeCharm"), new Rectangle((int)pos.X, (int)pos.Y, 8, 8), new Rectangle(0, 0, 8, 8), drawColor, pos.ToRotation(), Vector2.One * 4, 0, 0);
            }
            return true;
        }
    }

}
