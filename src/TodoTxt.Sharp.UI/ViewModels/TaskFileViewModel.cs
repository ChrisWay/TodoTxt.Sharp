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

        public TaskFileViewModel(string filePath, bool shouldFileBeWiped = false) {
            if(string.IsNullOrWhiteSpace(filePath))
                throw new ArgumentNullException("filePath");

            // If the user has chosen to create a new file (shouldFileBeWiped == true)
            // and that file already exits that means they said Yes to the overwrite prompt
            if (shouldFileBeWiped && System.IO.File.Exists(filePath)) {
                System.IO.File.Delete(filePath);
            }

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
