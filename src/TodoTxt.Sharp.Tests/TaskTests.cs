using NUnit.Framework;
using System;

namespace TodoTxt.Sharp.Tests
{
    [TestFixture]
    public class TaskTests
    {
		//Basic Tests

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
		public void Task_OnlyContent_EverthingNullExceptContent()
		{
			var t = new Task("Some task");

			Assert.IsNull(t.CompletionDate);
			Assert.IsNull(t.Priority);
			Assert.IsFalse(t.IsComplete);
			Assert.IsNull(t.CreationDate);
			Assert.AreEqual(0, t.Projects.Count);
			Assert.AreEqual(0, t.Contexts.Count);
			Assert.AreEqual("Some task", t.Content);
		}

		[Test]
		public void Task_Content_ContentShouldOnlyContainContent()
		{
			var t = new Task("(A) 2013-08-02 Some task");

			Assert.AreEqual("Some task", t.Content);
		}

		//Incomplete Task Tests

        [Test]
        public void Priority_TaskWithAPriority_IsA()
        {
            var t = new Task("(A) a task");
            
            Assert.AreEqual(Priority.A, t.Priority);
			Assert.AreEqual("(A) a task", t.Raw);
        }

        [Test]
        public void Priority_TaskWithALowerCasePriority_IsNull()
        {
            var t = new Task("(a) a task");

            Assert.IsNull(t.Priority);
			Assert.AreEqual("(a) a task", t.Raw);
        }

        [Test]
        public void Priority_TaskWithTwoPriorities_IsNull()
        {
            var t = new Task("(A)(B) a task");

            Assert.IsNull(t.Priority);
			Assert.AreEqual("(A)(B) a task", t.Raw);
        }

        [Test]
        public void Priority_TaskWithNoPriority_IsNull()
        {
            var t = new Task("a task");

            Assert.IsNull(t.Priority);
			Assert.AreEqual("a task", t.Raw);
        }

        [Test]
        public void CreationDate_TaskWithACreationDateAtStart_CreationDateIsNotNull()
        {
            var t = new Task("2013-02-05 some task");

            Assert.AreEqual(new DateTime(2013, 2, 5), t.CreationDate);
			Assert.IsNull(t.Priority);
			Assert.AreEqual("2013-02-05 some task", t.Raw);
        }

        [Test]
        public void CreationDate_TaskWithPriorityAndACreationDate_CreationDateIsNotNull()
        {
            var t = new Task("(A) 2013-02-05 some task");

            Assert.AreEqual(new DateTime(2013, 2, 5), t.CreationDate);
			Assert.AreEqual(Priority.A, t.Priority);
			Assert.AreEqual("(A) 2013-02-05 some task", t.Raw);
        }

		// Completed Task Tests

		[Test]
		public void CompletionDate_CompletedTask_CompletionDateIsSet()
		{
			var t = new Task("x 2013-02-06 some task");

			Assert.AreEqual(new DateTime(2013,02,06), t.CompletionDate);
			Assert.IsTrue(t.IsComplete);
			Assert.AreEqual("x 2013-02-06 some task", t.Raw);
		}

		[Test]
		public void CompletionDate_SetCompletionDate_CompletionDateIsSetToSpecifiedDateAndRaw()
		{
			var t = new Task("some task");
			t.CompletionDate = new DateTime(2013,8,2);

			Assert.AreEqual(new DateTime(2013, 8, 2), t.CompletionDate);
			Assert.AreEqual("x 2013-08-02 some task", t.Raw);
		}

		[Test]
		public void CompletionDate_SetCompletionDate_CompletionDateIsSetToSpecifiedDateAndCreationDateIntactAndRaw()
		{
			var t = new Task("2013-08-01 some task");
			t.CompletionDate = new DateTime(2013, 8, 2);

			Assert.AreEqual(new DateTime(2013, 8, 2), t.CompletionDate);
			Assert.AreEqual(new DateTime(2013, 8, 1), t.CreationDate);
			Assert.AreEqual("x 2013-08-02 2013-08-01 some task", t.Raw);
		}


		[Test]
		public void CompletionDate_CompletionAndCreationDate_CompletionDateIsSetToCorrectDateCreationDateIsCorrect()
		{
			var t = new Task("x 2013-02-05 2013-02-04 some task");

			Assert.AreEqual(new DateTime(2013,2,5), t.CompletionDate);
			Assert.AreEqual(new DateTime(2013, 2, 4), t.CreationDate);
			Assert.AreEqual("x 2013-02-05 2013-02-04 some task", t.Raw);
			Assert.IsTrue(t.IsComplete);
		}

		[Test]
		public void PriorityCompleted_SetIsCompletedToTrueOnATaskWithPriority_PriorityIsRemoved()
		{
			var t = new Task("(A) 2013-02-05 some task");
			t.CompletionDate = new DateTime(2013,8,2);

			Assert.AreEqual(new DateTime(2013, 8, 2), t.CompletionDate);
			Assert.AreEqual(new DateTime(2013, 2, 5), t.CreationDate);
			Assert.AreEqual("x 2013-08-02 2013-02-05 some task", t.Raw);
			Assert.IsTrue(t.IsComplete);
		}
    }
}
