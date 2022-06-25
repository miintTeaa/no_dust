using Terraria.ModLoader.Config;
using System.ComponentModel;
using System.Collections.Generic;

namespace NoDust {
    public enum ListConfigSettings { 
        Whitelist,
        Blacklist,
        Off
    }
    public class NoDustConfig : ModConfig {
        public override ConfigScope Mode => ConfigScope.ClientSide;

        [Label("Dust reduction percent")]
        [DefaultValue(100)]
        [Range(0, 100)]
        [Increment(1)]
        [Slider]
        public int DustDestroyChance;

        [Label("Print last dust ID (helps with configuring list)")]
        public bool PrintLastDust;

        [Label("List type")]
        public ListConfigSettings ListSetting;

        [Label("Dust list")]
        public HashSet<int> DustList;
    }
}