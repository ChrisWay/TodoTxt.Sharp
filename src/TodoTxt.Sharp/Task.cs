using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TodoTxt.Sharp
{
	public class Task
	{
		private readonly StringBuilder _rawBuilder;
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
			: this()
        {
            Raw = raw;
        }

        public Task()
        {
			_rawBuilder = new StringBuilder();
        }

		public Priority? Priority { get; set; }

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
	            _rawBuilder.Clear();

				if (CompletionDate.HasValue)
					_rawBuilder.AppendFormat("x {0} ", CompletionDate.Value.ToString("yyyy-MM-dd"));
				else if (Priority.HasValue)
					_rawBuilder.AppendFormat("({0}) ", Priority.Value.ToString());

				if (CreationDate.HasValue)
					_rawBuilder.Append(CreationDate.Value.ToString("yyyy-MM-dd")).Append(" ");

				_rawBuilder.Append(Content);

				return _rawBuilder.ToString();
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
