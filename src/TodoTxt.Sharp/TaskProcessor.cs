using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace TodoTxt.Sharp
{
    public static class TaskProcessor
    {
        public const string PriorityRegex = @"^\([A-Z]\) {1}";
        public const string DateRegex = @"^(19|20)\d\d-(0[1-9]|1[012])-(0[1-9]|[12][0-9]|3[01])$";

        private static Regex _priorityRegex;
        private static Regex _dateRegex;

        static TaskProcessor()
        {
            _priorityRegex = new Regex(PriorityRegex);
            _dateRegex = new Regex(DateRegex);
        }

        public static void ProcessRaw(string raw, Task task)
        {
            if (raw.Length >= 1 && raw[0] == 'x')
            {
                task.IsCompleted = true;
                var completionMatch = _dateRegex.Match(raw, 2, 10);
            }

            // Do we have a chance of having a priority?
            if (raw.Length >= 3)
            {
                var prioritymatch = _priorityRegex.Match(raw, 0, 4);
                if (prioritymatch.Success)
                    task.Priority = prioritymatch.Value[1];
            }

            //Check for a creation date
            if (raw.Length >= 10)
            {
                var creationMatch = task.HasPriority ? _dateRegex.Match(raw, 4, 10) : _dateRegex.Match(raw, 0, 10);

                if (creationMatch.Success)
                    task.CreationDate = DateTime.ParseExact(creationMatch.Value, "yyyy-MM-dd", CultureInfo.InvariantCulture);
            }
        }
    }

}
