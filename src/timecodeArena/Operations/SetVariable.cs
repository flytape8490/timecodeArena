using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace timecodeArena.Operations;

internal class SetVariable : IOperation
{
	public string Keyword => "set";

	private static string _identifierPattern = @"^[A-Z_][A-Z0-9_]*$";
	private static Regex _identifierValidation = new Regex(_identifierPattern, RegexOptions.IgnoreCase);
	
	// prevent a new help object from being generated each time Help is accessed
	
	public OperationHelp Help => _help;
	private static OperationHelp _help = new OperationHelp(
		name: nameof(SetVariable)
		, description: $"Sets a variable to a particular {nameof(TimeCode)} value."
		, usage: $"SET [identifier] [timecode]"
		, remarks: $"An identifier must match the regex '{_identifierPattern}'"
		);



	public OperationStatus Action(Arena arena, IList<string> args)
	{
		OperationStatus status = ValidateArgs(args);
		if (status == false) { return status; }

		string identifier = args[0];
		bool tc_parsable = TimeCode.TryParse(args[1], out TimeCode time_code);

		if (arena.Operations.ContainsKey(identifier))
		{
			status = OperationStatus.InvalidResult($"Variable name can not match an operation keyword: {identifier}");
		}

		else if (_identifierValidation.IsMatch(identifier) == false)
		{
			status = OperationStatus.InvalidResult($"Variable name is not in correct format: {identifier} does not match \"{_identifierPattern}\"");
		}

		else if (tc_parsable == false)
		{
			status = OperationStatus.InvalidResult($"Could not parse \"{args[1]}\" as a {nameof(TimeCode)}");
		}

		else if (arena.Variables.ContainsKey(identifier))
		{
			arena.Variables[identifier] = time_code;
		}

		else
		{
			arena.Variables.Add(identifier, time_code);
		}

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