using System;
using System.Collections.Generic;

namespace timecodeArena.Operations;

internal class Addition : IOperation
{
	public string Keyword => "add";

	public OperationHelp Help => _help;
	private static readonly OperationHelp _help = new OperationHelp(
		name: nameof(Addition)
		, description: $"Calculates the sum of two {nameof(TimeCode)} values."
		, usage: "ADD [timecode a] [timecode b]"
		, remarks: "Instead of being treated as a timestamp, [timecode b] is treated as an amount of time elapsed since [timecode a]."
	);

	public OperationStatus Action(Arena arena, IList<string> args)
	{
		OperationStatus status = ValidateArgs(args);
		if (status == false) { return status; }

		status = arena.GetTimeCodeOrVariable(args[0], out TimeCode code_a);
		if (status == false) { return status; }

		status = arena.GetTimeCodeOrVariable(args[1], out TimeCode code_b);
		if (status == false) { return status; }

		int frame_sum = code_a.TotalFrames + code_b.TotalFrames;

		TimeCode code_sum = new TimeCode(frame_sum);

		Console.WriteLine(code_sum);

		return status;
	}

	public OperationStatus ValidateArgs(IList<string> args)
	{
		const int ARG_COUNT = 2;

		if (args.Count < ARG_COUNT)
		{
			return OperationStatus.InvalidResult("Too few arguments given");  
		}

		else if (args.Count > ARG_COUNT)
		{
			return OperationStatus.InvalidResult("Too many arguments given");
		}

		return OperationStatus.ValidResult();
	}
}