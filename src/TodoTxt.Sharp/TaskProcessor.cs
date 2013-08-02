using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace TodoTxt.Sharp
{
	internal static class TaskProcessor
	{
		private static readonly Regex PriorityRegex;
		private static readonly Regex DateRegex;
		private static readonly Regex CompletedRegex;

		static TaskProcessor()
		{
			const string DateRegexString = @"(19|20)\d\d-(0[1-9]|1[012])-(0[1-9]|[12][0-9]|3[01]) ";
			
			PriorityRegex = new Regex(@"^\([A-Z]\) {1}");
			DateRegex = new Regex("^" + DateRegexString);
			CompletedRegex = new Regex("^x " + DateRegexString);
		}

		public static void ProcessRaw(string raw, Task task)
		{
			var isCompleted = CompletedRegex.IsMatch(raw);
			if (isCompleted)
			{
				task.CompletionDate = DateTime.Parse(raw.Substring(2, 10));
				raw = raw.Substring(13);
			}

			var hasPriority = PriorityRegex.IsMatch(raw);
			if (hasPriority)
			{
				task.Priority = (Priority)Enum.Parse(typeof(Priority), raw.Substring(1, 1));
				raw = raw.Substring(4);
			}

			var hasCreationDate = DateRegex.IsMatch(raw);
			if (hasCreationDate)
			{
				task.CreationDate = DateTime.Parse(raw.Substring(0, 10));
				raw = raw.Substring(11);
			}

			

			task.Content = raw;
		}
	}
}
