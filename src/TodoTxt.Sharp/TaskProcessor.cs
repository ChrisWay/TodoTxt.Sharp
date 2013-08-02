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

        static TaskProcessor()
        {
            PriorityRegex = new Regex(@"^\([A-Z]\) {1}");
            DateRegex = new Regex(@"^(19|20)\d\d-(0[1-9]|1[012])-(0[1-9]|[12][0-9]|3[01])$");
        }

        public static void ProcessRaw(string raw, Task task)
        {
            if (raw.Length >= 1 && raw[0] == 'x')
            {
                task.IsCompleted = true;
                var completionMatch = DateRegex.Match(raw, 2, 10);
            }
            else
            {
				// Do we have a chance of having a priority?
				if (raw.Length >= 3)
				{
					var prioritymatch = PriorityRegex.Match(raw, 0, 4);
					if (prioritymatch.Success)
						task.Priority = prioritymatch.Value[1];
				}
            }

            //Check for a creation date
            if (raw.Length >= 10)
            {
                var creationMatch = task.HasPriority ? DateRegex.Match(raw, 4, 10) : DateRegex.Match(raw, 0, 10);

                if (creationMatch.Success)
                    task.CreationDate = DateTime.ParseExact(creationMatch.Value, "yyyy-MM-dd", CultureInfo.InvariantCulture);
            }
        }
    }

}
