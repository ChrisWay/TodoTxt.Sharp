using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ReactiveUI;

namespace TodoTxt.Sharp.UI.ViewModels
{
    public class TaskFileViewModel : ReactiveObject
    {
        private readonly TaskFile _file;

        public TaskFileViewModel(TaskFile file) {
            if(file == null)
                throw new ArgumentNullException("file");

            _file = file;
        }

        public string Name {
            get { return Path.GetFileNameWithoutExtension(File.Path); }
        }

        public TaskFile File {
            get { return _file; }
        }
    }
}
