using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace TodoTxt.Sharp
{
    public static class TaskProcessor
    {
        public const string PriorityRegex = @"^\([A-Z]\){1}";

        private static Regex _priorityRegex;

        static TaskProcessor()
        {
            _priorityRegex = new Regex(PriorityRegex);
        }

        public static void ProcessRaw(string raw, Task task)
        {
            // Do we have a chance of having a priority?
            if (raw.Length >= 3)
            {
                var match = _priorityRegex.Match(raw, 0, 3);
                if (match.Success)
                    task.Priority = match.Value[1];
            }
        }
    }
}
