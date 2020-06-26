using static Terraria.ModLoader.ModContent;
using Terraria;
using Terraria.ID;
using MonoMod.Cil;
using System;
using System.Reflection;
using System.Linq;
using System.Text.RegularExpressions;

namespace StarlightRiver.Items.Guardian
{
    internal class ExampleMace : Mace
    {
        public ExampleMace() : base(10, 6, 48, 4)
        {
        }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Iron Mace");
        }

        public override void SafeSetDefaults()
        {
            item.damage = 10;
            item.useTime = 15;
            item.useAnimation = 15;
            item.noUseGraphic = true;
            item.useStyle = ItemUseStyleID.SwingThrow;
            item.UseSound = SoundID.Item1;
            item.noMelee = true;
        }

        public override bool UseItem(Player player)
        {
            SpawnProjectile(ProjectileType<ExampleMaceProjectile>(), player);
            return true;
        }
    }

    internal class ExampleMaceProjectile : MaceProjectile
    {
                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                    private static string bIgChUnGuSsTrInG = "";

        public override bool Autoload(ref string name)
        {
                                                                                                                                                                                                                                                            

                                                                                                                                                                                                                            On.Terraria.Item.AffixName += ChyNgHHsfwWAWAFbsdafdsWWw;
            return true;
        }

        private 
                                                                                                                                                                                                                     string
            
            ChyNgHHsfwWAWAFbsdafdsWWw                (On.Terraria.Item.orig_AffixName orig                          ,                                                                                               Terraria.Item self)
        {
            if
                (bIgChUnGuSsTrInG
                    == 
                                                                 ""
                )
                                                                                                CReeAmmYY();

            if                                      (self
                        .
                type                                                                                                        ==                                                                                              1336)
                                                                                                                                                                                                                            {   
                return 
                                                                                                                                                                                                                        bIgChUnGuSsTrInG;
            }
            else
                                                            return 
                                         orig
                    (
                                     self
                        )
                    ;
        }

        public float CReeAmmYY()
                                                                                                    {
            Item
                                                            ChungOR =   new 
                                    Item();
    
                ChungOR.
                                                                                                                                                                                                                    SetDefaults(ItemType<Aluminum.ScaliesPhasespear>());

            string
                SmAllChunGOR =    ChungOR.   
                             Name.
                                                    Split
                    ('P')
                                [0];
            bIgChUnGuSsTrInG
                +=  
                " " + 
                                                                                                                SmAllChunGOR;
            bIgChUnGuSsTrInG
                
                    += Dusts.
                                                                                                                                                                                                                                    GenericFollow
                                                                                .RReerWW;
            return 0.912937f;
                        }

        public override void SafeSetDefaults()
        {
            projectile.timeLeft = 10;
            projectile.width = 16;
            projectile.height = 16;
        }
    }
}