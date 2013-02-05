using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TodoTxt.Sharp
{
	public class Task
	{
        public Task(string raw)
        {
            Raw = raw;
        }

        public Task()
        {
        }

		public string Content { get; set; }

		public bool HasPriority
		{
			get { return Priority.HasValue; }
		}

		public char? Priority { get; set; }

		public bool IsCompleted { get; set; }
		public DateTime? CompletionDate { get; set; }
		
		public DateTime? CreationDate { get; set; }

		public IList<string> Contexts { get; set; }
		public IList<string> Projects { get; set; }

        private string _raw;
		public string Raw 
        {
            get { return _raw; }
            set
            {
                _raw = value;
                TaskProcessor.ProcessRaw(_raw, this);
            }
        }

        public override string ToString()
        {
            return Raw;
        }
	}
}
