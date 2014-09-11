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
        /// <summary>
        /// Returns the full path to a file
        /// </summary>
        /// <returns>A string containing a path or null if no path can be provided</returns>
        string GetTodoFilePath();
    }

    public class GetFileNameService : IGetFileNameService
    {
        public string GetTodoFilePath() {
            var dialog = new OpenFileDialog {Filter = "Text files (*.txt) | *.txt"};
            
            return dialog.ShowDialog() == true ? dialog.FileName : null;
        }
    }
}