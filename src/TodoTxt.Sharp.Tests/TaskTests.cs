using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TodoTxt.Sharp.Tests
{
    [TestFixture]
    public class TaskTests
    {
        [Test]
        public void Priority_ATaskWithAPriority_PriorityPropertySet()
        {
            var t = new Task("(A) a task");
            
            Assert.AreEqual('A', t.Priority);
            Assert.IsTrue(t.HasPriority);
        }
    }
}
