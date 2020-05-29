using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace StarlightRiver.NPCs.Town
{
    [AutoloadHead]
    public class Voidsmith : ModNPC
    {
        public override bool Autoload(ref string name)
        {
            name = "Voidsmith";
            return mod.Properties.Autoload;
        }

        public override void SetStaticDefaults()
        {
            Main.npcFrameCount[npc.type] = 25;
            NPCID.Sets.ExtraFramesCount[npc.type] = 9;
            NPCID.Sets.AttackFrameCount[npc.type] = 4;
            NPCID.Sets.DangerDetectRange[npc.type] = 700;
            NPCID.Sets.AttackType[npc.type] = 0;
            NPCID.Sets.AttackTime[npc.type] = 90;
            NPCID.Sets.AttackAverageChance[npc.type] = 30;
            NPCID.Sets.HatOffsetY[npc.type] = 4;
        }

        public override void SetDefaults()
        {
            npc.townNPC = true;
            npc.friendly = true;
            npc.width = 18;
            npc.height = 40;
            npc.aiStyle = 7;
            npc.damage = 10;
            npc.defense = 15;
            npc.lifeMax = 250;
            npc.HitSound = SoundID.NPCHit1;
            npc.DeathSound = SoundID.NPCDeath1;
            npc.knockBackResist = 0.5f;
            animationType = NPCID.Guide;
        }

        public override bool CanTownNPCSpawn(int numTownNPCs, int money)
        {
            return true;
        }

        public override bool CheckConditions(int left, int right, int top, int bottom)
        {
            return top >= (Main.maxTilesY - 200);
        }

        public override string TownNPCName()
        {
            switch (WorldGen.genRand.Next(4))
            {
                case 0:
                    return "111Nick";
                case 1:
                    return "121Nick";
                case 2:
                    return "122Nick";
                default:
                    return "123Nick";
            }
        }

        public override string GetChat()
        {
            switch (Main.rand.Next(3))
            {
                case 0:
                    return "Vortex is Gay";
                case 1:
                    return "Vortex is Gayer";
                default:
                    return "Vortex is Gayest";
            }
        }

        public override void SetChatButtons(ref string button, ref string button2)
        {
            button = Language.GetTextValue("LegacyInterface.28");
            button2 = "Upgrades";
        }


        public override void OnChatButtonClicked(bool firstButton, ref bool shop)
        {
            if (firstButton)
            {
                shop = true;
            }
            else
            {
                LegendWorld.NPCUpgrades[0]++;
            }
        }

        public override void SetupShop(Chest shop, ref int nextSlot)
        {
            shop.item[nextSlot].SetDefaults(ModContent.ItemType<Items.VoidGooItem>());
        }

        public override void TownNPCAttackStrength(ref int damage, ref float knockback)
        {
            damage = 20;
            knockback = 4f;
        }

        public override void TownNPCAttackCooldown(ref int cooldown, ref int randExtraCooldown)
        {
            cooldown = 30;
            randExtraCooldown = 30;
        }

        public override void TownNPCAttackProj(ref int projType, ref int attackDelay)
        {
            projType = mod.ProjectileType("SparklingBall");
            attackDelay = 1;
        }

        public override void TownNPCAttackProjSpeed(ref float multiplier, ref float gravityCorrection, ref float randomOffset)
        {
            multiplier = 12f;
            randomOffset = 2f;
        }
    }
}