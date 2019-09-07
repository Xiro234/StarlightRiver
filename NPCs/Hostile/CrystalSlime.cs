using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using spritersguildwip.Ability;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace spritersguildwip.NPCs.Hostile
{
    class CrystalSlime : ModNPC
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Crystal Slime");
        }
        public override void SetDefaults()
        {
            npc.width = 30;
            npc.height = 30;
            npc.damage = 10;
            npc.defense = 5;
            npc.lifeMax = 25;
            npc.HitSound = SoundID.NPCHit1;
            npc.DeathSound = SoundID.NPCDeath1;
            npc.value = 10f;
            npc.knockBackResist = 0.2f;
            npc.aiStyle = 1;          
        }

        bool shielded = true;
        public override void AI()
        {
            npc.TargetClosest(true);
            Player player = Main.player[npc.target];

            if (npc.Hitbox.Intersects(player.Hitbox) && player.GetModPlayer<AbilityHandler>().ability is Dash && player.GetModPlayer<AbilityHandler>().ability.Active)
            {
                if (shielded)
                {
                    shielded = false;
                    npc.velocity += player.velocity * 0.5f;
                }

                player.GetModPlayer<AbilityHandler>().ability.Active = false;
                player.GetModPlayer<AbilityHandler>().ability.OnExit();
            }

            if(shielded)
            {
                npc.immortal = true;
            }
            else
            {
                npc.immortal = false;
            }
        }

        public override void ModifyHitPlayer(Player target, ref int damage, ref bool crit)
        {
            if(target.GetModPlayer<AbilityHandler>().ability is Dash && target.GetModPlayer<AbilityHandler>().ability.Active)
            {
                target.immune = true;
                target.immuneTime = 5;
            }
        }

        public static Texture2D crystal = ModContent.GetTexture("spritersguildwip/NPCs/Hostile/Crystal");

        public override void PostDraw(SpriteBatch spriteBatch, Color drawColor)
        {
            if (shielded)
            {
                spriteBatch.Draw(crystal, npc.position - Main.screenPosition, Color.White);
            }
        }
    }
}
