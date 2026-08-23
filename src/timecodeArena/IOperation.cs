using System;
using System.Collections.Generic;

namespace timecodeArena;

internal interface IOperation
{
	public string Keyword { get; }
	public OperationHelp Help { get; }

	public OperationStatus Action(Arena arena, IList<string> args);

	public OperationStatus ValidateArgs(IList<string> args);
}