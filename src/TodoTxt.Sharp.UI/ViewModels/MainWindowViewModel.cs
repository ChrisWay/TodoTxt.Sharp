using System.Windows.Input;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ReactiveUI;
using TodoTxt.Sharp.UI.Services;

namespace TodoTxt.Sharp.UI.ViewModels
{
    public class MainWindowViewModel : ReactiveObject
    {
        private readonly IGetFileNameService _fileNameService;

        public MainWindowViewModel(IGetFileNameService fileNameService) {
            _fileNameService = fileNameService;

            OpenFileCommand = ReactiveCommand.Create();
            OpenFileCommand.Subscribe(x => {
                var fileName = _fileNameService.GetTodoFilePath();
                if (fileName == null)
                    return;

                FileViewModels.Add(new TaskFileViewModel(new TaskFile(fileName)));
            });

            FileViewModels = new ReactiveList<TaskFileViewModel>();
        }

        public ReactiveCommand<object> OpenFileCommand { get; protected set; }
        public ReactiveList<TaskFileViewModel> FileViewModels { get; protected set; } 
    }
}
