using System.Linq;
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
			Assert.IsNull(t.CompletionDate);
			Assert.IsNull(t.CreationDate);
			Assert.AreEqual(0,t.Projects.Count());
			Assert.AreEqual(0, t.Contexts.Count());
		}

		[Test]
		public void Task_OnlyContent_EverthingNullExceptContent()
		{
			var t = new Task("Some task");

			Assert.IsNull(t.CompletionDate);
			Assert.IsNull(t.Priority);
			Assert.IsNull(t.CreationDate);
			Assert.AreEqual(0, t.Projects.Count());
			Assert.AreEqual(0, t.Contexts.Count());
			Assert.AreEqual("Some task", t.Content);
		}

		[Test]
		public void Task_Content_ContentShouldOnlyContainContent()
		{
			var t = new Task("(A) 2013-08-02 Some task");

			Assert.AreEqual("Some task", t.Content);
		}

		[Test]
		public void Task_Projects_ProjectsListShouldContainProjects()
		{
			var t = new Task("Some task +TestProject +AnotherProject");

			Assert.IsNotNull(t.Projects);
			Assert.IsTrue(t.Projects.Contains("TestProject"));
			Assert.IsTrue(t.Projects.Contains("AnotherProject"));
		}

		[Test]
		public void Task_Contexts_ContextsListShouldContainContexts()
		{
			var t = new Task("Some task @TestContext @AnotherContext");

			Assert.IsNotNull(t.Contexts);
			Assert.IsTrue(t.Contexts.Contains("TestContext"));
			Assert.IsTrue(t.Contexts.Contains("AnotherContext"));
		}

		[Test]
		public void Task_ContextsProjects_MixContextsAndProjects()
		{
			var t = new Task("Some task @TestContext +AProject @AnotherContext");

			Assert.IsNotNull(t.Contexts);
			Assert.IsNotNull(t.Projects);
			Assert.IsTrue(t.Contexts.Contains("TestContext"));
			Assert.IsTrue(t.Contexts.Contains("AnotherContext"));
			Assert.IsTrue(t.Projects.Contains("AProject"));
		}

		[Test]
		public void Task_Projects_ProjectsListShouldNotContainInvalidProjects()
		{
			var t = new Task("Some task +TestProject +AnotherProject!");

			Assert.IsNotNull(t.Projects);
			Assert.IsTrue(t.Projects.Contains("TestProject"));
			Assert.IsFalse(t.Projects.Contains("AnotherProject!"));
		}

		[Test]
		[Ignore]
		public void Task_Projects_ProjectsListShouldBeAfterPriority()
		{
			var t = new Task("+AProject (A) Some task +TestProject");

			Assert.IsNotNull(t.Projects);
			Assert.IsTrue(t.Projects.Contains("TestProject"));
			Assert.IsFalse(t.Projects.Contains("AProject"));
		}

		[Test]
		[Ignore]
		public void Task_Contexts_ContextsListShouldBeAfterPriority()
		{
			var t = new Task("@AContext (A) Some task @TestContext");

			Assert.IsNotNull(t.Contexts);
			Assert.IsTrue(t.Projects.Contains("TestContext"));
			Assert.IsFalse(t.Projects.Contains("AContext"));
		}

		[Test]
		public void Task_Contexts_ContextssListShouldNotContainInvalidContexts()
		{
			var t = new Task("Some task @TestContext @AnotherContext!");

			Assert.IsNotNull(t.Contexts);
			Assert.IsTrue(t.Contexts.Contains("TestContext"));
			Assert.IsFalse(t.Contexts.Contains("AnotherContext!"));
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
		}

		[Test]
		public void PriorityCompleted_SetIsCompletedToTrueOnATaskWithPriority_PriorityIsRemoved()
		{
			var t = new Task("(A) 2013-02-05 some task");
			t.CompletionDate = new DateTime(2013,8,2);

			Assert.AreEqual(new DateTime(2013, 8, 2), t.CompletionDate);
			Assert.AreEqual(new DateTime(2013, 2, 5), t.CreationDate);
			Assert.AreEqual("x 2013-08-02 2013-02-05 some task", t.Raw);
		}
    }
}
