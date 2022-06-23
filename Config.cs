using Terraria.ModLoader.Config;
using System.ComponentModel;

namespace NoDust {
    public class NoDustConfig : ModConfig {
        public override ConfigScope Mode => ConfigScope.ClientSide;

        [Label("Turn off dust")]
		[DefaultValue(true)]
        public bool TurnOffDust;
    }
}