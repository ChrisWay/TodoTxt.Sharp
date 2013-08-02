using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TodoTxt.Sharp
{
	public class Task
	{
		/* 
		 * Raw getter is calculated from:
		 *	Are we completed?
		 *	- Yes
		 *		x "x 2013-08-02 a task"
		 *		Date of Completion "x 2013-08-02 a task"
		 *		(Optional) Creation Date "x 2013-08-02 2013-08-01 a task"
		 *	- No
		 *		(Optional) Priority "(A) a task"
		 *		(Optional) Creation Date "(A) 2013-08-02 a task" / "2013-08-02 a task"
		 *		Content
		 */
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
            get 
			{ 
				var sb = new StringBuilder();
				if (IsComplete)
					sb.AppendFormat("x {0} ", CompletionDate.Value.ToString("yyyy-MM-dd"));
				else if (Priority.HasValue)
					sb.AppendFormat("({0}) ", Priority.Value.ToString());

				if (CreationDate.HasValue)
					sb.Append(CreationDate.Value.ToString("yyyy-MM-dd")).Append(" ");

				sb.Append(Content);

				return sb.ToString();
			}
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
