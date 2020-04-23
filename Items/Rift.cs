using StarlightRiver.Tiles.Rift;

namespace StarlightRiver.Items
{
    class RiftCore1 : QuickMaterial
    {
        public RiftCore1() : base("Twitching Rift Catalyst", "", 999, 200, 6) { }
        public override void SetDefaults() { item.GetGlobalItem<RiftItem>().tier = 1; }
    }
    class RiftCore2 : QuickMaterial
    {
        public RiftCore2() : base("Pulsing Rift Catalyst", "", 999, 200, 6) { }
        public override void SetDefaults() { item.GetGlobalItem<RiftItem>().tier = 2; }
    }
    class RiftCore3 : QuickMaterial
    {
        public RiftCore3() : base("Flowing Rift Catalyst", "", 999, 200, 6) { }
        public override void SetDefaults() { item.GetGlobalItem<RiftItem>().tier = 3; }
    }
    class RiftCore4 : QuickMaterial
    {
        public RiftCore4() : base("Restless Rift Catalyst", "", 999, 200, 6) { }
        public override void SetDefaults() { item.GetGlobalItem<RiftItem>().tier = 4; }
    }
    class RiftCore5 : QuickMaterial
    {
        public RiftCore5() : base("Chaotic Rift Catalyst", "", 999, 200, 6) { }
        public override void SetDefaults() { item.GetGlobalItem<RiftItem>().tier = 5; }
    }
}
