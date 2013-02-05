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

        private bool _isCompleted;
        private bool _isSettingCompleted;
		public bool IsCompleted 
        {
            get { return _isCompleted; }
            set
            {
                _isCompleted = value;
                if (!_isSettingCompleted)
                {
                    _isSettingCompleted = true;
                    CompletionDate = _isCompleted ? (DateTime?)DateTime.Today : null;
                    _isSettingCompleted = false;
                }
            }
        }

        private DateTime? _completionDate;
		public DateTime? CompletionDate 
        {
            get { return _completionDate; }
            set
            {
                _completionDate = value;
                if (!_isSettingCompleted)
                {
                    _isSettingCompleted = true;
                    IsCompleted = _completionDate.HasValue;
                    _isSettingCompleted = false;
                }
            }
        }
		
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
