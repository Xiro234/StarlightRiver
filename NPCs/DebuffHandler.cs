using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace StarlightRiver.NPCs
{
    public class DebuffHandler : GlobalNPC
    {
        public override bool InstancePerEntity => true;
        public bool snared = false;
        public bool ivy = false;
        public int frozenTime = 0;
        public override void ResetEffects(NPC npc)
        {
            snared = false;
            ivy = false;
            frozenTime = 0;
        }
        public override void UpdateLifeRegen(NPC npc, ref int damage)
        {
            //Weird debuff code, seems to be a test for something.
            //It's modifies the color and velocity of the NPC.
            if (frozenTime != 0)
            {
                frozenTime -= 1;
                npc.color.B += 180;
                npc.color.G += 90;
                if (npc.color.B >= 255)
                {
                    npc.color.B = 255;
                }
                if (npc.color.G >= 255)
                {
                    npc.color.G = 255;
                }
                npc.velocity *= 0.2f;
            }
            if (snared)
            {
                npc.position = npc.oldPosition;
            }
            if (ivy)
            {
                int lR = 4;
                if (npc.boss)
                { lR = 8; }
                else
                { lR = 4; }
                npc.lifeRegen -= lR;
            }
        }
        //This also seems to be used to for the weird debuff code.
        //...I got no idea why it's spawning dust but whatever.
        public override void DrawEffects(NPC npc, ref Color drawColor)
        {
            if (frozenTime != 0)
            {
                Dust dust = Main.dust[Dust.NewDust(npc.position, npc.width, npc.height, 15, 0f, 0f, 255, default, 1f)];
                dust.noGravity = true;
                dust.scale = 1.1f;
                dust.noLight = true;
            }
        }
    }
}