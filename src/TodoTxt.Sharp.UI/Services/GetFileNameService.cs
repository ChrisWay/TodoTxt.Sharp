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

        /// <summary>
        /// Returns the full path to a new file.
        /// </summary>
        /// <returns>A string containing a path or null if no path can be provided</returns>
        string GetNewTodoFilePath();
    }

    public class GetFileNameService : IGetFileNameService
    {
        private const string FilterPattern = "Text files (*.txt) | *.txt|All Files (*.*) | *.*";

        public string GetTodoFilePath() {
            var dialog = new OpenFileDialog { Filter = FilterPattern };
            
            return dialog.ShowDialog() == true ? dialog.FileName : null;
        }

        public string GetNewTodoFilePath() {
            var dialog = new SaveFileDialog { FileName = "todo.txt", Filter = FilterPattern };
            
            return dialog.ShowDialog() == true ? dialog.FileName : null;
        }
    }
}