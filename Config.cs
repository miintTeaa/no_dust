using Terraria.ModLoader.Config;
using System.ComponentModel;

namespace NoDust {
    public class NoDustConfig : ModConfig {
        public override ConfigScope Mode => ConfigScope.ClientSide;

        [Label("Dust reduction percent")]
        [DefaultValue(100)]
        [Range(0, 100)]
        [Increment(1)]
        [Slider]
        public int DustDestroyChance;
    }
}