using System;

namespace timecodeArena;

internal class OperationHelp
{
	internal string Name { get; }
	internal string Description { get; }
	internal string Usage { get; }
	internal string? Remarks { get; }

	internal OperationHelp(string name, string description, string usage, string? remarks = null)
	{
		Name = name;
		Description = description;
		Usage = usage;
		Remarks = remarks;
	}

	internal void Show()
	{
		Console.WriteLine($"Command Name: {Name}");
		Console.WriteLine($"Usage: {Usage}");
		Console.WriteLine($"\n{Description}");

		if (Remarks != null) { Console.WriteLine($"Note: {Remarks}"); }
	}
}