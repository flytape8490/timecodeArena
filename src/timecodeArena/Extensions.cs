using System;
using System.Collections.Generic;
using System.Linq;

namespace timecodeArena;

internal static class Extensions
{
	private static StringComparison _ordIgnoreCase = StringComparison.OrdinalIgnoreCase;

	public static bool CaselessEquals(this string str, string other)
		=> str.Equals(other, _ordIgnoreCase);

	public static bool CaselessEqualsAny(this string str, params string[] others)
		=> others.CaselessContains(str);

	public static bool CaselessContains(this IEnumerable<string> strEnumerable, string value)
		=> strEnumerable.Any(s => s.CaselessEquals(value));
	
	public static bool CaselessContainsAny(this IEnumerable<string> strEnumerable, params string[] values)
		=> values.Any(v => strEnumerable.CaselessContains(v));
}