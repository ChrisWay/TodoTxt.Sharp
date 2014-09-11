using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Caliburn.Micro;

namespace TodoTxt.Sharp.UI.ViewModels
{
    public class TaskFileViewModel : Screen
    {
        private TaskFile _file;

        public TaskFileViewModel(string filePath) {
            if(string.IsNullOrWhiteSpace(filePath))
                throw new ArgumentNullException("filePath");

            File = new TaskFile(filePath);
        }

        public TaskFile File {
            get { return _file; }
            private set {
                _file = value;
                DisplayName = Path.GetFileNameWithoutExtension(File.Path);
            }
        }
    }
}
