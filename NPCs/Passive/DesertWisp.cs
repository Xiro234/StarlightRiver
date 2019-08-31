using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using spritersguildwip.Ability;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace spritersguildwip.NPCs.Passive
{
    class DesertWisp : ModNPC
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Mysterious Wisp");
        }
        public override void SetDefaults()
        {
            npc.width = 8;
            npc.height = 8;
            npc.damage = 0;
            npc.defense = 0;
            npc.lifeMax = 1;
            npc.noGravity = true;
            npc.noTileCollide = true;
            npc.immortal = true;
            npc.value = 0f;
            npc.knockBackResist = 0f;
            npc.aiStyle = 65;
        }

        bool fleeing = false;
        public override void AI()
        {
            npc.TargetClosest(true);
            Player player = Main.player[npc.target];
            AbilityHandler mp = player.GetModPlayer<AbilityHandler>();
            Vector2 distance = player.Center - npc.Center;

            if (npc.Hitbox.Intersects(player.Hitbox) && mp.dashcd > 1)
            {
                npc.active = false;
                Item.NewItem(npc.Center, Vector2.Zero, mod.ItemType("Reset"));
            }

            if((distance.Length() <= 180 && mp.dashcd == 0) || Main.dayTime)
            {
                fleeing = true;
            }
            if (fleeing)
            {
                npc.velocity.Y = 10;
                npc.velocity.X = 0;
                Dust.NewDustPerfect(npc.Center, mod.DustType("Air"), new Vector2(Main.rand.Next(-10, 10) * 0.1f, Main.rand.Next(-10, 10) * 0.1f));
            }
            Dust.NewDustPerfect(npc.Center, mod.DustType("Air"), new Vector2(Main.rand.Next(-20, 20)* 0.01f, Main.rand.Next(-20, 20) * 0.01f));
            if(LegendWorld.rottime == 0)
            {
                for(float k = 0; k <= Math.PI * 2; k += (float)Math.PI / 20)
                {
                    Dust.NewDustPerfect(npc.Center, mod.DustType("Air"), new Vector2((float)Math.Cos(k),(float)Math.Sin(k)),0,default,0.5f);
                }
            }
        }

        public override float SpawnChance(NPCSpawnInfo spawnInfo)
        {
            return (spawnInfo.player.ZoneOverworldHeight && !Main.dayTime && spawnInfo.player.ZoneDesert) ? 1f : 0f;
        }
    }
}
