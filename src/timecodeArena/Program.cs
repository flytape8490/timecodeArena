using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using timecodeArena.Operations;

namespace timecodeArena;

internal static class Program
{
	internal static int FPS { get; private set; } = 60;

	internal static void Main(string[] args)
	{
		ArgParse(args);
		
		Dictionary<string, IOperation> operations = OperationLoader();
		
		Console.WriteLine("TimeCode Arena");
		Console.WriteLine("Enter LIST to view available arguments.");

		Arena arena = new Arena(operations);
		
		arena.Run();
	}

	private static void ArgParse(string[] args)
	{
		int fps_index = -1;
		for (int i = 0; i < args.Length; i += 1)
		{
			if (args[i].CaselessEqualsAny("-f", "--fps", "fps"))
			{
				fps_index = i + 1;
				break;
			}
		}

		if (fps_index != -1)
		{
			// todo: there's an off-by-one error here?
			if (fps_index > args.Length) { throw new Exception(); }
			else { FPS = int.Parse(args[fps_index]); }
		}
	}

	private static Dictionary<string, IOperation> OperationLoader()
	{
		Assembly assembly = Assembly.GetExecutingAssembly();

		// todo: decompose this linq so that we can check for and error on duplicate keywords

		Dictionary<string, IOperation> operations = assembly.GetTypes()
															.Where(t => typeof(IOperation).IsAssignableFrom(t)
																     && t.IsClass
																     && t.IsAbstract == false)
															.Select(t => (IOperation)Activator.CreateInstance(t)!)
															.ToDictionary(op => op.Keyword, StringComparer.OrdinalIgnoreCase);

		return operations;
	}
}