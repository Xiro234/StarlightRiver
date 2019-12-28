using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.IO;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;

namespace StarlightRiver.Dimensions
{
    class Dimension : WorldFileData
    {
        public static bool safeTravel = false;
        WorldFileData Parent;
        public Dimension(WorldFileData parent, string name) : base(Main.WorldPath + "/Dimensions/" + Main.ActiveWorldFileData.Name + "_" + name + ".wld", false)
        {
            Parent = parent;
            Name = name;           
        }

        public static void Travel(Dimension dim)
        {
            string dir = Main.WorldPath + "/Dimensions";
            if (!Directory.Exists(dir)) Directory.CreateDirectory(dir);

            string path = Main.WorldPath + "/Dimensions/" + Main.ActiveWorldFileData.Name + "_" + dim.Name + ".wld";

            if (!File.Exists(path))
            {
                using (FileStream f = File.Create(path))
                {
                    BinaryWriter w = new BinaryWriter(f);
                    WorldFile.SaveWorld_Version2(w);
                }
                dim.SetAsActive();
                WorldGen.playWorld();
                dim.Generate();
                WorldFile.saveWorld();
            }
            else
            {
                WorldFile.saveWorld();
                dim.SetAsActive();
                WorldGen.playWorld();
            }
            dim.OnEnterDim();
        }

        public static void Return(Dimension dim)
        {
            safeTravel = true;
            WorldFile.saveWorld();
            dim.Parent.SetAsActive();
            WorldGen.playWorld();
            dim.OnLeaveDim();
        }

        public virtual void Generate() { }
        public virtual void OnEnterDim() { }
        public virtual void OnLeaveDim() { }

    }
}
