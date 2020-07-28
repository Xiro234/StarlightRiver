using Microsoft.Xna.Framework;
using Terraria.ModLoader;
using Terraria;

namespace StarlightRiver.NPCs.Miniboss.Glassweaver
{
    internal partial class GlassMiniboss : ModNPC
    {
        internal ref float GlobalTimer => ref npc.ai[0];
        internal ref float Phase => ref npc.ai[1];
        internal ref float AttackPhase => ref npc.ai[2];
        internal ref float AttackTimer => ref npc.ai[3];

        Vector2 spawnPos => StarlightWorld.VitricBiome.TopLeft() * 16 + new Vector2( -9.5f * 16, 76 * 16);

        public enum PhaseEnum
        {
            SpawnEffects = 0,
            SpawnAnimation = 1,
            FirstPhase = 2,
            SuckGlass = 3,
            SecondPhase = 4
        }

        public enum AttackState
        {
            CastSwords = 0,
            SwordSlash = 1,
            CastOrb = 2,
            Recoil = 3
        }

        public override void SetStaticDefaults() => DisplayName.SetDefault("Glassweaver");

        public override void SetDefaults()
        {
            npc.width = 64;
            npc.height = 128;
            npc.lifeMax = 2500;
            npc.damage = 20;
            npc.aiStyle = -1;
            npc.noGravity = true;
            npc.knockBackResist = 0;
            npc.boss = true;
            npc.defense = 14;
        }

        public override void AI()
        {
            GlobalTimer++;
            AttackTimer++;

            switch (Phase)
            {
                case (int)PhaseEnum.SpawnEffects:

                    ResetAttack();
                    SetPhase(PhaseEnum.SpawnAnimation);

                    break;

                case (int)PhaseEnum.SpawnAnimation:

                    if (GlobalTimer < 300) SpawnAnimation();
                    else
                    {
                        SetPhase(PhaseEnum.FirstPhase);
                        ResetAttack();
                        npc.noGravity = false;
                    }

                    break;

                case (int)PhaseEnum.FirstPhase:

                    npc.spriteDirection = npc.Center.X > spawnPos.X ? 1 : -1;

                    if (AttackTimer == 1)
                    {
                        AttackPhase++;
                        if (AttackPhase > 3) AttackPhase = 0;
                    }

                    switch (AttackPhase)
                    {
                        case 0: HammerSlam(); break;
                        case 1: SummonKnives(); break;
                        case 2: SlashCombo(); break;
                        case 3: SummonKnives(); break;
                    }

                    break;
            }
        }

        private void SetPhase(PhaseEnum phase) => Phase = (float)phase;
    }
}
