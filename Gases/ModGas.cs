using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
namespace StarlightRiver.Gases
{
    class GasWorld : ModWorld
    {
        public static ModGas[,] Gas;
        public override void Initialize()
        {
            Gas = new ModGas[Main.maxTilesX, Main.maxTilesY];
        }
        public override void PostUpdate()
        {
            Player player = Main.LocalPlayer;
            int max = Main.screenWidth / 24;


            for (int x = (int)(player.Center.X / 16) - max; x <= (int)(player.Center.X / 16) + max; x++)
            {
                for (int y = (int)(player.Center.Y / 16) - max; y <= (int)(player.Center.Y / 16) + max; y++)
                {
                    /*ModGas gas = Gas[x, y];

                    if (gas != null && gas.Strength >= 1)
                    {
                        Gas[gas.i, gas.j + 1] = new ModGas(gas.maxStrength, gas.Strength - Main.rand.Next(2, 15), gas.DustType, gas.i, gas.j + 1);

                        Gas[gas.i, gas.j - 1] = new ModGas(gas.maxStrength, gas.Strength - Main.rand.Next(2, 15), gas.DustType, gas.i, gas.j - 1);

                        Gas[gas.i + 1, gas.j] = new ModGas(gas.maxStrength, gas.Strength - Main.rand.Next(2, 15), gas.DustType, gas.i + 1, gas.j);

                        Gas[gas.i - 1, gas.j] = new ModGas(gas.maxStrength, gas.Strength - Main.rand.Next(2, 15), gas.DustType, gas.i - 1, gas.j);

                        gas.Strength -= 3;
                    }
                    else if (gas != null) { Gas[gas.i, gas.j] = null; }
                    if (gas != null) { gas.Update(); }*/
                }
            }
                //timer = 0;
            //}
            
        }
    }
    class ModGas
    {
        public int maxStrength = 0;
        public int Strength = 0;
        public int DustType = 0;
        public int i = 0;
        public int j = 0;
        public ModGas(int maxstrength, int strength, int dusttype, int x, int y)
        {
            maxStrength = maxstrength;
            Strength = strength;
            DustType = dusttype;
            i = x;
            j = y;            
        }

        public virtual void Update()
        {
            Color color = Lighting.GetColor(i,j) * 0.05f * (Strength / (float)maxStrength);
            int dus = Dust.NewDust(new Vector2(i, j) * 16, 16, 16, DustType, 0, 0, 0, color, Strength / (maxStrength / 20f));
        }

        public static void SpawnGas(int i, int j, int type, int strength)
        {
            GasWorld.Gas[i, j] = new ModGas(strength, strength, type, i, j); 
        }

    }
}
 