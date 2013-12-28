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
        private const string ContextsRegexString = @"@\S+\b(\s|$)";
        private const string ProjectsRegexString = @"\+\S+\b(\s|$)";

        private static readonly Regex ContextsRegex;
        private static readonly Regex ProjectsRegex;
        private static readonly Regex PriorityRegex;
        private static readonly Regex DateRegex;
        private static readonly Regex CompletedRegex;

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
        }

        static Task()
        {
            ContextsRegex = new Regex(ContextsRegexString);
            ProjectsRegex = new Regex(ProjectsRegexString);
            const string dateRegexString = @"(19|20)\d\d-(0[1-9]|1[012])-(0[1-9]|[12][0-9]|3[01]) ";

            PriorityRegex = new Regex(@"^\([A-Z]\) {1}");
            DateRegex = new Regex("^" + dateRegexString);
            CompletedRegex = new Regex("^x " + dateRegexString);
        }

        public Priority? Priority
        {
            get { return _priority; }
            private set { _priority = value; }
        }

        public DateTime? CompletionDate
        {
            get { return _completionDate; }
            private set { _completionDate = value; }
        }

        public DateTime? CreationDate
        {
            get { return _creationDate; }
            private set { _creationDate = value; }
        }

        public string Content
        {
            get { return _content; }
            private set { _content = value; }
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
            get { return _raw; }
            set
            {
                _raw = value;
                ProcessRaw(_raw);
            }
        }

        public override string ToString()
        {
            return Raw;
        }

        private void ProcessRaw(string raw)
        {
            var isCompleted = CompletedRegex.IsMatch(raw);
            if (isCompleted)
            {
                CompletionDate = DateTime.Parse(raw.Substring(2, 10));
                raw = raw.Substring(13);
            }

            var hasPriority = PriorityRegex.IsMatch(raw);
            if (hasPriority)
            {
                Priority = (Priority)Enum.Parse(typeof(Priority), raw.Substring(1, 1));
                raw = raw.Substring(4);
            }

            var hasCreationDate = DateRegex.IsMatch(raw);
            if (hasCreationDate)
            {
                CreationDate = DateTime.Parse(raw.Substring(0, 10));
                raw = raw.Substring(11);
            }

            Content = raw;
        }

        public void IncreasePriority()
        {
            ChangePriority(true);
        }

        public void ReducePriority()
        {
            ChangePriority(false);
        }

        private void ChangePriority(bool increase)
        {
            if (!Priority.HasValue)
            {
                Priority = Sharp.Priority.A;
                Raw = Raw.Insert(0, string.Format("({0}) ", Priority));
                return;
            }

            if(Priority == (increase ? Sharp.Priority.A : Sharp.Priority.Z))
                return;

            if (increase)
                Priority--;
            else 
                Priority++;

            Raw = Raw.Remove(1, 1).Insert(1, Priority.ToString());
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