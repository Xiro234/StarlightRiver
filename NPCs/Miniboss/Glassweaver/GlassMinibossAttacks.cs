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

        private Vector2 PickSide() => Main.player[npc.target].Center.X > spawnPos.X ? spawnPos + new Vector2(-160, 32) : spawnPos + new Vector2(160, 32); //picks the opposite side of the player.

        private void SpawnAnimation()
        {
            if (AttackTimer == 1) moveStart = npc.Center;
            npc.Center = Vector2.SmoothStep(moveStart, spawnPos + new Vector2(0, -100), AttackTimer / 300f);
        }

        private void HammerSlam()
        {
            if (AttackTimer == 1) //sets appropriate movement points
            {
                npc.TargetClosest();
                moveTarget = PickSide();
                moveStart = npc.Center;
            }

            if (AttackTimer < 60) npc.Center = Vector2.SmoothStep(moveStart, moveTarget, AttackTimer / 60f); //move into position

            if (AttackTimer == 60) Projectile.NewProjectile(npc.Center, Vector2.Zero, ProjectileType<GlassHammer>(), 40, 0, Main.myPlayer, npc.Center.X > spawnPos.X ? -1 : 1); //spawn our hammer, see GlassHammer's AI for more information

            if (AttackTimer >= 180) ResetAttack();
        }

        private void SlashCombo()
        {
            ResetAttack();
        }

        private void SummonKnives()
        {
            ResetAttack();
        }


    }
}
