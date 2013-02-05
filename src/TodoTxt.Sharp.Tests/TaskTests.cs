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
        public void Priority_TaskWithAPriority_IsA()
        {
            var t = new Task("(A) a task");
            
            Assert.AreEqual('A', t.Priority);
            Assert.IsTrue(t.HasPriority);
        }

        [Test]
        public void Priority_TaskWithALowerCasePriority_IsNull()
        {
            var t = new Task("(a) a task");

            Assert.IsNull(t.Priority);
            Assert.IsFalse(t.HasPriority); 
        }

        [Test]
        public void Priority_TaskWithTwoPriorities_IsA()
        {
            var t = new Task("(A)(B) a task");
            
            Assert.AreEqual('A', t.Priority);
            Assert.IsTrue(t.HasPriority);
        }

        [Test]
        public void Priority_TaskWithNoPriority_IsNull()
        {
            var t = new Task("a task");

            Assert.IsNull(t.Priority);
            Assert.IsFalse(t.HasPriority);
        }
    }
}
