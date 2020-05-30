using Microsoft.Xna.Framework;
using StarlightRiver.GUI;
using Terraria;
using Terraria.ModLoader;

namespace StarlightRiver
{
    public class WindowLightDust : BootlegDust
    {
        private Vector2 basePos;
        private readonly float depth;

        public WindowLightDust(Vector2 basepos, Vector2 velocity) : base(ModContent.GetTexture("StarlightRiver/GUI/HolyBig"), basepos, velocity, Color.White, Main.rand.NextFloat(0.2f, 0.7f), 500)
        {
            depth = Main.rand.NextFloat(0.4f, 0.5f);
            basePos = basepos;
        }

        public override void Update()
        {
            pos = basePos + vel * (500 - time) - Main.screenPosition + StarlightRiver.FindOffset(basePos, depth);
            col = Color.White * (time / 500f) * 0.8f;
            scl *= 0.99999f;
            time--;
        }
    }
}