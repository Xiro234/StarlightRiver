using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace StarlightRiver.NPCs.Miniboss.Glassweaver
{
    internal partial class GlassMiniboss : ModNPC
    {
        Vector2 moveStart;
        Vector2 moveTarget;

        private void ResetAttack() => AttackTimer = 0;

        private void SwingSword()
        {
            int relTimer = (int)AttackTimer % 90;

            if (relTimer == 1)
            {
                npc.TargetClosest();

                int i = Projectile.NewProjectile(npc.Center, -Vector2.Normalize(npc.Center - Main.player[npc.target].Center), ProjectileType<SwordSlash>(), npc.damage * 2, 1);
                (Main.projectile[i].modProjectile as SwordSlash).parent = npc;

                moveTarget = Main.player[npc.target].Center;
                npc.velocity = Vector2.Normalize(npc.Center - moveTarget) * -0.1f;
            }

            if (relTimer < 60) npc.velocity = Vector2.Normalize(npc.Center - moveTarget) * (-0.1f * (60 - relTimer));

            if (AttackTimer >= 239) ResetAttack();
        }

        private void CastOrb()
        {
            if (AttackTimer == 1)
            {
                moveStart = npc.Center;
                npc.TargetClosest();
                moveTarget = spawnPos + new Vector2(Main.player[npc.target].Center.X > spawnPos.X ? -250 : 250, 20);
            }

            if (AttackTimer <= 60)
            {
                npc.Center = Vector2.SmoothStep(moveStart, moveTarget, AttackTimer / 60f);
            }

            if (AttackTimer == 120)
            {
                int i = NPC.NewNPC((int)npc.Center.X, (int)npc.Center.Y, NPCType<WeaverOrb>());
                Main.npc[i].velocity = Vector2.UnitX * (npc.Center.X > spawnPos.X ? -3 : 3);
            }

            if (AttackTimer >= 240) ResetAttack();
        }

        private void MoltenSwords()
        {
            int relTimer = (int)AttackTimer % 60;

            if (relTimer == 0) Projectile.NewProjectile(npc.Center + Vector2.UnitX.RotatedBy(AttackTimer / 240f * (6.28f / 3)) * -100, Vector2.Zero, ProjectileType<WeaverSwordMolten>(), npc.damage, 1);

            if (AttackTimer >= 300) ResetAttack();
        }

        private void CastSwords()
        {
            int relTimer = (int)AttackTimer % 60;

            if (relTimer == 0) Projectile.NewProjectile(npc.Center + Vector2.UnitX.RotatedBy(AttackTimer / 240f * (6.28f / 3)) * -100, Vector2.Zero, ProjectileType<WeaverSword>(), npc.damage, 1);

            if (AttackTimer >= 359) ResetAttack();
        }

        private void Recoil()
        {
            npc.defense = 0;
            npc.velocity *= 0.94f;

            if (AttackTimer >= 120)
            {
                AttackPhase = (int)AttackState.SwordSlash;
                npc.defense = 14;
                ResetAttack();
            }
        }
    }
}
