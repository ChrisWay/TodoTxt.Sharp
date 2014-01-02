using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace TodoTxt.Sharp
{
    public class TaskFile
    {
        private readonly string _path;
        private bool _endsWithNewLine;
        private DateTime _fileLastWriteTime;

        public TaskFile(string path)
        {
            _path = path;
            LoadFromFile();
        }

        public List<Task> Tasks { get; private set; }

        /// <summary>
        /// Loads the tasks from a file.
        /// If the file does not exist it is created.
        /// </summary>
        public async void LoadFromFile(bool ignoreLastWriteTime = false)
        {
            if (!ignoreLastWriteTime && File.GetLastWriteTimeUtc(_path) <= _fileLastWriteTime)
                return;

            Tasks = new List<Task>();

            if (!File.Exists(_path)) {
                File.Create(_path);
                return;
            }

            using (var file = File.OpenRead(_path))
            using (var stream = new StreamReader(file)) {
                string line;
                while ((line = await stream.ReadLineAsync()) != null) {
                    if (!string.IsNullOrWhiteSpace(line)) {
                        Tasks.Add(new Task(line));
                        _endsWithNewLine = line.EndsWith(Environment.NewLine);
                    }
                }
            }

            _fileLastWriteTime = File.GetLastWriteTimeUtc(_path);
        }

        public void AddTask(Task task)
        {
            LoadFromFile();

            var rawWithNewLine = task.Raw + Environment.NewLine;
            var toWrite = _endsWithNewLine ? rawWithNewLine : Environment.NewLine + rawWithNewLine;

            File.AppendAllText(_path, toWrite);

            Tasks.Add(task);
        }

        public void DeleteTask(Task task)
        {
            LoadFromFile();
            File.WriteAllLines(_path, File.ReadLines(_path).Where(l => l != task.Raw).ToList());

            Tasks.Remove(task);
        }

        public void UpdateTask(Task originalTask, Task newTask)
        {
            throw new NotImplementedException();
        }
    }
}