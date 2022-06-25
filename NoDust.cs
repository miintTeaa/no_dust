using Terraria.ModLoader;
using MonoMod.Cil;
using Mono.Cecil.Cil;
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

			//Pushes value of config on the stack
			c.EmitDelegate<Func<bool>>(() => {
				return ModContent.GetInstance<NoDustConfig>().TurnOffDust;
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