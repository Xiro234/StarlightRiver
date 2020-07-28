using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria.ID;
using Terraria;
using Terraria.ModLoader;
using Terraria.Localization;
using static Terraria.ModLoader.ModContent;

namespace StarlightRiver.NPCs.Miniboss.Glassweaver
{
    [AutoloadHead]
    class GlassweaverTown : ModNPC
    {
        public override bool CanTownNPCSpawn(int numTownNPCs, int money) => true;

        public override bool CheckConditions(int left, int right, int top, int bottom) => true;

        public override string TownNPCName() => "";

        public override void SetStaticDefaults() => DisplayName.SetDefault("Glassweaver");

        public override void SetDefaults()
        {
            npc.townNPC = true;
            npc.friendly = true;
            npc.width = 64;
            npc.height = 64;
            npc.aiStyle = -1;
            npc.damage = 10;
            npc.defense = 15;
            npc.lifeMax = 250;
            npc.HitSound = SoundID.NPCHit1;
            npc.DeathSound = SoundID.NPCDeath1;
            npc.knockBackResist = 0;
        }

        public override string GetChat()
        {
            return "I am cooming!!!!!";
        }

        public override void SetChatButtons(ref string button, ref string button2)
        {
            button = "Challenge";
            button2 = "Upgrade Weapon";
        }

        public override void OnChatButtonClicked(bool firstButton, ref bool shop)
        {
            if (firstButton)
            {
                NPC.NewNPC((int)npc.Center.X, (int)npc.Center.Y, NPCType<GlassMiniboss>());
                npc.active = false;
            }
        }
    }
}
