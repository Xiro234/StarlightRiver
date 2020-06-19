using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace StarlightRiver.NPCs.Miniboss.Glassweaver
{
    internal partial class GlassMiniboss : ModNPC
    {
        internal ref float GlobalTimer => ref npc.ai[0];
        internal ref float Phase => ref npc.ai[1];
        internal ref float AttackPhase => ref npc.ai[2];
        internal ref float AttackTimer => ref npc.ai[3];

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
            Recoil = 2
        }

        public override string Texture => "StarlightRiver/MarioCumming";

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

                    SetPhase(PhaseEnum.SpawnAnimation);

                    break;

                case (int)PhaseEnum.SpawnAnimation:

                    SetPhase(PhaseEnum.FirstPhase);

                    break;

                case (int)PhaseEnum.FirstPhase:

                    if(AttackTimer == 1)
                    {
                        AttackPhase++;
                        if (AttackPhase > 1) AttackPhase = 1;
                    }

                    switch (AttackPhase)
                    {
                        case (int)AttackState.CastSwords: CastSwords(); break;
                        case (int)AttackState.SwordSlash: SwingSword(); break;
                        case (int)AttackState.Recoil: Recoil(); break;
                    }

                    break;
            }
        }

        private void SetPhase(PhaseEnum phase) => Phase = (float)phase;
    }
}
