using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Win32;

namespace TodoTxt.Sharp.UI.Services
{
    public interface IGetFileNameService
    {
        string GetTodoFilePath();
    }

    public class GetFileNameService : IGetFileNameService
    {
        public string GetTodoFilePath() {
            var dialog = new OpenFileDialog();
            dialog.ShowDialog();

            return dialog.FileName;
        }
    }
}