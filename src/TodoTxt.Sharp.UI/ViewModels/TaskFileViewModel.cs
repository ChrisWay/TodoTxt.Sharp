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
        private bool _isInEdit;
        private Task _selectedTodo;

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

        public bool IsInEdit {
            get { return _isInEdit; }
            set {
                if (value.Equals(_isInEdit))
                    return;
                _isInEdit = value;
                NotifyOfPropertyChange(() => IsInEdit);
            }
        }

        public Task SelectedTodo {
            get { return _selectedTodo; }
            set {
                if (Equals(value, _selectedTodo))
                    return;
                _selectedTodo = value;
                NotifyOfPropertyChange(() => SelectedTodo);
            }
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
