using System;
using System.Collections.Generic;

namespace timecodeArena.Operations;

internal class Difference : IOperation
{
	public string Keyword => "delta";

	public OperationHelp Help => _help;
	internal static OperationHelp _help = new OperationHelp(
		name: nameof(Difference)
		, description: $"Calculates the difference between two {nameof(TimeCode)} values."
		, usage: "DELTA [timecode a] [timecode b]"
		, remarks: "The timecode values can also be an identifier in the variable table."
	);

	public OperationStatus Action(Arena arena, IList<string> args)
	{
		OperationStatus status = ValidateArgs(args);
		if (status == false) { return status; }

		status = arena.GetTimeCodeOrVariable(args[0], out TimeCode code_a);
		if (status == false) { return status; }

		status = arena.GetTimeCodeOrVariable(args[1], out TimeCode code_b);
		if (status == false) { return status; }

		int frame_difference = code_a.TotalFrames - code_b.TotalFrames;

		bool is_negative = frame_difference < 0;

		frame_difference = System.Math.Abs(frame_difference);

		TimeCode difference = new TimeCode(frame_difference);

		if (is_negative) { Console.WriteLine($"-{difference}"); }
		else { Console.WriteLine(difference); }

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