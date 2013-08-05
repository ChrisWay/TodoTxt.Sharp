using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.RegularExpressions;
using TodoTxt.Sharp.Annotations;

namespace TodoTxt.Sharp
{
	public class Task : INotifyPropertyChanged
	{
		private readonly StringBuilder _rawBuilder;
		private const string ContextsRegexString = @"@\S+\b(\s|$)";
		private const string ProjectsRegexString = @"\+\S+\b(\s|$)";

		private static readonly Regex ContextsRegex;
		private static readonly Regex ProjectsRegex;

		private string _raw;
		private Priority? _priority;
		private DateTime? _completionDate;
		private DateTime? _creationDate;
		private string _content;

        public Task(string raw)
			: this()
        {
            Raw = raw;
        }

        public Task()
        {
			_rawBuilder = new StringBuilder();
        }

		static Task()
		{
			ContextsRegex = new Regex(ContextsRegexString);
			ProjectsRegex = new Regex(ProjectsRegexString);
		}

		public Priority? Priority
		{
			get { return _priority; }
			set
			{
				if(_priority == value)
					return;
				
				_priority = value;
				if (_priority.HasValue)
					CompletionDate = null;

				OnPropertyChanged();
				OnPropertyChanged("Raw");
			}
		}

		public DateTime? CompletionDate
		{
			get { return _completionDate; }
			set 
			{ 
				if(_completionDate == value)
					return;

				_completionDate = value;

				if (_completionDate.HasValue)
					Priority = null;

				OnPropertyChanged();
				OnPropertyChanged("Raw");
			}
		}

		public DateTime? CreationDate
		{
			get { return _creationDate; }
			set
			{
				if(_creationDate == value)
					return;

				_creationDate = value;

				OnPropertyChanged();
				OnPropertyChanged("Raw");
			}
		}

		public string Content
		{
			get { return _content; }
			set
			{
				if (_content == value)
					return;

				_content = value;

				OnPropertyChanged();
				OnPropertyChanged("Contexts");
				OnPropertyChanged("Projects");
				OnPropertyChanged("Raw");
			}
		}

		public IEnumerable<string> Contexts
		{
			get
			{		
				return ContextsRegex.Matches(Content).Cast<Match>().Select(m => m.Value.Substring(1).TrimEnd(' '));
			}

		}
		public IEnumerable<string> Projects
		{
			get
			{
				return ProjectsRegex.Matches(Content).Cast<Match>().Select(m => m.Value.Substring(1).TrimEnd(' '));
			}
		}

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

		public event PropertyChangedEventHandler PropertyChanged;

		[NotifyPropertyChangedInvocator]
		protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
		{
			PropertyChangedEventHandler handler = PropertyChanged;
			if (handler != null) 
				handler(this, new PropertyChangedEventArgs(propertyName));
		}
	}
}
