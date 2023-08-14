using TodoList;

namespace TodoTest
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
            TodoManager manager = new TodoManager();
        }

        [Test]
        public void GetTaskByIdNonExistingReturnsNull()
        {
            TodoManager manager = new TodoManager();

            int taskIdToFind = 1;
            TaskItem taskItem = manager.GetTaskById(taskIdToFind);

            Assert.IsNull(taskItem);
        }

        [Test]
        public void GetTaskByTitleNonExistingReturnsNull()
        {
            TodoManager manager = new TodoManager();

            string taskTitleToFind = "Task 1";
            TaskItem taskItem = manager.GetTaskByTitle(taskTitleToFind);

            Assert.IsNull(taskItem);
        }

    }
}