using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using StarlightRiver.Configs;
using Terraria;
using Terraria.ModLoader;

namespace StarlightRiver.GUI
{
    public class BootlegDust
    {
        public Texture2D tex;
        public Vector2 pos;
        public Vector2 vel;
        public Color col;
        public float scl;
        public float rot = 0;
        public int time;

        public BootlegDust(Texture2D texture, Vector2 position, Vector2 velocity, Color color, float scale, int timeleft)
        {
            tex = texture;
            pos = position;
            vel = velocity;
            col = color;
            scl = scale;
            time = timeleft;
        }

        public void SafeDraw(SpriteBatch spriteBatch)
        {
            if (ModContent.GetInstance<Config>().Active) Draw(spriteBatch);        
        }

        public virtual void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(tex, pos, default, col, rot, tex.Size() / 2, scl, default, 0);
        }

        public virtual void Update()
        {
        }
    }

    public class ExpertDust : BootlegDust
    {
        public ExpertDust(Texture2D texture, Vector2 position, Vector2 velocity, Color color, float scale, int timeleft) : base(texture, position, velocity, color, scale, timeleft)
        {
        }

        public override void Update()
        {
            pos += vel;
            col.R -= 4;
            col.G -= 4;
            col.B -= 4;
            scl *= 0.94f;
            time--;
        }
    }
}