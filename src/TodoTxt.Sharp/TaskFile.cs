﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using TodoTxt.Sharp.Comparers;

namespace TodoTxt.Sharp
{
    //TODO Add Exception handing
    public class TaskFile
    {
        private readonly string _path;
        private DateTime _fileLastWriteTime;
        private string _newLineDelimter;

        public TaskFile(string path) {
            _path = path;
            LoadFromFile();
        }

        public string Path {
            get { return _path; }
        }

        public List<Task> Tasks { get; private set; }

        /// <summary>
        /// Loads the tasks from a file.
        /// If the file does not exist it is created.
        /// </summary>
        public void LoadFromFile(bool ignoreLastWriteTime = false) {
            if (!ignoreLastWriteTime && File.GetLastWriteTimeUtc(_path) <= _fileLastWriteTime)
                return;

            Tasks = new List<Task>();

            if (!File.Exists(_path)) {
                using (File.Create(_path)) {}
                _newLineDelimter = Environment.NewLine;
                return;
            }

            using (var file = File.OpenRead(_path))
            using (var stream = new StreamReader(file)) {
                string line;
                int filePosition = 1;
                while ((line = stream.ReadLine()) != null) {
                    if (string.IsNullOrWhiteSpace(line))
                        continue;
                    Tasks.Add(new Task(line) {FilePosition = filePosition});
                    filePosition++;
                }
            }

            _newLineDelimter = DetectNewLine(_path);
            _fileLastWriteTime = File.GetLastWriteTimeUtc(_path);
        }

        /// <summary>
        /// Adds a new task to the TodoTxt file.
        /// </summary>
        /// <param name="task">The task to add</param>
        /// <param name="filePosition">The line number where the task should be added, -1 adds to the task to the end of the file</param>
        public void AddTask(Task task, int filePosition = -1) {
            LoadFromFile();

            var rawWithNewLine = task.Raw + _newLineDelimter;
            Tasks.Add(task);
            if (filePosition == -1) {
                task.FilePosition = Tasks.Count;
                File.AppendAllText(_path, rawWithNewLine);
            }
            else {
                UpdateFilePositions(task, filePosition);
                WriteAllTasksToFile();
            }
        }

        public void DeleteTask(Task task) {
            LoadFromFile();
            File.WriteAllText(_path, string.Join(_newLineDelimter, File.ReadLines(_path).Where(l => l != task.Raw).ToList()));
            LoadFromFile();
        }

        public void UpdateTask(Task originalTask, Task newTask) {
            throw new NotImplementedException();
        }

        //TODO This needs some testing!!
        public void MoveTask(Task task, int newFilePosition) {
            LoadFromFile();

            if (newFilePosition > Tasks.Count + 1)
                throw new InvalidOperationException();

            if (!Tasks.Contains(task))
                throw new InvalidOperationException();

            UpdateFilePositions(task, newFilePosition);
            WriteAllTasksToFile();
        }

        //A method that can be used from MoveTask and AddTask
        //so we only have the task moving stuff in one place

        private void UpdateFilePositions(Task task, int filePosition) {
            // We are adding a new task at the specified position
            if (task.FilePosition == 0) {
                foreach (var task2 in Tasks.Where(t => t.FilePosition >= filePosition)) {
                    task2.FilePosition++;
                }
            }
            else {
                foreach (var task2 in Tasks.Where(t => t.FilePosition > task.FilePosition && t.FilePosition <= filePosition)) {
                    task2.FilePosition--;
                }
            }
            task.FilePosition = filePosition;
            Tasks.Sort(new TaskFilePositionComparer());
        }

        private void WriteAllTasksToFile() {
            File.WriteAllText(_path, string.Join(_newLineDelimter, Tasks.Select(t => t.Raw)));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        /// <remarks>Original Source: http://stackoverflow.com/questions/11829559/how-can-i-detect-if-a-file-has-unix-line-feeds-n-or-windows-line-feeds-r-n </remarks>
        private string DetectNewLine(string path) {
            using (var fileStream = File.OpenRead(path)) {
                var prevChar = '\0';
                // Read the first 4000 characters to try and find a newline
                for (int i = 0; i < 4000; i++) {
                    int b;
                    if ((b = fileStream.ReadByte()) == -1)
                        break;

                    var curChar = (char) b;

                    if (curChar == '\n') {
                        return prevChar == '\r' ? "\r\n" : "\n";
                    }

                    prevChar = curChar;
                }

                // Returning system default means we could not determine linefeed convention
                return Environment.NewLine;
            }
        }
    }
}