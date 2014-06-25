using System.Windows.Input;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TodoTxt.Sharp.UI.Library;

namespace TodoTxt.Sharp.UI.ViewModels
{
    public class MainWindowViewModel : NotifyPropertyChanged
    {
        public MainWindowViewModel()
        {
            OpenFileCommand = new SimpleCommand(() => {
                var ofd = new OpenFileDialog();

                if (ofd.ShowDialog() ?? false) {
                    File = new TaskFile(ofd.FileName);
                }
             });
        }

        public SimpleCommand OpenFileCommand { get; set; }

        private TaskFile _file;
        public TaskFile File
        {
            get { return _file; }
            set { RaiseAndSetIfChanged(ref _file, value);}
        }
    }
}
