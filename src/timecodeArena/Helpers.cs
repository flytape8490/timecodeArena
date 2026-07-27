using System;

namespace timecodeArena;

internal static class Helpers
{
	internal static OperationStatus GetTimeCodeOrVariable(Arena arena, string code, out TimeCode? timeCode)
	{
		if (TimeCode.TryParse(code, out timeCode) == false)
		{
			if (arena.Variables.ContainsKey(code))
			{
				timeCode = arena.Variables[code];
			}
			else
			{
				return OperationStatus.InvalidResult($"Not a valid time code or identifier doesn't exist: {code}");
			}
		}

		return OperationStatus.ValidResult();
	}
}