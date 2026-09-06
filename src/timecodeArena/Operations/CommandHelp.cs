using System;
using System.Collections.Generic;

namespace timecodeArena.Operations;

internal class CommandHelp : IOperation
{
	public string Keyword => "help";

	public OperationHelp Help => _help;
	private static readonly OperationHelp _help = new OperationHelp(
		name: nameof(CommandHelp)
		, description: "Shows the usage details for a given command."
		, usage: "HELP [command name]" 
		// , remarks: ""
	);

	public OperationStatus Action(Arena arena, IList<string> args)
	{
		OperationStatus status = ValidateArgs(args);
		if (status == false) { return status; }

		string operation_keyword = args[0];

		if (arena.Operations.ContainsKey(operation_keyword))
		{
			arena.Operations[operation_keyword].Help.Show();
		}

		return OperationStatus.ValidResult();
	}

	public OperationStatus ValidateArgs(IList<string> args)
	{
		const int ARG_COUNT = 1;

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