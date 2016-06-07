using CortanaExtensions.Enums;
using CortanaExtensions.Models;
using CortanaExtensions.PhraseGroups;
using System;
using System.Collections.Generic;

namespace CortanaExtensions.Common
{
	public static class ValueInterpreter
	{
		/// <summary>
		/// Deserialise Custom Markup into VCD compatible ListenFor Elements, see https://github.com/WilliamABradley/CortanaExtensions for more info.
		/// </summary>
		/// <param name="Raw">Serialised string</param>
		/// <returns>Listen Statements</returns>
		public static List<ListenStatement> DeserialiseListenStatement(string Raw)
		{
			var result = new List<ListenStatement>();
			foreach (var Group in Raw.Split(new string[] { " #!# " }, StringSplitOptions.RemoveEmptyEntries))
			{
				var Statement = Group.Split(new string[] { " \\/ " }, StringSplitOptions.None);
				result.Add
				(
					new ListenStatement(Statement[0], EnumHelper.FromLabel<RequireAppName>(Statement[1]))
				);
			}
			return result;
		}
		/// <summary>
		/// Deserialise Custom Markup into VCD compatible PhraseLists, see https://github.com/WilliamABradley/CortanaExtensions for more info.
		/// </summary>
		/// <param name="Raw">Serialised string</param>
		/// <returns>PhraseLists</returns>
		public static List<PhraseList> DeserialisePhraseList(string Raw)
		{
			var result = new List<PhraseList>();
			foreach (var phraselist in Raw.Split(new string[] { " #!# " }, StringSplitOptions.RemoveEmptyEntries))
			{
				var Entry = phraselist.Split(new string[] { " \\/ " }, StringSplitOptions.None);
				result.Add(new PhraseList(Entry[0], Entry[1].Split('\\')));
			}
			return result;
		}
	}
}
