using System;
using System.Collections.Generic;
using System.Linq;

namespace timecodeArena;

internal class Arena
{
	internal Dictionary<string, TimeCode> Variables;
	internal Dictionary<string, IOperation> Operations;
	
	private bool _IsRunning = true;
	
	internal Arena(Dictionary<string, IOperation> operations)
	{
		 Variables = new Dictionary<string, TimeCode>(StringComparer.OrdinalIgnoreCase);
		 Operations = operations;
	}

	/// <summary>Determines if a string is an assigned identifier or if it's a <see cref="TimeCode"/> literal.</summary>
	internal OperationStatus GetTimeCodeOrVariable(string code, out TimeCode? timeCode)
	{
		if (TimeCode.TryParse(code, out timeCode) == false)
		{
			if (Variables.ContainsKey(code))
			{
				timeCode = Variables[code];
			}
			else
			{
				return OperationStatus.InvalidResult($"Not a valid time code or identifier doesn't exist: {code}");
			}
		}

		return OperationStatus.ValidResult();
	}

	/// <summary>The primary execution loop.</summary>
	internal void Run()
	{
		while (_IsRunning)
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

	/// <summary>Lists all loaded operation keywords.</summary>
	internal void ListCommands()
	{
		foreach (string keyword in Operations.Keys.OrderBy(o => o))
		{
			Console.WriteLine($"* {keyword}".ToUpper());
		}
	}

	private static void HandleStatus(OperationStatus opStat)
	{
		if (opStat.IsSuccess) { return; }

		if (opStat.HasCause)
		{
			Console.WriteLine($"Operation failed - {opStat.Cause}");
			if (opStat.HasException)
			{
				Console.WriteLine($"Exception:\n{opStat.Exception}");
			}
		}

		else if (opStat.HasException)
		{
			Console.WriteLine($"Operation failed - exception raised\n{opStat.Exception}");
		}

		else
		{
			Console.WriteLine("Operation failed - no reason given.");
		}

		if (opStat.HasResolution)
		{
			Console.WriteLine($"Suggested resolution: {opStat.Resolution}");
		}

		Console.WriteLine();
	}

	internal void Shutdown()
	{
		_IsRunning = false;
	}
}