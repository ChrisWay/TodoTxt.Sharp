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

		public Priority? Priority { get; set; }

		public bool IsComplete
		{
			get { return CompletionDate.HasValue; }
		}

		public DateTime? CompletionDate { get; set; }

		public DateTime? CreationDate { get; set; }

		public string Content { get; set; }

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
