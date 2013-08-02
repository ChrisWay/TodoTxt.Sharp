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
		public void Task_ABlankTask_AllDefault()
		{
			var t = new Task(string.Empty);

			Assert.AreEqual(string.Empty,t.Content);
			Assert.IsNull(t.Priority);
			Assert.IsFalse(t.IsComplete);
			Assert.IsNull(t.CompletionDate);
			Assert.IsNull(t.CreationDate);
			Assert.AreEqual(0,t.Projects.Count);
			Assert.AreEqual(0, t.Contexts.Count);
		}

        [Test]
        public void Priority_TaskWithAPriority_IsA()
        {
            var t = new Task("(A) a task");
            
            Assert.AreEqual('A', t.Priority);
        }

        [Test]
        public void Priority_TaskWithALowerCasePriority_IsNull()
        {
            var t = new Task("(a) a task");

            Assert.IsNull(t.Priority);
        }

        [Test]
        public void Priority_TaskWithTwoPriorities_IsNull()
        {
            var t = new Task("(A)(B) a task");

            Assert.IsNull(t.Priority);
        }

        [Test]
        public void Priority_TaskWithNoPriority_IsNull()
        {
            var t = new Task("a task");

            Assert.IsNull(t.Priority);
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

		[Test]
		public void CompletionDate_CompletedTask_CompletionDateIsSet()
		{
			var t = new Task("x 2013-02-06 some task");

			Assert.AreEqual(new DateTime(2013,02,06), t.CompletionDate);
			Assert.IsTrue(t.IsCompleted);

		}

		[Test]
		public void CompletionDate_SetIsCompletedToTrue_CompletionDateIsSetToTodaysDate()
		{
			var t = new Task("some task");
			t.IsCompleted = true;

			Assert.AreEqual(DateTime.Today, t.CompletionDate);
			Assert.IsTrue(t.IsCompleted);

		}

		[Test]
		public void CompletionDate_SetCompletedDate_IsCompleteIsTrue()
		{
			var t = new Task("some task");
			t.CompletionDate = DateTime.Today;

			Assert.AreEqual(DateTime.Today, t.CompletionDate);
			Assert.IsTrue(t.IsCompleted);

		}


		[Test]
		public void CompletionDate_ACompletedTask_IsCompleteIsTrueCompleteDateIsSet()
		{
			var t = new Task("x 2013-02-05 some task");

			Assert.AreEqual(new DateTime(2013, 2, 5), t.CompletionDate);
			Assert.IsTrue(t.IsCompleted);

		}

		[Test]
		public void CompletionDate_SetIsCompletedToTrueOnATaskWithCreationDate_CompletionDateIsSetToTodaysDateCreationDateIsCorrect()
		{
			var t = new Task("2013-02-05 some task");
			t.IsCompleted = true;

			Assert.AreEqual(DateTime.Today, t.CompletionDate);
			Assert.AreEqual(new DateTime(2013, 2, 5), t.CreationDate);
			Assert.IsTrue(t.IsCompleted);

		}

		[Test]
		public void PriorityCompleted_SetIsCompletedToTrueOnATaskWithPriority_PriorityIsRemoved()
		{
			var t = new Task("(A) 2013-02-05 some task");
			t.IsCompleted = true;

			Assert.AreEqual(DateTime.Today, t.CompletionDate);
			Assert.AreEqual(new DateTime(2013, 2, 5), t.CreationDate);
			Assert.IsTrue(t.IsCompleted);
			Assert.IsFalse(t.HasPriority);

		}
    }
}
