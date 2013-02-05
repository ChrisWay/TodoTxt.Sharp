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
        public void Priority_TaskWithTwoPriorities_IsNull()
        {
            var t = new Task("(A)(B) a task");

            Assert.IsNull(t.Priority);
            Assert.IsFalse(t.HasPriority);
        }

        [Test]
        public void Priority_TaskWithNoPriority_IsNull()
        {
            var t = new Task("a task");

            Assert.IsNull(t.Priority);
            Assert.IsFalse(t.HasPriority);
        }

        [Test]
        public void CreationDate_TaskWithACreationDate_CreationDateIsNotNull()
        {
            var t = new Task("2013-02-05 some task");

            Assert.AreEqual(new DateTime(2013, 2, 5), t.CreationDate);
        }

        [Test]
        public void CreationDate_TaskWithPriorityAndACreationDate_CreationDateIsNotNull()
        {
            var t = new Task("(A) 2013-02-05 some task");

            Assert.AreEqual(new DateTime(2013, 2, 5), t.CreationDate);
        }

        [Test]
        public void CreationDate_TaskWithNoCreationDate_CreationDateIsNull()
        {
            var t = new Task("some task");

            Assert.IsNull(t.CreationDate);
        }

        //Test for setting IsCompleted sets DateCompleted to todays date and setting DateCompleted sets IsCompleted to true.
    }
}
