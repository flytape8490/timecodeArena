using System;
using System.Collections.Generic;
using System.Linq;

namespace timecodeArena;

internal class Arena
{
	internal Dictionary<string, TimeCode> Variables;
	internal Dictionary<string, IOperation> Operations;
	
	internal Arena(Dictionary<string, IOperation> operations)
	{
		 Variables = new Dictionary<string, TimeCode>(StringComparer.OrdinalIgnoreCase);
		 Operations = operations;
	}

	internal void Run()
	{
		Console.WriteLine("TimeCode Arena");
		while (true)
		{
			Console.Write("> ");
			string input = Console.ReadLine()!.Trim();

			if (string.IsNullOrWhiteSpace(input)) { continue; }

			string[] tokens = input.Split(' ');

			string keyword = tokens[0].ToUpper();
			string[] args = tokens.Skip(1).ToArray();

			OperationStatus status = OperationStatus.ValidResult();

			if (Operations.ContainsKey(keyword))
			{
				Console.WriteLine();
				status = Operations[keyword].Action(this, args);
			}

			else if (Variables.ContainsKey(keyword))
			{
				Console.WriteLine(Variables[keyword]);
			}

			else
			{
				status = OperationStatus.InvalidResult($"Unknown operation: {keyword} - Use 'list' to show available commands");
			}

			HandleStatus(status);
		}
	}

	private void ListCommands()
	{
		foreach (string keyword in Operations.Keys.OrderBy(o => o))
		{
			Console.WriteLine($"* {keyword}".ToUpper());
		}

		Console.WriteLine("\n* EXIT");
	}

	private static void HandleStatus(OperationStatus opStat)
	{
		if (opStat.IsSuccess) { return; }

		if (opStat.Cause != null)
		{
			Console.WriteLine($"Operation failed - {opStat.Cause}");
		}

		else if (opStat.HasException)
		{
			Console.WriteLine($"Operation failed - exception raised\n{opStat.Exception}");
		}

		else
		{
			Console.WriteLine("Operation failed - no reason given.");
		}

		if (opStat.Resolution != null)
		{
			Console.WriteLine($"Suggested resolution: {opStat.Resolution}");
		}

		Console.WriteLine();
	}
}