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

	// todo: perhaps refactor show in to the arena? the arena is already 
	//       handling the status display moving it over there causes "class 
	//       does too many things" and "tight coupling" smells... perhaps have
	//       a display controller class that arena talks to? so if we were to 
    //       reimplement this with a gui all we would need to rewrite is the 
    //       display controller
	internal void Show()
	{
		Console.WriteLine($"Command Name: {Name}");
		Console.WriteLine($"Usage: {Usage}");
		Console.WriteLine($"\n{Description}");

		if (Remarks != null) { Console.WriteLine($"Note: {Remarks}"); }
	}
}