using System;
using System.Text.RegularExpressions;

namespace TodoTxt.Sharp
{
	internal static class TaskProcessor
	{
		private static readonly Regex PriorityRegex;
		private static readonly Regex DateRegex;
		private static readonly Regex CompletedRegex;

		static TaskProcessor()
		{
			const string dateRegexString = @"(19|20)\d\d-(0[1-9]|1[012])-(0[1-9]|[12][0-9]|3[01]) ";
			
			PriorityRegex = new Regex(@"^\([A-Z]\) {1}");
			DateRegex = new Regex("^" + dateRegexString);
			CompletedRegex = new Regex("^x " + dateRegexString);
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
