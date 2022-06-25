using Terraria.ModLoader;
using MonoMod.Cil;
using Mono.Cecil.Cil;
using Terraria;
using System;

namespace NoDust
{
	public class NoDust : Mod
	{
		public override void Load() {
			IL.Terraria.Dust.NewDust += HookDust;
		}

		private void HookDust(ILContext il) {
			var c = new ILCursor(il);
			var if_end = c.DefineLabel();
			var if_start = c.DefineLabel();

			// Loads dust type onto the stack
			c.Emit(OpCodes.Ldarg_3);

			// Pushes value of config on the stack
			c.EmitDelegate<Func<int, bool>>((dust_id) => {
				var config = ModContent.GetInstance<NoDustConfig>();
				var l_config = ModContent.GetInstance<NoDustListConfig>();

				if (!config.Enabled) {
					return false;
				}

				var should_destroy = Main.rand.NextFloat() * 100 <= config.DustDestroyChance;

				if (l_config.ListSetting == ListConfigSettings.Whitelist && l_config.DustList.Contains(dust_id)) {
					should_destroy = false;
				}
				else if (l_config.ListSetting == ListConfigSettings.Blacklist && !l_config.DustList.Contains(dust_id)) {
					should_destroy = false;
				}

				if (!should_destroy && l_config.PrintLastDust) {
					Main.NewText("Last generated dust id:" + dust_id);
				}

				return should_destroy;
			});

			// Jump to end of if statement if returned boolean value is false
			c.Emit(OpCodes.Brfalse_S, if_end);

			// Puts 6000 on the stack and returns
			// This value, when returned from this method, means no dust is spawned
			c.Emit(OpCodes.Ldc_I4, 6000);
			c.Emit(OpCodes.Ret);

			c.MarkLabel(if_end);
			
		}
	}
}