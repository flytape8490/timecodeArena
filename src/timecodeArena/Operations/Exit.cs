using System;
using System.Collections.Generic;

namespace timecodeArena.Operations;

internal class Exit : IOperation
{
	public string Keyword => "exit";
	public OperationHelp Help => _help;

	private static OperationHelp _help = new OperationHelp(
		name: nameof(Exit)
		, description: "Exits the application."
		, usage: "EXIT"
		// , remarks: ""
	);

	public OperationStatus Action(Arena arena, IList<string> args)
	{
		OperationStatus status = ValidateArgs(args);
		if (status == false) { return status; }

		arena.Shutdown();

		return status;
	}

	public OperationStatus ValidateArgs(IList<string> args)
	{
		if (args != null && args.Count != 0)
		{
			return OperationStatus.InvalidResult("Too many arguments given");
		}

		return OperationStatus.ValidResult();
	}
}