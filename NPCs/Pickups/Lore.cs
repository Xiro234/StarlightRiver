using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using StarlightRiver.Codex;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace StarlightRiver.NPCs.Pickups
{
    class Lore : AbilityPickup
    {
        public override string Texture => "StarlightRiver/GUI/Book1Closed";
        public override Color GlowColor => new Color(200, 130, 40);
        public override bool CanPickup(Player player) => player.GetModPlayer<CodexHandler>().CodexState == 0;
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Starlight Codex");
        }

        public override void Visuals()
        {
            float rot = Main.rand.NextFloat(6.28f);
            Dust.NewDustPerfect(npc.Center + Vector2.One.RotatedBy(rot) * 20, ModContent.DustType<Dusts.Stamina>(), Vector2.One.RotatedBy(rot) * -1);

            Lighting.AddLight(npc.Center, new Vector3(1, 0.5f, 0));
        }
        public override void PickupVisuals(int timer)
        {
           if(timer == 119)
            {
                string message = "Open the codex from your inventory to learn about the world.";

                StarlightRiver.Instance.abilitytext.Display("Starlight Codex", message, null, 240);
                Helper.UnlockEntry<Codex.CodexEntry>(Main.LocalPlayer);
            }
        }
        public override void PickupEffects(Player player)
        {
            CodexHandler mp = player.GetModPlayer<CodexHandler>();
            mp.CodexState = 1;

            player.GetModPlayer<StarlightPlayer>().MaxPickupTimer = 120;
            player.AddBuff(BuffID.Featherfall, 130);
        }
    }
}
