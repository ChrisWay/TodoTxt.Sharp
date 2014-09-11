using System.Windows.Input;
using Caliburn.Micro;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TodoTxt.Sharp.UI.Services;

namespace TodoTxt.Sharp.UI.ViewModels
{
    public class ShellViewModel : Conductor<IScreen>.Collection.OneActive
    {
        private readonly IGetFileNameService _fileNameService;

        public ShellViewModel(IGetFileNameService fileNameService) {
            _fileNameService = fileNameService;
        }

        protected override void OnInitialize() {
            base.OnInitialize();
            DisplayName = "TodoTxt Sharp";
        }

        public void OpenFile() {
            var fileName = _fileNameService.GetTodoFilePath();
            if (fileName == null)
                return;

            ActivateItem(new TaskFileViewModel(fileName));
        }
    }
}
