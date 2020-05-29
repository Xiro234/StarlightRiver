using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using StarlightRiver.Abilities;
using StarlightRiver.Codex.Entries;
using System;
using Terraria;
using Terraria.Graphics.Effects;
using Terraria.ID;
using Terraria.ModLoader;

namespace StarlightRiver.NPCs.Pickups
{
    class Wisp : AbilityPickup
    {
        public override string Texture => "StarlightRiver/NPCs/Pickups/Wisp1";
        public override Color GlowColor => new Color(255, 255, 130);
        public override bool CanPickup(Player player) => player.GetModPlayer<AbilityHandler>().wisp.Locked;
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Faeflame");
        }

        public override void Visuals()
        {
            Dust.NewDustPerfect(npc.Center + new Vector2((float)Math.Cos(LegendWorld.rottime), (float)Math.Sin(LegendWorld.rottime)) * 32, ModContent.DustType<Dusts.Gold>(), null, 0, default, 0.4f);
            Dust.NewDustPerfect(npc.Center + new Vector2((float)Math.Cos(LegendWorld.rottime + 3) / 2, (float)Math.Sin(LegendWorld.rottime + 3)) * 32, ModContent.DustType<Dusts.Gold>(), null, 0, default, 0.4f);
            Dust.NewDustPerfect(npc.Center + new Vector2((float)Math.Cos(LegendWorld.rottime + 2), (float)Math.Sin(LegendWorld.rottime + 2) / 2) * 32, ModContent.DustType<Dusts.Gold>(), null, 0, default, 0.4f);
        }

        public override void PickupVisuals(int timer)
        {
            if (timer == 1)
            {
                Main.PlaySound(mod.GetLegacySoundSlot(SoundType.Custom, "Sounds/Pickups/get")); //start the SFX
                Filters.Scene.Deactivate("ShockwaveFilter");
            }
        }

        public override void PickupEffects(Player player)
        {
            AbilityHandler mp = player.GetModPlayer<AbilityHandler>();
            mp.wisp.Locked = false;
            mp.StatStaminaMaxPerm++;

            player.GetModPlayer<StarlightPlayer>().MaxPickupTimer = 570;
            player.AddBuff(BuffID.Featherfall, 580);
        }
    }
}
