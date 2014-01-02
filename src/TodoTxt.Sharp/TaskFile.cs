using System;

namespace TodoTxt.Sharp
{
    public class TaskFile
    {
        private string _path;

        public TaskFile(string path)
        {
            _path = path;
        }

        public void Save()
        {
            
        }

        public void SaveAs(string path)
        {
            _path = path;
        }
    }
}