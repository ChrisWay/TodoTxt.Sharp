using System;
using System.Collections.Generic;

namespace TodoTxt.Sharp.Comparers
{
    public class TaskFilePositionComparer : IComparer<Task>
    {
        public int Compare(Task x, Task y)
        {
            return x.FilePosition.CompareTo(y.FilePosition);
        }
    }
}
