using System;
using System.Collections.Generic;

namespace timecodeArena.Operations;

internal class List : IOperation
{
	public string Keyword => "list";
	public OperationHelp Help => _help;

	private static OperationHelp _help = new OperationHelp(
		name: nameof(List)
		, description: "Lists all command available to run."
		, usage: "LIST"
		// , remarks: ""
	);

	public OperationStatus Action(Arena arena, IList<string> args)
	{
		OperationStatus status = ValidateArgs(args);
		if (status == false) { return status; }

		arena.ListCommands();

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