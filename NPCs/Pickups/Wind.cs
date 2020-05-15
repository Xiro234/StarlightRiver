using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using StarlightRiver.Abilities;
using StarlightRiver.Codex.Entries;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace StarlightRiver.NPCs.Pickups
{
    class Wind : AbilityPickup
    {
        public override string Texture => "StarlightRiver/NPCs/Pickups/Wind1";
        public override bool CanPickup(Player player) => player.GetModPlayer<AbilityHandler>().dash.Locked;
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Forbidden Winds");
        }

        public override void Visuals()
        {
            Dust dus = Dust.NewDustPerfect(new Vector2(npc.Center.X + (float)Math.Sin(LegendWorld.rottime) * 30, npc.Center.Y - 20), ModContent.DustType<Dusts.Air>(), Vector2.Zero);
            dus.fadeIn = Math.Abs((float)Math.Sin(LegendWorld.rottime));

            Dust dus2 = Dust.NewDustPerfect(new Vector2(npc.Center.X + (float)Math.Cos(LegendWorld.rottime) * 15, npc.Center.Y), ModContent.DustType<Dusts.Air>(), Vector2.Zero);
            dus2.fadeIn = Math.Abs((float)Math.Cos(LegendWorld.rottime));

            if (Main.rand.Next(5) == 0)
            {
              // Dust dus3 = Dust.NewDustPerfect(npc.Center, ModContent.DustType<Dusts.Air>(), Vector2.One.RotatedByRandom(6.28f) * Main.rand.NextFloat(0.2f));
                //dus3.fadeIn = 0.4f;
            }
        }
        public override void PickupVisuals(int timer)
        {
            Player player = Main.LocalPlayer;

            if (timer == 1) Main.PlaySound(mod.GetLegacySoundSlot(SoundType.Custom, "Sounds/Pickups/get")); //start the SFX

            if (timer < 300) //spiraling out
            {
                for (int k = 0; k < 3; k++)
                {
                    float scale = timer < 50 ? timer / 50f : 1;
                    float rot = timer / 80f * 6.28f + k * 2;
                    Vector2 pos = player.Center + new Vector2(0, 30) + new Vector2((float)Math.Sin(rot) * (timer / 570f * 80f), -timer / 300f * 80 + k * 10);
                    Dust dus = Dust.NewDustPerfect(pos, ModContent.DustType<Dusts.Air>(), Vector2.Zero, 0, default, scale);
                    dus.fadeIn = rot > 3.14f ? 1 - Math.Abs((float)Math.Sin(timer / 100f * 6.28f + k * 2)) : 1;
                    dus.fadeIn = Math.Abs((float)Math.Sin(timer / 80f * 6.28f + k * 2));
                }
            }

            if(timer > 300 && timer < 400) //coming in
            {
                for (int k = 0; k < 3; k++)
                {
                    Vector2 startPos = player.Center + new Vector2(0, 30) + new Vector2((float)Math.Sin(timer / 80f * 6.28f + k * 2) * (300 / 570f * 80f), -80 + k * 10);
                    Vector2 endPos = player.Center;
                    Dust dus = Dust.NewDustPerfect(Vector2.Lerp(startPos, endPos, (timer - 300) / 100f), ModContent.DustType<Dusts.Air>(), Vector2.Zero, 0, default, 1.2f - (timer - 300) / 100f);
                    dus.fadeIn = Math.Abs((float)Math.Sin(timer / 80f * 6.28f + k * 2));
                }
            }

            if (timer == 400)
            {
                for (int k = 0; k < 60; k++)
                {
                    Dust.NewDustPerfect(player.Center, ModContent.DustType<Dusts.Air>(), new Vector2(0, -1).RotatedBy(k / 60f * 6.28f));
                }
                Main.PlaySound(SoundID.DD2_ExplosiveTrapExplode);
            }

            if(timer > 400)
            {
                Dust.NewDust(player.position, player.width, player.height, ModContent.DustType<Dusts.Air>(), 0,  0, 0, default, 0.3f);
            }

            if (timer == 569) //popup + codex entry
            {
                string message = StarlightRiver.Dash.GetAssignedKeys().Count > 0 ?
                    "Press " + StarlightRiver.Dash.GetAssignedKeys()[0] + " + A/W/S/D to dash." :
                    "Press " + "[Please bind a key]" + " + A/W/S/D to dash.";

                StarlightRiver.Instance.abilitytext.Display("Forbidden Winds", message, Main.LocalPlayer.GetModPlayer<AbilityHandler>().dash);
                Helper.UnlockEntry<WindsEntry>(Main.LocalPlayer);
            }
        }
        public override void PickupEffects(Player player)
        {
            AbilityHandler mp = player.GetModPlayer<AbilityHandler>();
            mp.dash.Locked = false;
            mp.StatStaminaMaxPerm++;

            player.GetModPlayer<StarlightPlayer>().MaxPickupTimer = 570;
            player.AddBuff(BuffID.Featherfall, 580);
        }
    }
}
